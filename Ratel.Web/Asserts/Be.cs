using System;
using System.Collections.Generic;
using System.Text;

namespace Ratel.Web.Asserts
{
    public class Be
    {

        private readonly bool _condition;
        private readonly string _description;
        private readonly RWebElement _rWebElement;
        private readonly AutomationManager _automationManager;

        public Be(bool condition, string description, RWebElement rWebElement, AutomationManager automationManager)
        {

            _condition = condition;
            _description = description;
            _rWebElement = rWebElement;
            _automationManager = automationManager;
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

        private string GetDescription(string propertyName)
        {
            return $"{_description} {propertyName}";
        }
    }
}
