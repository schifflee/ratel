using System;
using System.Collections.Generic;
using System.Text;
using NLog;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using Ratel.Web.Models;
using Ratel.Web.Services;

namespace Ratel.Web
{
    public class TestRunner
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly AutomationManager _automationManager;
        public TestConfig Config { get; set; }

        public TestRunner(TestConfig config)
        {
            Config = config;
            _automationManager = new AutomationManager
            {
                Config = config,
            };
        }

        public TestRunner()
        {
            Config = TestConfigService.GetConfig();
            _automationManager = new AutomationManager
            {
                Config = Config
            };
        }

        public void Run()
        {
            Logger.Debug($"START TEST: {TestContext.CurrentContext.Test.Name}");
            _automationManager.Driver = new RWebDriver(StartDriver(_automationManager), _automationManager);
        }

        public T OpenPage<T>(Func<AutomationManager, T> page) where T : BasePage<T>
        {
            return _automationManager.Driver.Navigate().OpenPage(page);
        }

        public void Stop()
        {
            Logger.Debug($"END TEST: {TestContext.CurrentContext.Test.Name}");
            try
            {
                if (TestContext.CurrentContext.Result.Outcome.Status != TestStatus.Passed)
                {
                    var screenshotPath =
                        $"{TestContext.CurrentContext.TestDirectory}\\{TestContext.CurrentContext.Test.Name}-{DateTime.Now:MM-dd-yyyy-hh-mm-ss}.jpeg";

                    _automationManager.Driver.GetScreenshot(screenshotPath);
                    TestContext.AddTestAttachment(screenshotPath);
                }

                _automationManager.Driver?.Quit();
                Logger.Debug("Close Driver");
            }
            catch (Exception e)
            {
                Logger.Error(e, "Close Driver after Exception");
                _automationManager.Driver?.Quit();
            }
        }

        private IRWebDriver StartDriver(AutomationManager automationManager)
        {

            Logger.Debug($"Init {automationManager.Config.Browser} driver");
            switch (automationManager.Config.Browser)
            {
                case "Edge":
                    return new RWebDriver(new EdgeDriver(EdgeDriverService.CreateDefaultService(), new EdgeOptions(), TimeSpan.FromSeconds(Config.TimeoutMilliseconds)), automationManager);
                case "Firefox":
                    return new RWebDriver(new FirefoxDriver(FirefoxDriverService.CreateDefaultService(), new FirefoxOptions(), TimeSpan.FromSeconds(Config.TimeoutMilliseconds)), automationManager);
                default:
                    if (string.IsNullOrEmpty(Config.ChromeChromeOptions.BinaryLocation))
                    {
                        return new RWebDriver(new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, GetChromeOptions()), automationManager);
                    }
                    return new RWebDriver(new ChromeDriver(GetChromeOptions()), automationManager);
            }
        }

        public ChromeOptions GetChromeOptions()
        {
            var options = new ChromeOptions();

            if (string.IsNullOrEmpty(Config.ChromeChromeOptions.BinaryLocation))
            {
                options.BinaryLocation = Config.ChromeChromeOptions.BinaryLocation;
            }

            Config.ChromeChromeOptions.Arguments.ForEach(x =>
            {
                options.AddArgument(x);
            });

            foreach (var data in Config.ChromeChromeOptions.AdditionalCapability)
            {
                options.AddAdditionalCapability(data.Key, data.Value);
            }

            foreach (var data in Config.ChromeChromeOptions.LocalStatePreference)
            {
                options.AddLocalStatePreference(data.Key, data.Value);
            }

            foreach (var data in Config.ChromeChromeOptions.UserProfilePreference)
            {
                options.AddUserProfilePreference(data.Key, data.Value);
            }
            return options;
        }
    }
}
