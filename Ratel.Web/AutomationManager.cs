using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Ratel.Web.Models;

namespace Ratel.Web
{
    public class AutomationManager
    {
        public TestConfig Config { get; set; }
        public IRWebDriver Driver { get; set; }

        public DefaultWait<IWebDriver> WebDriverWait
        {
            get
            {
                var wait = new DefaultWait<IWebDriver>(Driver);
                wait.IgnoreExceptionTypes(Config.IgnoredExceptions.ToArray());
                wait.PollingInterval = TimeSpan.FromMilliseconds(Config.WaitIntervalMilliseconds);
                wait.Timeout = TimeSpan.FromMilliseconds(Config.TimeoutMilliseconds);
                return wait;
            }
        }

        public DefaultWait<T> Wait<T>(T input)
        {
            var wait = new DefaultWait<T>(input);
            wait.IgnoreExceptionTypes(Config.IgnoredExceptions.ToArray());
            wait.PollingInterval = TimeSpan.FromMilliseconds(Config.WaitIntervalMilliseconds);
            wait.Timeout = TimeSpan.FromMilliseconds(Config.TimeoutMilliseconds);
            return wait;
        }

        public RWebElement El(By locator, [CallerMemberName]string elementName = "") => new RWebElement(this, locator, elementName);

        public RWebElementCollection Els(By locator, [CallerMemberName]string elementName = "") => new RWebElementCollection(this, locator, elementName);
    }
}
