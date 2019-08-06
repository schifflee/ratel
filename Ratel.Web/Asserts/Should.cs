namespace Ratel.Web.Asserts
{
    public class Should
    {
        private readonly RWebElement _rWebElement;
        private readonly AutomationManager _automationManager;

        public Should(RWebElement rWebElement, AutomationManager automationManager)
        {
            _rWebElement = rWebElement;
            _automationManager = automationManager;
        }

        private string GetDescription(string conditionName)
        {
            return $"{_rWebElement} {nameof(Should)} {conditionName}";
        }

        public Have Have => new Have(_rWebElement, _automationManager, true, GetDescription(nameof(Have)));
        public Have NotHave => new Have(_rWebElement, _automationManager, false, GetDescription(nameof(NotHave)));
        public Be Be => new Be(_rWebElement, _automationManager, true);
        public Be NotBe => new Be(_rWebElement, _automationManager, false);
    }
}
