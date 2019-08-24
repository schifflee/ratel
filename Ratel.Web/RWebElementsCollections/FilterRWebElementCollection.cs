using System;
using System.Collections.ObjectModel;
using System.Linq;
using OpenQA.Selenium;

namespace Ratel.Web.RWebElementsCollections
{
    public class FilterRWebElementCollection
    {
        private readonly RWebElementCollection _elementCollection;

        private readonly AutomationManager _automationManager;

        public string Name { get; set; }

        public FilterRWebElementCollection(RWebElementCollection elementCollection, AutomationManager automationManager)
        {
            _elementCollection = elementCollection;
            _automationManager = automationManager;
            Name = elementCollection.Name;
        }

        public StringFilterRWebElementCollection Text =>
            new StringFilterRWebElementCollection((func, funcName, text) =>
            {
                var condition = new ElementsFinderFromCollectionByCondition(_elementCollection, c =>
                        new ReadOnlyCollection<IWebElement>(c.FindAll()
                            .Where(x => func(x.Text)).ToList()),
                    $"Where(x => x.Text.{funcName}({text})", Name += $" Filtered By Text.{funcName}({text})");
                return new RWebElementCollection(_automationManager, condition);
            });

        public StringFilterRWebElementCollection Style =>
            new StringFilterRWebElementCollection((func, funcName, style) =>
            {
                var condition = new ElementsFinderFromCollectionByCondition(_elementCollection, c =>
                        new ReadOnlyCollection<IWebElement>(c.FindAll()
                            .Where(x => func(x.GetAttribute("style"))).ToList()),
                    $"Where(x => x.GetAttribute('style').{funcName}({style})", Name += $" Filtered By GetAttribute('style').{funcName}({style})");
                return new RWebElementCollection(_automationManager, condition);
            });

        public StringFilterRWebElementCollection Value =>
            new StringFilterRWebElementCollection((func, funcName, value) =>
            {
                var condition = new ElementsFinderFromCollectionByCondition(_elementCollection, c =>
                        new ReadOnlyCollection<IWebElement>(c.FindAll()
                            .Where(x => func(x.GetAttribute("value"))).ToList()),
                    $"Where(x => x.GetAttribute('value').{funcName}({value})", Name += $" Filtered By GetAttribute('value').{funcName}({value})");
                return new RWebElementCollection(_automationManager, condition);
            });

        public StringFilterRWebElementCollection Attribute(string attributeName)
        {
            return new StringFilterRWebElementCollection((func, funcName, text) =>
            {
                var condition = new ElementsFinderFromCollectionByCondition(_elementCollection, c =>
                        new ReadOnlyCollection<IWebElement>(c.FindAll()
                            .Where(x => func(x.GetAttribute(attributeName))).ToList()),
                    $"Where(x => x.GetAttribute({attributeName}).{funcName}({text})", Name += $" Filtered By GetAttribute({attributeName}).{funcName}({text})");
                return new RWebElementCollection(_automationManager, condition);
            });
        }

        public StringFilterRWebElementCollection Property(string propertyName)
        {
            return new StringFilterRWebElementCollection((func, funcName, text) =>
            {
                var condition = new ElementsFinderFromCollectionByCondition(_elementCollection, c =>
                        new ReadOnlyCollection<IWebElement>(c.FindAll()
                            .Where(x => func(x.GetProperty(propertyName))).ToList()),
                    $"Where(x => x.GetProperty({propertyName}).{funcName}({text})", Name += $" Filtered By GetProperty({propertyName}).{funcName}({text})");
                return new RWebElementCollection(_automationManager, condition);
            });
        }

        public StringFilterRWebElementCollection CssValue(string propertyName)
        {
            return new StringFilterRWebElementCollection((func, funcName, text) =>
            {
                var condition = new ElementsFinderFromCollectionByCondition(_elementCollection, c =>
                        new ReadOnlyCollection<IWebElement>(c.FindAll()
                            .Where(x => func(x.GetCssValue(propertyName))).ToList()),
                    $"Where(x => x.GetCssValue({propertyName}).{funcName}({text})", Name += $" Filtered By GetCssValue({propertyName}).{funcName}({text})");
                return new RWebElementCollection(_automationManager, condition);
            });
        }

        public StringFilterRWebElementCollection TagName =>
            new StringFilterRWebElementCollection((func, funcName, tagName) =>
            {
                var condition = new ElementsFinderFromCollectionByCondition(_elementCollection, c =>
                        new ReadOnlyCollection<IWebElement>(c.FindAll()
                            .Where(x => func(x.TagName)).ToList()),
                    $"Where(x => x.TagName.{funcName}({tagName})", Name += $" Filtered By TagName.{funcName}({tagName})");
                return new RWebElementCollection(_automationManager, condition);
            });

        public RWebElementCollection Displayed(bool status)
        {
            var condition = new ElementsFinderFromCollectionByCondition(_elementCollection,
                c => new ReadOnlyCollection<IWebElement>(c.FindAll().Where(x => x.Displayed == status).ToList()),
                $"Where(x => x.Displayed == {status}", Name += $" Filtered By Displayed == {status}");
            return new RWebElementCollection(_automationManager, condition);
        }

        public RWebElementCollection Selected(bool status)
        {
            var condition = new ElementsFinderFromCollectionByCondition(_elementCollection,
                c => new ReadOnlyCollection<IWebElement>(c.FindAll().Where(x => x.Selected == status).ToList()),
                $"Where(x => x.Selected == {status}", Name += $" Filtered By Selected == {status}");
            return new RWebElementCollection(_automationManager, condition);
        }

        public RWebElementCollection Enabled(bool status)
        {
            var condition = new ElementsFinderFromCollectionByCondition(_elementCollection,
                c => new ReadOnlyCollection<IWebElement>(c.FindAll().Where(x => x.Enabled == status).ToList()),
                $"Where(x => x.Enabled == {status}", Name += $" Filtered By Enabled == {status}");
            return new RWebElementCollection(_automationManager, condition);
        }
    }

    public class StringFilterRWebElementCollection
    {
        private readonly Func<Func<string, bool>, string, string, RWebElementCollection> _func;

        public StringFilterRWebElementCollection(Func<Func<string, bool>, string, string, RWebElementCollection> func)
        {
            _func = func;
        }

        public RWebElementCollection EndsWith(string text)
        {
            return _func(x => x.EndsWith(text), nameof(EndsWith), text);
        }

        public RWebElementCollection StartWith(string text)
        {
            return _func(x => x.StartsWith(text), nameof(StartWith), text);
        }

        public RWebElementCollection Equals(string text)
        {
            return _func(x => x.Equals(text), nameof(Equals), text);
        }

        public RWebElementCollection Contains(string text)
        {
            return _func(x => x.Contains(text), nameof(Contains), text);
        }
    }
}
