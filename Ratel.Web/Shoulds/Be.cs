using System;
using Ratel.Web.Helpers;

namespace Ratel.Web.Shoulds
{
    public class Be
    {
        private readonly ConditionBuilder _condition;
        private readonly RWebElement _rWebElement;
        private readonly AutomationManager _automationManager;

        public Be(ConditionBuilder condition, RWebElement rWebElement, AutomationManager automationManager)
        {
            _condition = condition;
            _rWebElement = rWebElement;
            _automationManager = automationManager;
        }

        public RWebElement Exist()
        {
            ExecuteCondition(x => x.Exist == _condition.ExpectedCondition, nameof(Exist));
            return _rWebElement;
        }

        public RWebElement Visible()
        {
            ExecuteCondition(x => x.Displayed == _condition.ExpectedCondition, nameof(Visible));
            return _rWebElement;
        }

        public RWebElement Clickable()
        {
            ExecuteCondition(x => (x.Displayed && x.Enabled) == _condition.ExpectedCondition, nameof(Clickable));
            return _rWebElement;
        }

        public RWebElement Selected()
        {
            ExecuteCondition(x => x.Selected == _condition.ExpectedCondition, nameof(Selected));
            return _rWebElement;
        }

        public RWebElement Enabled()
        {
            ExecuteCondition(x => x.Enabled == _condition.ExpectedCondition, nameof(Enabled));
            return _rWebElement;
        }

        public RWebElement Displayed()
        {
            ExecuteCondition(x => x.Displayed == _condition.ExpectedCondition, nameof(Displayed));
            return _rWebElement;
        }

        private void ExecuteCondition(Func<RWebElement, bool> condition, string conditionName)
        {
            var wait = _automationManager.DefaultWait(_rWebElement);
            wait.Message = $"Failed to get condition: {_condition.Description} {conditionName}";
            wait.Until(condition);
        }
    }
}
