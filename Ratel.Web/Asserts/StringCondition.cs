using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace Ratel.Web.Asserts
{
    public class StringCondition
    {
        private readonly Func<string> _value;

        private readonly bool _condition;

        private readonly RWebElement _rWebElement;

        private readonly AutomationManager _automationManager;

        private readonly string _description;

        string currentCondition = string.Empty;

        public StringCondition(Func<string> value, AutomationManager automationManager, bool condition, string description)
        {
            _value = value;
            _automationManager = automationManager;
            _condition = condition;
            _description = description;
        }

        public RWebElement AreEqual(string value)
        {
            currentCondition = nameof(AreEqual);
            GetStringCondition(AreEqualCondition, value);
            return _rWebElement;
        }

        public RWebElement Contains(string value)
        {
            currentCondition = nameof(Contains);
            GetStringCondition(ContainsCondition, value);
            return _rWebElement;
        }

        public RWebElement StartsWith(string value)
        {
            currentCondition = nameof(StartsWith);
            GetStringCondition(StartsWithCondition, value);
            return _rWebElement;
        }

        public RWebElement EndsWith(string value)
        {
            currentCondition = nameof(EndsWith);
            GetStringCondition(EndsWithCondition, value);
            return _rWebElement;
        }

        public RWebElement IsEmpty()
        {
            currentCondition = nameof(IsEmpty);
            GetStringCondition(AreEqualCondition, string.Empty);
            return _rWebElement;
        }

        private bool AreEqualCondition(string expected, string actual)
        {
            return (actual == expected) == _condition;
        }

        private bool ContainsCondition(string expected, string actual)
        {
            return actual.Contains(expected) == _condition;
        }

        private bool StartsWithCondition(string expected, string actual)
        {
            return actual.StartsWith(expected) == _condition;
        }

        private bool EndsWithCondition(string expected, string actual)
        {
            return actual.EndsWith(expected) == _condition;
        }

        private void GetStringCondition(Func<string, string, bool> condition, string expected)
        {
            var actual = string.Empty;
            try
            {
                _automationManager.Wait(_value).Until(x =>
                {
                     actual = x();
                    if (condition(actual, expected))
                    {
                        return true;
                    }
                    return false;
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new WebDriverTimeoutException($"Condition is not a true: {_description} {currentCondition} Expected: {expected}. Actual: {actual} ", e);
            }
        }
    }
}
