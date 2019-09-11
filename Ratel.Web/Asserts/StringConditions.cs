using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Ratel.Web.Asserts
{
    public class StringConditions
    {
        private readonly Func<string> _value;
        private readonly string _description;

        public StringConditions(Func<string> value, string description)
        {
            _value = value;
            _description = description;
        }

        public StringAsserts Is
            => new StringAsserts(_value, _description, true);

        public StringAsserts toBe(bool condition)
            => new StringAsserts(_value, _description, condition);

        public StringAsserts IsNot
            => new StringAsserts(_value, _description, false);
    }

    public class BoolConditions
    {
        private readonly Func<bool> _value;
        private readonly string _description;

        public BoolConditions(Func<bool> value, string description)
        {
            _value = value;
            _description = description;
        }

        public void Is(bool condition) {
            var actual = _value();
            if (condition)
            {
                Assert.IsTrue(actual, $"{_description} is not a {condition}. Actual: '{actual}'");
            }
            else
            {
                Assert.IsFalse(actual, $"{_description} is a {condition}. Actual: '{actual}'");
            }
        }
    }
}
