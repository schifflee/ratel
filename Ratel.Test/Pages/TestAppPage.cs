using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using Ratel.Web;
using Ratel.Web.Attributrs;

namespace Ratel.Test.Pages
{
    [PageUrl(@"\TestApp\index.html")]
    public class TestAppPage : BasePage<TestAppPage>
    {
        public TestAppPage(AutomationManager automationManager) : base(automationManager)
        {
        }
        public TestAppPage EnterFullName(string name)
        {
            El(By.XPath("//*[@name='name']"), "Name field").SendKeys(name);
            return this;
        }

        public TestAppPage AssertFullNameValueAreEqual(string text)
        {
            Els(By.XPath("//*[@name='name']"), "Name field")[0].Assert.Value.Is.Equal(text);
            return this;
        }

        public TestAppPage AssertFullNameValueIsEmpty()
        {
            Els(By.XPath("//*[@name='name']"), "Name field")[0].Assert.Value.Is.Empty();
            return this;
        }
    }
}
