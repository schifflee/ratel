using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using OpenQA.Selenium;

namespace Ratel.Web.RWebElementsCollections
{
    public class RWebElementCollection : IReadOnlyList<RWebElement>
    {
        private readonly BaseElementFinder<ReadOnlyCollection<IWebElement>> _context;
        private ReadOnlyCollection<IWebElement> _elements;
        private readonly AutomationManager _automationManager;
        public string Name { get; set; }
        public RWebElementCollection(AutomationManager automationManager, By locator, [CallerMemberName]string name = "")
        {
            Name = name;
            _context = new ElementsFinder(locator, name, automationManager.Driver);
            _automationManager = automationManager;
        }

        public RWebElementCollection(AutomationManager automationManager, BaseElementFinder<ReadOnlyCollection<IWebElement>> context)
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
                _automationManager.DefaultWait(this).Until(x => x.Count > 0);
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

        public IEnumerator<RWebElement> GetEnumerator()
        {
            return new List<RWebElement>(FindAll().Select((el, index) 
                => new RWebElement(_automationManager, new ElementFinderFromCollectionByCondition(this, x => x.FindAll()[index], $"x.FindAll()[{index}]", Name)))).GetEnumerator();
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
                    throw new NoSuchElementException($"Element '{Name}' By: ({_context}).FindElement(By Index{index}) out of range '{els.Count}'");
                }
                return x.FindAll()[index];
            }, $"FindAll()[{index}]", Name));

        public FilterRWebElementCollection Filter => new FilterRWebElementCollection(this, _automationManager);

        public MapRWebElementCollection Map => new MapRWebElementCollection(this, _automationManager);
        public AssertRWebElementCollection Assert => new AssertRWebElementCollection(this, _automationManager);
    }
}
