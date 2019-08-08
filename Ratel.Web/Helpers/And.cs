using System;
using System.Collections.Generic;
using System.Text;
using Ratel.Web.Shoulds;

namespace Ratel.Web.Helpers
{
    public class And
    {
        private readonly RWebElement _rWebElement;

        public And(RWebElement rWebElement)
        {
            _rWebElement = rWebElement;
        }

        public Should Should => _rWebElement.Should;
        public Should ShouldNot => _rWebElement.ShouldNot;
    }
}
