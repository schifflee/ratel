using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Ratel.Web.Asserts
{
    public class StringAsserts
    {
        private readonly Func<string> _value;
        private readonly string _description;
        private readonly bool _condition;

        public StringAsserts(Func<string> value, string description, bool condition)
        {
            _value = value;
            _description = description;
            _condition = condition;
        }

        public void Equal(string value)
        {
            var actual = _value();
            if (_condition)
            {
                Assert.AreEqual(actual, value, $"{_description} are not equal to expected.");
            }
            else
            {
                Assert.AreNotEqual(actual, value, $"{_description} are equal to expected.");
            }
        }

        public void Contain(string value)
        {
            var actual = _value();
            Assert.IsTrue(actual.Contains(value) == _condition, $"{_description} contain '{value}' is not a {_condition}. Actual: '{actual}'");
        }

        public void StartsWith(string value)
        {
            var actual = _value();
            Assert.IsTrue(actual.StartsWith(value) == _condition, $"{_description} start with '{value}' is not a {_condition}. Actual: '{actual}'");
        }

        public void EndsWith(string value)
        {
            var actual = _value();
            Assert.IsTrue(actual.EndsWith(value) == _condition, $"{_description} start with '{value}' is not a {_condition}. Actual: '{actual}'");
        }

        public void Empty()
        {
            var actual = _value();
            Assert.IsTrue(string.IsNullOrEmpty(_value()) == _condition, $"{_description} is empty not {_condition} Actual: '{actual}'");
        }
    }
}
