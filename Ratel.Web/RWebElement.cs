using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Ratel.Web.Asserts;

namespace Ratel.Web
{
    public class RWebElement : IRWebElement
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly AutomationManager _automationManager;
        private readonly SearchContext<IWebElement> _context;

        public string Name { get; set; }
        internal IWebElement Cache { get; set; }

        public RWebElement(AutomationManager automationManager, By locator, [CallerMemberName]string name = "")
        {
            Name = name;
            _automationManager = automationManager;
            _context = new ElementFinder(locator, name, automationManager.Driver);
        }

        public RWebElement(AutomationManager automationManager, SearchContext<IWebElement> context)
        {
            Name = context.Name;
            _automationManager = automationManager;
            _context = context;
        }

        public override string ToString()
        {
            return _context.ToString();
        }

        IWebElement ISearchContext.FindElement(By by)
        {
            return _context.Find().FindElement(by);
        }

        ReadOnlyCollection<IWebElement> ISearchContext.FindElements(By by)
        {
            return _context.Find().FindElements(by);
        }

        public AssertRWebElement Assert => new AssertRWebElement(this, _automationManager);
        public Should Should => new Should(this, _automationManager);

        public RWebElement FindElement(By by, string name)
        {
            return new RWebElement(_automationManager, new ElementFinder(by, $"Child of {Name}", this));
        }

        public RWebElementCollection FindElements(By by, string name)
        {
            return new RWebElementCollection(_automationManager, new ElementsFinder(by, $"Children of {Name}", this));
        }

        public IWebElement Find() => Cache ?? (Cache = _context.Find());

        public bool Exist
        {
            get
            {
                try
                {
                    _context.Find();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public RWebElement Clear()
        {
            Logger.Info($"Clear '{Name}'");
            ExecuteAction(() => Should.Be.Clickable().Find().Clear());
            return this;
        }

        public RWebElement SendKeys(string text)
        {
            Logger.Info($"SendKeys '{text}' to '{Name}'");
            ExecuteAction(() => Should.Be.Clickable().Find().SendKeys(text));
            return this;
        }

        public RWebElement Submit()
        {
            Logger.Info($"Submit '{Name}'");
            ExecuteAction(() => Should.Be.Exist().Find().Submit());
            return this;
        }

        public RWebElement Click()
        {
            Logger.Info($"Click '{Name}'");
            ExecuteAction(() => Should.Be.Clickable().Find().Click());
            return this;
        }

        void IWebElement.SendKeys(string text)
        {
            Clear();
        }

        void IWebElement.Submit()
        {
            Submit();
        }

        void IWebElement.Click()
        {
            Click();
        }

        void IWebElement.Clear()
        {
            Clear();
        }

        public string GetAttribute(string attributeName)
        {
            return ExecuteFunc(() => Should.Be.Exist().Find().GetAttribute(attributeName));
        }


        public string GetProperty(string propertyName)
        {
            return ExecuteFunc(() => Should.Be.Exist().Find().GetProperty(propertyName));
        }

        public string GetCssValue(string propertyName)
        {
            return ExecuteFunc(() => Should.Be.Exist().Find().GetCssValue(propertyName));
        }

        public string Value => ExecuteFunc(() => Should.Be.Exist().Find().GetAttribute("value"));
        public string Style => ExecuteFunc(() => Should.Be.Exist().Find().GetAttribute("style")); 
        public string TagName => ExecuteFunc(() => Should.Be.Exist().Find().TagName);
        public string Text => ExecuteFunc(() => Should.Be.Exist().Find().Text);
        public bool Enabled => ExecuteFunc(() => Should.Be.Exist().Find().Enabled);
        public bool Selected => ExecuteFunc(() => Should.Be.Exist().Find().Selected); 
        public Point Location => ExecuteFunc(() => Should.Be.Exist().Find().Location); 
        public Size Size => ExecuteFunc(() => Should.Be.Exist().Find().Size);
        public bool Displayed => ExecuteFunc(() => Should.Be.Exist().Find().Displayed);

        private void ExecuteAction(Action action)
        {
            _automationManager
                .Wait(this)
                .Until(x =>
            {
                try
                {
                    action();
                }
                catch (StaleElementReferenceException e)
                {
                    Cache = _context.Find();
                    action();
                }
                return true;
            });
        }

        private T ExecuteFunc<T>(Func<T> func)
        {
            return _automationManager
                .Wait(this)
                .Until(x =>
                {
                    try
                    {
                        return func();
                    }
                    catch (StaleElementReferenceException e)
                    {
                        Cache = _context.Find();
                        return func();
                    }
                });
        }
    }
}
