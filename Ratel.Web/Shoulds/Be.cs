using System;
using Ratel.Web.Helpers;

namespace Ratel.Web.Shoulds
{
    public class Be
    {
        private readonly ExpressionBuilder _expression;
        private readonly RWebElement _rWebElement;
        private readonly AutomationManager _automationManager;

        public Be(ExpressionBuilder expression, RWebElement rWebElement, AutomationManager automationManager)
        {
            _expression = expression;
            _rWebElement = rWebElement;
            _automationManager = automationManager;
        }

        public RWebElement Exist()
        {
            ExecuteCondition(x => x.Exist == _expression.Condition.Operator, nameof(Exist));
            return _rWebElement;
        }

        public RWebElement Visible()
        {
            ExecuteCondition(x => x.Displayed == _expression.Condition.Operator, nameof(Visible));
            return _rWebElement;
        }

        public RWebElement Clickable()
        {
            ExecuteCondition(x => (x.Displayed && x.Enabled) == _expression.Condition.Operator, nameof(Clickable));
            return _rWebElement;
        }

        public RWebElement Selected()
        {
            ExecuteCondition(x => x.Selected == _expression.Condition.Operator, nameof(Selected));
            return _rWebElement;
        }

        public RWebElement Enabled()
        {
            ExecuteCondition(x => x.Enabled == _expression.Condition.Operator, nameof(Enabled));
            return _rWebElement;
        }

        public RWebElement Displayed()
        {
            ExecuteCondition(x => x.Displayed == _expression.Condition.Operator, nameof(Displayed));
            return _rWebElement;
        }

        private void ExecuteCondition(Func<RWebElement, bool> condition, string conditionName)
        {
            var wait = _automationManager.DefaultWait(_rWebElement);
            wait.Message = $"Failed to get condition: {_expression.Condition.Description} {conditionName}";
            wait.Until(condition);
        }
    }
}
