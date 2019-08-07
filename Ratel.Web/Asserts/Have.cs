using System;
using System.Collections.Generic;
using System.Text;

namespace Ratel.Web.Asserts
{
    public class Have
    {
        private readonly bool _condition;
        private readonly string _description;
        private readonly RWebElement _rWebElement;
        private readonly AutomationManager _automationManager;

        public Have(bool condition, string description, RWebElement rWebElement, AutomationManager automationManager)
        {
            _condition = condition;
            _description = description;
            _rWebElement = rWebElement;
            _automationManager = automationManager;
        }

        private string GetDescription(string propertyName)
        {
            return $"{_description} {propertyName}";
        }

        public StringConditions Text 
            => new StringConditions(() => _rWebElement.Text, _condition, GetDescription(nameof(Text)), _automationManager, _rWebElement);
        public StringConditions Value 
            => new StringConditions(() => _rWebElement.Value, _condition, GetDescription(nameof(Value)), _automationManager, _rWebElement);
        public StringConditions Attribute(string attributeName) 
            => new StringConditions(() => _rWebElement.GetAttribute(attributeName), _condition, GetDescription( $"{nameof(Attribute)}({attributeName})"), _automationManager, _rWebElement);
    }
}
