using Ratel.Web.Asserts;
using Ratel.Web.Helpers;

namespace Ratel.Web.Shoulds
{
    public class Should
    {
        private readonly RWebElement _rWebElement;
        private readonly AutomationManager _automationManager;
        private readonly ConditionBuilder _condition;

        public Should(ConditionBuilder condition, RWebElement rWebElement, AutomationManager automationManager)
        {
            _condition = condition;
            _rWebElement = rWebElement;
            _automationManager = automationManager;

        }

        public Have Have 
            => new Have(_condition.Append(nameof(Have)), _rWebElement, _automationManager);

        public Be Be 
            => new Be( _condition.Append(nameof(Be)), _rWebElement, _automationManager);
    }
}
