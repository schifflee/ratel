using System;
using System.Collections.Generic;
using System.Text;
using NLog;
using OpenQA.Selenium;

namespace Ratel.Web
{
    public class SwitchTo
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly ITargetLocator _targetLocator;
        public SwitchTo(ITargetLocator targetLocator)
        {
            _targetLocator = targetLocator;
        }

        public IWebDriver Frame(int frameIndex)
        {
            return _targetLocator.Frame(frameIndex);
        }


        public IWebDriver Frame(string frameName)
        {
            return _targetLocator.Frame(frameName);
        }

        public IWebDriver Frame(IWebElement frameElement)
        {
            return _targetLocator.Frame(frameElement);
        }

        public IWebDriver ParentFrame()
        {
            return _targetLocator.ParentFrame();
        }

        public IWebDriver Window(string windowName)
        {
            return _targetLocator.ParentFrame();
        }

        public IWebDriver DefaultContent()
        {
            return _targetLocator.DefaultContent();
        }

        public IWebElement ActiveElement()
        {
            return _targetLocator.ActiveElement();
        }

        public IAlert Alert()
        {
            return _targetLocator.Alert();
        }
    }
}
