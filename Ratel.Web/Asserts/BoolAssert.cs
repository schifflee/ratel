using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace Ratel.Web.Asserts
{
    public class BoolAssert
    {
        private readonly Func<bool> _value;
        private readonly string _description;
        private readonly bool _condition;

        public BoolAssert(Func<bool> value, string description, bool condition)
        {
            _value = value;
            _description = description;
            _condition = condition;
        }

        public void True()
        {
            var actual = _value();
            if (_condition)
            {
                Assert.IsTrue(actual, $"{_description} is not a {_condition}. Actual: '{actual}'");
            }
            else
            {
                Assert.IsFalse(actual, $"{_description} is a {_condition}. Actual: '{actual}'");
            }

        }
    }
}
