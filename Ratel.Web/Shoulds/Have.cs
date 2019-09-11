using Ratel.Web.Asserts;
using Ratel.Web.Helpers;

namespace Ratel.Web.Shoulds
{
    public class Have
    {
        private readonly ExpressionBuilder _expression;
        private readonly RWebElement _rWebElement;
        private readonly AutomationManager _automationManager;

        public Have(ExpressionBuilder expression, RWebElement rWebElement, AutomationManager automationManager)
        {
            _expression = expression;
            _rWebElement = rWebElement;
            _automationManager = automationManager;
        }

        public StringShould Text 
            => new StringShould(_expression.Append(nameof(Text)).SetCondition(() => _rWebElement.Text), _automationManager, _rWebElement);

        public StringShould Value 
            => new StringShould(
                _expression.Append(nameof(Value)).SetCondition(() => _rWebElement.Value), 
                _automationManager, _rWebElement);

        public StringShould Attribute(string attributeName) 
            => new StringShould(
                _expression.Append($"{nameof(Attribute)}({attributeName})").SetCondition(() => _rWebElement.GetAttribute(attributeName)), 
                _automationManager, _rWebElement);
    }
}
