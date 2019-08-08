using Ratel.Web.Asserts;
using Ratel.Web.Helpers;

namespace Ratel.Web.Shoulds
{
    public class Have
    {
        private readonly ConditionBuilder _condition;
        private readonly RWebElement _rWebElement;
        private readonly AutomationManager _automationManager;

        public Have(ConditionBuilder condition, RWebElement rWebElement, AutomationManager automationManager)
        {
            _condition = condition;
            _rWebElement = rWebElement;
            _automationManager = automationManager;
        }

        public StringShould Text 
            => new StringShould(
                _condition.Append(nameof(Text)).SetResultFunc(() => _rWebElement.Text), 
                _automationManager, _rWebElement);

        public StringShould Value 
            => new StringShould(
                _condition.Append(nameof(Value)).SetResultFunc(() => _rWebElement.Value), 
                _automationManager, _rWebElement);

        public StringShould Attribute(string attributeName) 
            => new StringShould(
                _condition.Append($"{nameof(Attribute)}({attributeName})").SetResultFunc(() => _rWebElement.GetAttribute(attributeName)), 
                _automationManager, _rWebElement);
    }
}
