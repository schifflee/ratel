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
using Ratel.Web.Helpers;
using Ratel.Web.Shoulds;

namespace Ratel.Web
{
    public class RWebElement : IRWebElement
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly AutomationManager _automationManager;
        private readonly SearchContext<IWebElement> _context;

        public string Name { get; set; }
        internal IWebElement Cache { get; set; }

        public RWebElement(AutomationManager automationManager, By locator, string name)
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
        public Should Should 
            => new Should(new ConditionBuilder(ToString(), Name)
                .Append(nameof(Should)).SetExpectedCondition(true),
                this, _automationManager);
        public Should ShouldNot 
            => new Should(new ConditionBuilder(ToString(), Name)
                .Append(nameof(ShouldNot)).SetExpectedCondition(false),
                this, _automationManager);

        public RWebElement FindElement(By by, string name)
        {
            return new RWebElement(_automationManager, new ElementFinder(by, $"Child of {Name}", this));
        }

        public RWebElementCollection FindElements(By by, string name)
        {
            return new RWebElementCollection(_automationManager, new ElementsFinder(by, $"Children of {Name}", this));
        }

        public IWebElement Find()
        {
            Cache = _context.Find();
            return Cache;
        }

        public IWebElement FindWithCache() => Cache ?? Find();

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
            ExecuteAction(() => Should.Be.Clickable().FindWithCache().Clear());
            return this;
        }

        public RWebElement SendKeys(string text)
        {
            Logger.Info($"SendKeys '{text}' to '{Name}'");
            ExecuteAction(() => Should.Be.Clickable().FindWithCache().SendKeys(text));
            return this;
        }

        public RWebElement Submit()
        {
            Logger.Info($"Submit '{Name}'");
            ExecuteAction(() => Should.Be.Exist().FindWithCache().Submit());
            return this;
        }

        public RWebElement Click(int count)
        {
            for (var i = 0; i < count; i++)
            {
                Click(x => x.Clickable());
            }
            return this;
        }

        public RWebElement Click(int count, Func<Be, RWebElement> func)
        {
            for (var i = 0; i < count; i++)
            {
                Click(func);
            }
            return this;
        }

        public RWebElement Click()
        {
            Click(x => x.Clickable());
            return this;
        }

        public RWebElement Click(Func<Be, RWebElement> func)
        {
            Logger.Info($"Click '{Name}'");
            ExecuteAction(() => func(Should.Be).FindWithCache().Click());
            return this;
        }

        void IWebElement.SendKeys(string text)
        {
            _context.Find().SendKeys(text);
        }

        void IWebElement.Submit()
        {
            _context.Find().Submit();
        }

        void IWebElement.Click()
        {
            _context.Find().Click();
        }

        void IWebElement.Clear()
        {
            _context.Find().Clear();
        }

        public string GetAttribute(string attributeName)
        {
            return ExecuteFunc(() => Should.Be.Exist().FindWithCache().GetAttribute(attributeName));
        }


        public string GetProperty(string propertyName)
        {
            return ExecuteFunc(() => Should.Be.Exist().FindWithCache().GetProperty(propertyName));
        }

        public string GetCssValue(string propertyName)
        {
            return ExecuteFunc(() => Should.Be.Exist().FindWithCache().GetCssValue(propertyName));
        }

        public string Value => ExecuteFunc(() => Should.Be.Exist().FindWithCache().GetAttribute("value"));
        public string Style => ExecuteFunc(() => Should.Be.Exist().FindWithCache().GetAttribute("style")); 
        public string TagName => ExecuteFunc(() => Should.Be.Exist().FindWithCache().TagName);
        public string Text => ExecuteFunc(() => Should.Be.Exist().FindWithCache().Text);
        public bool Enabled => ExecuteFunc(() => Should.Be.Exist().FindWithCache().Enabled);
        public bool Selected => ExecuteFunc(() => Should.Be.Exist().FindWithCache().Selected); 
        public Point Location => ExecuteFunc(() => Should.Be.Exist().FindWithCache().Location); 
        public Size Size => ExecuteFunc(() => Should.Be.Exist().FindWithCache().Size);
        public bool Displayed => ExecuteFunc(() => Should.Be.Exist().FindWithCache().Displayed);

        private void ExecuteAction(Action action)
        {
            _automationManager
                .AnyWait(this)
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
                .AnyWait(this)
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
