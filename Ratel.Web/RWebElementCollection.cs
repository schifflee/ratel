using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using OpenQA.Selenium;

namespace Ratel.Web
{
    public class RWebElementCollection : IReadOnlyList<RWebElement>
    {
        private readonly SearchContext<ReadOnlyCollection<IWebElement>> _context;
        private ReadOnlyCollection<IWebElement> _elements;
        private readonly AutomationManager _automationManager;
        public string Name { get; set; }
        public RWebElementCollection(AutomationManager automationManager, By locator, [CallerMemberName]string name = "")
        {
            Name = name;
            _context = new ElementsFinder(locator, name, automationManager.Driver);
            _automationManager = automationManager;
        }

        public RWebElementCollection(AutomationManager automationManager, SearchContext<ReadOnlyCollection<IWebElement>> context)
        {
            _context = context;
            _automationManager = automationManager;
        }

        public override string ToString()
        {
            return _context.ToString();
        }

        public RWebElementCollection ForcedSearch()
        {
            _elements = _context.Find();
            return this;
        }

        public RWebElementCollection WaitNotZero(bool throwEx = false)
        {
            try
            {
                _automationManager.Wait(this).Until(x => x.Count > 0);
            }
            catch (Exception e)
            {
                if (throwEx)
                {
                    throw;
                }
            }
            return this;
        }

        public ReadOnlyCollection<IWebElement> FindAll()
        {
            if (_elements == null)
            {
                _elements = _context.Find();
            }
            try
            {
                var staleCheck = _elements.Count > 0 ? _elements[0].TagName : string.Empty;
                return _elements;
            }
            catch (StaleElementReferenceException)
            {
                _elements = _context.Find();
                return _elements;
            }
        }

        public AssertRWebElementCollection Assert => new AssertRWebElementCollection();

        public IEnumerator<RWebElement> GetEnumerator()
        {
            return new List<RWebElement>(FindAll().Select((el, index) 
                => new RWebElement(_automationManager, new ElementFinderFromCollectionByCondition(this, x => x.FindAll()[index], $"x.FindAll()[{index}]", Name)))).GetEnumerator();
        }

        public RWebElementCollection WhereTextAreEqual(string text)
        {
            return new RWebElementCollection(_automationManager,
                new ElementsFinderFromCollectionByCondition(this, c => new ReadOnlyCollection<IWebElement>(c.FindAll()
                    .Where(x => x.Text.Equals(text)).ToList()), $"Where(x => x.Text.Equals({text})", Name));
        }

        public RWebElementCollection WhereTextContain(string text)
        {
            return new RWebElementCollection(_automationManager,
                new ElementsFinderFromCollectionByCondition(this, c => new ReadOnlyCollection<IWebElement>(c.FindAll()
                    .Where(x => x.Text.Contains(text)).ToList()), $"Where(x => x.Text.Contains({text})", Name));
        }

        public RWebElementCollection WhereTextStartsWith(string text)
        {
            return new RWebElementCollection(_automationManager,
                new ElementsFinderFromCollectionByCondition(this, c => new ReadOnlyCollection<IWebElement>(c.FindAll()
                    .Where(x => x.Text.StartsWith(text)).ToList()), $"Where(x => x.Text.EndsWith({text})", Name));
        }

        public RWebElementCollection WhereTextEndsWith(string text)
        {
            return new RWebElementCollection(_automationManager,
                new ElementsFinderFromCollectionByCondition(this, c => new ReadOnlyCollection<IWebElement>(c.FindAll()
                    .Where(x => x.Text.EndsWith(text)).ToList()), $"Where(x => x.Text.EndsWith({text})", Name));
        }

        public RWebElementCollection WhereValueAreEqual(string text)
        {
            return new RWebElementCollection(_automationManager,
                new ElementsFinderFromCollectionByCondition(this, c => new ReadOnlyCollection<IWebElement>(c.FindAll()
                    .Where(x => x.GetAttribute("value").Equals(text)).ToList()), $"Where(x => x.Value.Equals({text})", Name));
        }

        public RWebElementCollection WhereValueContain(string text)
        {
            return new RWebElementCollection(_automationManager,
                new ElementsFinderFromCollectionByCondition(this, c => new ReadOnlyCollection<IWebElement>(c.FindAll()
                    .Where(x => x.GetAttribute("value").Contains(text)).ToList()), $"Where(x => x.Value.Contains({text})", Name));
        }

        public RWebElementCollection WhereValueStartsWith(string text)
        {
            return new RWebElementCollection(_automationManager,
                new ElementsFinderFromCollectionByCondition(this, c => new ReadOnlyCollection<IWebElement>(c.FindAll()
                    .Where(x => x.GetAttribute("value").StartsWith(text)).ToList()), $"Where(x => x.Value.EndsWith({text})", Name));
        }

        public RWebElementCollection WhereValueEndsWith(string text)
        {
            return new RWebElementCollection(_automationManager,
                new ElementsFinderFromCollectionByCondition(this, c => new ReadOnlyCollection<IWebElement>(c.FindAll()
                    .Where(x => x.GetAttribute("value").EndsWith(text)).ToList()), $"Where(x => x.Value.EndsWith({text})", Name));
        }


        public RWebElementCollection WhereAttributeAreEqual(string attributeName, string text)
        {
            return new RWebElementCollection(_automationManager,
                new ElementsFinderFromCollectionByCondition(this, c => new ReadOnlyCollection<IWebElement>(c.FindAll()
                    .Where(x => x.GetAttribute(attributeName).Equals(text)).ToList()), $"Where(x => x.GetAttribute({attributeName}).Equals({text})", Name));
        }

        public RWebElementCollection WhereAttributeContain(string attributeName, string text)
        {
            return new RWebElementCollection(_automationManager,
                new ElementsFinderFromCollectionByCondition(this, c => new ReadOnlyCollection<IWebElement>(c.FindAll()
                    .Where(x => x.GetAttribute(attributeName).Contains(text)).ToList()), $"Where(x => x.GetAttribute({attributeName}).Contains({text})", Name));
        }

        public RWebElementCollection WhereAttributeStartsWith(string attributeName, string text)
        {
            return new RWebElementCollection(_automationManager,
                new ElementsFinderFromCollectionByCondition(this, c => new ReadOnlyCollection<IWebElement>(c.FindAll()
                    .Where(x => x.GetAttribute(attributeName).StartsWith(text)).ToList()), $"Where(x => x.GetAttribute({attributeName}).EndsWith({text})", Name));
        }

        public RWebElementCollection WhereAttributeEndsWith(string attributeName, string text)
        {
            return new RWebElementCollection(_automationManager,
                new ElementsFinderFromCollectionByCondition(this, c => new ReadOnlyCollection<IWebElement>(c.FindAll()
                    .Where(x => x.GetAttribute(attributeName).EndsWith(text)).ToList()), $"Where(x => x.GetAttribute({attributeName}).EndsWith({text})", Name));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => FindAll().Count;

        public RWebElement this[int index] 
            => new RWebElement(_automationManager, new ElementFinderFromCollectionByCondition(this, x =>
            {
                var els = x.FindAll();
                if (els.Count < index)
                {
                    throw new NoSuchElementException($"Find element '{Name}' by ({_context}).FindElement(By Index{index}) out of range '{els.Count}'");
                }
                return x.FindAll()[index];
            }, $"FindAll()[{index}]", Name)); 
    }
}
