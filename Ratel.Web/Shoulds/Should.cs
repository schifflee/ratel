using Ratel.Web.Asserts;
using Ratel.Web.Helpers;

namespace Ratel.Web.Shoulds
{
    public class Should
    {
        private readonly RWebElement _rWebElement;
        private readonly AutomationManager _automationManager;
        private readonly ExpressionBuilder _expression;

        public Should(ExpressionBuilder expression, RWebElement rWebElement, AutomationManager automationManager)
        {
            _expression = expression;
            _rWebElement = rWebElement;
            _automationManager = automationManager;

        }

        public Have Have 
            => new Have(_expression.Append(nameof(Have)), _rWebElement, _automationManager);

        public Be Be 
            => new Be( _expression.Append(nameof(Be)), _rWebElement, _automationManager);
    }
}
