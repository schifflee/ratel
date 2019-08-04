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
            var actual = GetNotEmpty();
            Assert.AreEqual(value, actual, $"'{_description}' is not equal to expected. Expected: '{value}'  Actual: '{actual}'");
        }

        public void Contains(string value)
        {
            var actual = GetNotEmpty();
            Assert.IsTrue(_value().Contains(value), $"'{_description}' is not contain expected. Expected: '{value}'  Actual: '{actual}'");
        }

        public void StartsWith(string value)
        {
            var actual = GetNotEmpty();
            Assert.IsTrue(_value().StartsWith(value), $"'{_description}' is not start with expected. Expected: '{value}'  Actual: '{actual}'");
        }

        public void EndsWith(string value)
        {
            var actual = GetNotEmpty();
            Assert.IsTrue(_value().EndsWith(value), $"'{_description}' is not start with expected. Expected: '{value}'  Actual: '{actual}'");
        }

        public void IsNotEmpty()
        {
            var actual = GetNotEmpty();
            Assert.IsFalse(string.IsNullOrEmpty(_value()), $"'{_description}' is empty, but expected is not empty. Actual: '{actual}'");
        }

        public void IsEmpty()
        {
            var actual = _value();
            Assert.IsTrue(string.IsNullOrEmpty(_value()), $"'{_description}' is not empty, but expected empty. Actual: '{actual}'");
        }

        private string GetNotEmpty()
        {
            try
            {
                return _automationManager.Wait(_value).Until(x =>
                {
                    var value = x();
                    if (!string.IsNullOrEmpty(value))
                    {
                        return value;
                    }
                    return null;
                });
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
