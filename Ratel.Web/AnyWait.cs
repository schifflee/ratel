using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Ratel.Web
{
    public class AnyWait<T> : IWait<T>
    {
        private readonly T _input;
        private readonly IClock _clock;

        private readonly List<Type> _ignoredExceptions = new List<Type>();

        /// <summary>
        /// Initializes a new instance of the <see cref="AnyWait&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="input">The input value to pass to the evaluated conditions.</param>
        public AnyWait(T input)
            : this(input, new SystemClock())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AnyWait&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="input">The input value to pass to the evaluated conditions.</param>
        /// <param name="clock">The clock to use when measuring the timeout.</param>
        public AnyWait(T input, IClock clock)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input", "input cannot be null");
            }

            this._input = input;
            this._clock = clock ?? throw new ArgumentNullException("clock", "clock cannot be null");
        }

        /// <summary>
        /// Gets or sets how long to wait for the evaluated condition to be true. The default timeout is 500 milliseconds.
        /// </summary>
        public TimeSpan Timeout { get; set; } = DefaultSleepTimeout;

        /// <summary>
        /// Gets or sets how often the condition should be evaluated. The default timeout is 500 milliseconds.
        /// </summary>
        public TimeSpan PollingInterval { get; set; } = DefaultSleepTimeout;

        /// <summary>
        /// Gets or sets the message to be displayed when time expires.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        private static TimeSpan DefaultSleepTimeout => TimeSpan.FromMilliseconds(500);

        /// <summary>
        /// Configures this instance to ignore specific types of exceptions while waiting for a condition.
        /// Any exceptions not whitelisted will be allowed to propagate, terminating the wait.
        /// </summary>
        /// <param name="exceptionTypes">The types of exceptions to ignore.</param>
        public void IgnoreExceptionTypes(params Type[] exceptionTypes)
        {
            if (exceptionTypes == null)
            {
                throw new ArgumentNullException("exceptionTypes", "exceptionTypes cannot be null");
            }

            foreach (Type exceptionType in exceptionTypes)
            {
                if (!typeof(Exception).IsAssignableFrom(exceptionType))
                {
                    throw new ArgumentException("All types to be ignored must derive from System.Exception", "exceptionTypes");
                }
            }

            _ignoredExceptions.AddRange(exceptionTypes);
        }

        /// <summary>
        /// Repeatedly applies this instance's input value to the given function until one of the following
        /// occurs:
        /// <para>
        /// <list type="bullet">
        /// <item>the function returns neither null</item>
        /// <item>the function throws an exception that is not in the list of ignored exception types</item>
        /// <item>the timeout expires</item>
        /// </list>
        /// </para>
        /// </summary>
        /// <typeparam name="TResult">The delegate's expected return type.</typeparam>
        /// <param name="condition">A delegate taking an object of type T as its parameter, and returning a TResult.</param>
        /// <returns>The delegate's return value.</returns>
        public TResult Until<TResult>(Func<T, TResult> condition)
        {
            if (condition == null)
            {
                throw new ArgumentNullException("condition", "condition cannot be null");
            }

            var resultType = typeof(TResult);
            if ((resultType.IsValueType && resultType != typeof(bool)) || !typeof(object).IsAssignableFrom(resultType))
            {
                throw new ArgumentException("Can only wait on an object or boolean response, tried to use type: " + resultType.ToString(), "condition");
            }

            Exception lastException = null;
            var endTime = _clock.LaterBy(Timeout);
            while (true)
            {
                try
                {
                    var result = condition(_input);
                    if (result != null)
                    {
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    if (!IsIgnoredException(ex))
                    {
                        throw;
                    }
                    lastException = ex;
                }

                // Check the timeout after evaluating the function to ensure conditions
                // with a zero timeout can succeed.
                if (!_clock.IsNowBefore(endTime))
                {
                    var timeoutMessage = string.Format(CultureInfo.InvariantCulture, "Timed out after {0} seconds", Timeout.TotalSeconds);
                    if (!string.IsNullOrEmpty(Message))
                    {
                        timeoutMessage += ": " + Message;
                    }

                    ThrowTimeoutException(timeoutMessage, lastException);
                }

                Thread.Sleep(PollingInterval);
            }
        }

        public void Until<TResult>(Action action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("condition", "condition cannot be null");
            }

            var resultType = typeof(TResult);
            if ((resultType.IsValueType && resultType != typeof(bool)) || !typeof(object).IsAssignableFrom(resultType))
            {
                throw new ArgumentException("Can only wait on an object or boolean response, tried to use type: " + resultType.ToString(), "condition");
            }

            var endTime = _clock.LaterBy(Timeout);
            while (true)
            {
                Exception lastException;
                try
                {
                    action();
                    break;
                }
                catch (Exception ex)
                {
                    if (!IsIgnoredException(ex))
                    {
                        throw;
                    }
                    lastException = ex;
                }

                // Check the timeout after evaluating the function to ensure conditions
                // with a zero timeout can succeed.
                if (!_clock.IsNowBefore(endTime))
                {
                    var timeoutMessage = string.Format(CultureInfo.InvariantCulture, "Timed out after {0} seconds", Timeout.TotalSeconds);
                    if (!string.IsNullOrEmpty(Message))
                    {
                        timeoutMessage += ": " + Message;
                    }

                    ThrowTimeoutException(timeoutMessage, lastException);
                }

                Thread.Sleep(PollingInterval);
            }
        }

        /// <summary>
        /// Throws a <see cref="WebDriverTimeoutException"/> with the given message.
        /// </summary>
        /// <param name="exceptionMessage">The message of the exception.</param>
        /// <param name="lastException">The last exception thrown by the condition.</param>
        /// <remarks>This method may be overridden to throw an exception that is
        /// idiomatic for a particular test infrastructure.</remarks>
        protected virtual void ThrowTimeoutException(string exceptionMessage, Exception lastException)
        {
            throw new WebDriverTimeoutException(exceptionMessage, lastException);
        }

        private bool IsIgnoredException(Exception exception)
        {
            return _ignoredExceptions.Any(type => type.IsInstanceOfType(exception));
        }
    }

}
