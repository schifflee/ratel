using System;
using System.Collections.Generic;
using System.Text;

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

        public BoolAssert Is
            => new BoolAssert(_value, _description, true);

        public BoolAssert IsNot
            => new BoolAssert(_value, _description, false);
    }
}
