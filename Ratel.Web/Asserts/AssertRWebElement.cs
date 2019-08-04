namespace Ratel.Web.Asserts
{
    public class AssertRWebElement
    {
        private readonly RWebElement _element;
        private readonly AutomationManager _automationManager;
        public AssertRWebElement(RWebElement element, AutomationManager automationManager)
        {
            _element = element;
            _automationManager = automationManager;
        }

        public AssertString Text => new AssertString(() => _element.Text, $"Text of {_element.Name}", _automationManager);

        public AssertString Value => new AssertString(() => _element.Value, $"Value of {_element.Name}", _automationManager);
    }
}
