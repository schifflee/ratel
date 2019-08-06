using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Ratel.Web.Asserts
{
    public class AssertString
    {
        private readonly Func<string> _value;
        private readonly string _description;

        private readonly AutomationManager _automationManager;
        public AssertString(Func<string> value, string description, AutomationManager automationManager)
        {
            _value = value;
            _description = description;
            _automationManager = automationManager;
        }

        public void AreEqual(string value)
        {
            WaitNotEmpty();
            var actual = _value();
            Assert.AreEqual(actual, value, $"'{_description}' is not equal to expected. Expected: '{value}'  Actual: '{actual}'");
        }

        public void Contains(string value)
        {
            WaitNotEmpty();
            var actual = _value();
            Assert.IsTrue(actual.Contains(value), $"'{_description}' is not contain expected. Expected: '{value}'  Actual: '{actual}'");
        }

        public void StartsWith(string value)
        {
            WaitNotEmpty();
            var actual = _value();
            Assert.IsTrue(actual.StartsWith(value), $"'{_description}' is not start with expected. Expected: '{value}'  Actual: '{actual}'");
        }

        public void EndsWith(string value)
        {
            WaitNotEmpty();
            var actual = _value();
            Assert.IsTrue(actual.EndsWith(value), $"'{_description}' is not start with expected. Expected: '{value}'  Actual: '{actual}'");
        }

        public void IsNotEmpty()
        {
            WaitNotEmpty();
            var actual = _value();
            Assert.IsFalse(string.IsNullOrEmpty(actual), $"'{_description}' is empty, but expected is not empty. Actual: '{actual}'");
        }

        public void IsEmpty()
        {
            var actual = _value();
            Assert.IsTrue(string.IsNullOrEmpty(_value()), $"'{_description}' is not empty, but expected empty. Actual: '{actual}'");
        }

        private void WaitNotEmpty()
        {
            try
            {
                _automationManager.Wait(this).Until(x =>
                {
                    if (!string.IsNullOrEmpty(x._value()))
                    {
                        return true;
                    }
                    return false;
                });
            }
            catch
            {
                // ignored
            }
        }
    }
}
