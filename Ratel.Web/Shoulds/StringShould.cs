using System;
using Ratel.Web.Helpers;

namespace Ratel.Web.Shoulds
{
    public class StringShould
    {

        private readonly RWebElement _rWebElement;

        private readonly AutomationManager _automationManager;

        private readonly ExpressionBuilder<string> _condition;


        public StringShould(ExpressionBuilder<string> condition, AutomationManager automationManager, RWebElement rWebElement)
        {
            _automationManager = automationManager;
            _condition = condition;
            _rWebElement = rWebElement;
        }

        public RWebElement Equal(string value)
        {
            ExecuteCondition(AreEqualCondition, value, nameof(EndsWith));
            return _rWebElement;
        }

        public RWebElement Contains(string value)
        {
            ExecuteCondition(ContainsCondition, value, nameof(EndsWith));
            return _rWebElement;
        }

        public RWebElement StartsWith(string value)
        {
            ExecuteCondition(StartsWithCondition, value, nameof(EndsWith));
            return _rWebElement;
        }

        public RWebElement EndsWith(string value)
        {
            ExecuteCondition(EndsWithCondition, value, nameof(EndsWith));
            return _rWebElement;
        }

        public RWebElement IsEmpty()
        {
            ExecuteCondition(AreEqualCondition, string.Empty, nameof(IsEmpty));
            return _rWebElement;
        }

        private bool AreEqualCondition(string expected, string actual)
        {
            return (actual == expected) == _condition.Condition.Operator;
        }

        private bool ContainsCondition(string expected, string actual)
        {
            return actual.Contains(expected) == _condition.Condition.Operator;
        }

        private bool StartsWithCondition(string expected, string actual)
        {
            return actual.StartsWith(expected) == _condition.Condition.Operator;
        }

        private bool EndsWithCondition(string expected, string actual)
        {
            return actual.EndsWith(expected) == _condition.Condition.Operator;
        }

        private void ExecuteCondition(Func<string, string, bool> condition, string expected, string propertyName)
        {
            var wait = _automationManager.DefaultWait(_condition);

            wait.Until(x =>
            {
                var actual = x.Expression();
                wait.Message =
                    $"Failed to get condition: {_condition.Condition.Description} {propertyName}: {expected}. Actual: {actual} ";
                return condition(actual.ToString(), expected);
            });
        }
    }
}
