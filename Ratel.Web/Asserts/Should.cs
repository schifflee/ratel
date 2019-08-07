namespace Ratel.Web.Asserts
{
    public class Should
    {
        private readonly RWebElement _rWebElement;
        private readonly AutomationManager _automationManager;
        private readonly bool _condition;
        private readonly string _description;

        public Should(RWebElement rWebElement, AutomationManager automationManager, bool condition, string description)
        {
            _rWebElement = rWebElement;
            _automationManager = automationManager;
            _condition = condition;
            _description = description;
        }

        private string GetDescription(string propertyName)
        {
            return $"{_rWebElement} {_description} {propertyName}";
        }

        public Have Have => new Have(_condition, GetDescription(nameof(Have)), _rWebElement, _automationManager);
        public Be Be => new Be(_condition, GetDescription(nameof(Be)), _rWebElement, _automationManager);
    }
}
