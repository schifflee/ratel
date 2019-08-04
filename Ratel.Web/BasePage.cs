using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace Ratel.Web
{
    public class BasePage <TPage>
    {
        protected readonly AutomationManager AutomationManager;

        public BasePage(AutomationManager automationManager)
        {
            AutomationManager = automationManager;
        }

        protected RWebElement El(By by, string name) => AutomationManager.El(by, name);
        protected RWebElementCollection Els(By by, string name) => AutomationManager.Els(by, name);

    }
}
