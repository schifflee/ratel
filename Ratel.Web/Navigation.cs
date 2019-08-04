using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using NLog;
using OpenQA.Selenium;
using Ratel.Web.Attributrs;

namespace Ratel.Web
{
    public class Navigation : INavigation
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IRWebDriver _driver;
        private readonly AutomationManager _automationManager;
        public Navigation(IRWebDriver driver, AutomationManager automationManager)
        {
            _driver = driver;
            _automationManager = automationManager;
        }

        public T OpenPage<T>(Func<AutomationManager, T> page) where T : BasePage<T>
        {
            string url;
            try
            {
                 url = typeof(T).GetTypeInfo().GetCustomAttribute<PageUrlAttribute>().Path;
            }
            catch (NullReferenceException)
            {
                Logger.Info("Ensure Page Url attribute is exist");
                throw;
            }

            GoToUrl(_automationManager.Config.BaseUrl + url);
            return page(_automationManager);
        }

        public void ScrollToElement(IWebElement element)
        {
            Logger.Debug($"Scroll to element");
            _driver.ExecuteScript("arguments[0].scrollIntoView(true);");
        }

        public void Back()
        {
            _driver.WrappedDriver.Navigate().Back();
        }

        public void Forward()
        {
            _driver.WrappedDriver.Navigate().Forward();
        }

        public void GoToUrl(string url)
        {
            Logger.Debug($"Go To Url: {url}");
            _driver.WrappedDriver.Navigate().GoToUrl(url);
        }

        public void GoToUrl(Uri url)
        {
            _driver.WrappedDriver.Navigate().GoToUrl(url);
        }

        public void Refresh()
        {
            _driver.WrappedDriver.Navigate().Refresh();
        }
    }
}
