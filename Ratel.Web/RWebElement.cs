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
            return _context.Find();
        }

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


        public IWebElement GetElementExist()
        {
            var element = _automationManager.Wait(_context).Until(x => _context.Find());
            return element;
        }

        public IWebElement GetElementVisible()
        {
            _automationManager.Wait(_context).Until(x => _context.Find().Displayed);
            return _context.Find();
        }

        public IWebElement GetElementClickable()
        {
            _automationManager.Wait(_context).Until(x =>
            {
                var element = x.Find();
                return element.Displayed && Enabled;
            });
            return _context.Find();
        }


        public void Clear()
        {
            Logger.Info($"Clear '{Name}'");
            ExecuteAction(() => GetElementClickable().Clear());
        }

        public void SendKeys(string text)
        {
            Logger.Info($"SendKeys '{text}' to '{Name}'");
            ExecuteAction(() => GetElementClickable().SendKeys(text));
        }

        public void Submit()
        {
            Logger.Info($"Submit '{Name}'");
            ExecuteAction(() => GetElementExist().Submit());
        }

        public void Click()
        {
            Logger.Info($"Click '{Name}'");
            ExecuteAction(() => GetElementClickable().Click());
        }

        public string GetAttribute(string attributeName)
        {
            return ExecuteFunc(() => GetElementExist().GetAttribute(attributeName));
        }


        public string GetProperty(string propertyName)
        {
            return ExecuteFunc(() => GetElementExist().GetProperty(propertyName));
        }

        public string GetCssValue(string propertyName)
        {
            return ExecuteFunc(() => GetElementExist().GetCssValue(propertyName));
        }

        public string Value => GetElementExist().GetAttribute("value");
        public string Style => GetElementExist().GetAttribute("style");
        public string TagName => GetElementExist().TagName;
        public string Text => GetElementExist().Text;
        public bool Enabled => GetElementExist().Enabled;
        public bool Selected => GetElementExist().Selected;
        public Point Location => ExecuteFunc(() => GetElementExist().Location); 
        public Size Size => ExecuteFunc(() => GetElementExist().Size);
        public bool Displayed => ExecuteFunc(() => GetElementExist().Displayed);

        private void ExecuteAction(Action action)
        {
            _automationManager.Wait(this).Until(x =>
            {
                action();
                return true;
            });
        }

        private T ExecuteFunc<T>(Func<T> func)
        {
            return _automationManager.Wait(this).Until(x => func());
        }
    }
}
