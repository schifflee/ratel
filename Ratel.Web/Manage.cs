using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;
using NLog;

namespace Ratel.Web.Services
{
    public class Manage : IOptions
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IRWebDriver _driver;
        private readonly AutomationManager _automationManager;
        public Manage(IRWebDriver driver, AutomationManager automationManager)
        {
            _driver = driver;
            _automationManager = automationManager;
        }

        public ITimeouts Timeouts()
        {
            return _driver.Manage().Timeouts();
        }

        public ICookieJar Cookies => _driver.Manage().Cookies;
        public IWindow Window => _driver.Manage().Window;
        public ILogs Logs => _driver.Manage().Logs;
    }
}
