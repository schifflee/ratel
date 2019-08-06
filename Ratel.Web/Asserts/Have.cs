using System;
using System.Collections.Generic;
using System.Text;

namespace Ratel.Web.Asserts
{
    public class Have
    {
        private readonly RWebElement _rWebElement;
        private readonly AutomationManager _automationManager;
        private readonly bool _condition;
        private readonly string _description;

        public Have(RWebElement rWebElement, AutomationManager automationManager, bool condition, string description)
        {
            _rWebElement = rWebElement;
            _automationManager = automationManager;
            _condition = condition;
            _description = description;
        }

        private string GetDescription(string propertyName)
        {
            return $"{_description} {propertyName}";
        }

        public StringCondition Text => new StringCondition(() => _rWebElement.Text, _automationManager, _condition, GetDescription(nameof(Text)));
        public StringCondition Value => new StringCondition(() => _rWebElement.Value, _automationManager, _condition, GetDescription(nameof(Value)));
        public StringCondition Attribute(string attributeName) => new StringCondition(() => _rWebElement.GetAttribute(attributeName), _automationManager, _condition, GetDescription( $"{nameof(Attribute)}({attributeName})"));
    }
}
