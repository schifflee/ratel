using System;
using System.Collections.ObjectModel;
using NLog;
using OpenQA.Selenium;
using Ratel.Web.Asserts;

namespace Ratel.Web
{
    public class RWebDriver : IRWebDriver
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IWebDriver _driver;

        private readonly AutomationManager _automationManager;

        public RWebDriver(IWebDriver driver, AutomationManager automationManager)
        {
            _driver = driver;
            _automationManager = automationManager;
        }

        public override string ToString()
        {
            return nameof(RWebDriver);
        }

        public RWebElement FindElement(By by, string name)
        {
            return new RWebElement(_automationManager, new ElementFinder(by, name, this));
        }

        public RWebElementCollection FindElements(By by, string name)
        {
            return new RWebElementCollection(_automationManager, new ElementsFinder(by, name, this));
        }

        IWebElement ISearchContext.FindElement(By by)
        {
            return _driver.FindElement(by);
        }

        ReadOnlyCollection<IWebElement> ISearchContext.FindElements(By @by)
        {
            return _driver.FindElements(by);
        }

        public void Dispose()
        {
            _driver?.Dispose();
        }

        public void Close()
        {
            _driver?.Close();
        }

        public void Quit()
        {
            _driver?.Quit();
        }

         IOptions IWebDriver.Manage()
        {
            return _driver.Manage();
        }

        INavigation IWebDriver.Navigate()
        {
            return _driver.Navigate();
        }

        public Navigation Navigate()
        {
            return new Navigation(this, _automationManager);
        }

        ITargetLocator IWebDriver.SwitchTo()
        {
            return _driver.SwitchTo();
        }

        public SwitchTo SwitchTo()
        {
            return new SwitchTo(_driver.SwitchTo());
        }

        public string Url
        {
            get => _driver.Url;
            set => _driver.Url = value;
        }

        public string Title => _driver.Title;
        public string PageSource => _driver.PageSource;
        public string CurrentWindowHandle => _driver.CurrentWindowHandle;
        public ReadOnlyCollection<string> WindowHandles => _driver.WindowHandles;

        public IWebDriver WrappedDriver => _driver;

        public object ExecuteScript(string script, params object[] args)
        {
            var javaScriptExecutor = (IJavaScriptExecutor)_driver;
            return javaScriptExecutor.ExecuteScript(script, args);
        }

        public object ExecuteAsyncScript(string script, params object[] args)
        {
            var javaScriptExecutor = (IJavaScriptExecutor)_driver;
            return javaScriptExecutor.ExecuteAsyncScript(script, args);
        }

        public Screenshot GetScreenshot()
        {
            var takesScreenshot = (ITakesScreenshot)_driver;
            return takesScreenshot.GetScreenshot();
        }

        public void GetScreenshot(string filePath)
        {
            Logger.Debug($"Take screenshot. File path: {filePath}");
            var takesScreenshot = (ITakesScreenshot)_driver;
            var screenshot = takesScreenshot.GetScreenshot();
            screenshot.SaveAsFile(filePath);
        }
    }
}
