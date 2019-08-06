using System;
using System.Collections.Generic;
using System.Text;

namespace Ratel.Web.Asserts
{
    public class Be
    {

        private readonly RWebElement _rWebElement;
        private readonly AutomationManager _automationManager;
        private readonly bool _condition;

        public Be(RWebElement rWebElement, AutomationManager automationManager, bool condition)
        {
            _rWebElement = rWebElement;
            _automationManager = automationManager;
            _condition = condition;
        }

        public RWebElement Exist()
        {
            _automationManager.Wait(_rWebElement).Until(x => x.Exist);
            return _rWebElement;
        }

        public RWebElement Visible()
        {
            _automationManager.Wait(_rWebElement).Until(x => x.Displayed == _condition);
            return _rWebElement;
        }

        public RWebElement Clickable()
        {
            _automationManager.Wait(_rWebElement).Until(x => (x.Displayed && x.Enabled) == _condition);
            return _rWebElement;
        }

        public RWebElement Selected()
        {
            _automationManager.Wait(_rWebElement).Until(x => x.Selected == _condition);
            return _rWebElement;
        }

        public RWebElement Enabled()
        {
            _automationManager.Wait(_rWebElement).Until(x => x.Enabled == _condition);
            return _rWebElement;
        }

        public RWebElement Displayed()
        {
            _automationManager.Wait(_rWebElement).Until(x => x.Displayed == _condition);
            return _rWebElement;
        }
    }
}
