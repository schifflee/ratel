using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using OpenQA.Selenium;

namespace Ratel.Web
{
    public abstract class SearchContext<T>
    {
        public string Name { get; set; }
        public abstract string Description { get; }
        public abstract T Find();

        protected SearchContext(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Description;
        }
    }

    internal class ElementFinder : SearchContext<IWebElement>
    {
        private readonly ISearchContext _context;
        private readonly By _locator;

        public ElementFinder(By locator, string name, ISearchContext context) : base(name)
        {
            _locator = locator;
            _context = context;
        }

        public override string Description => $"FindWithCache element '{Name}' by ({_context}).FindElement({_locator})";
        public override IWebElement Find() => _context.FindElement(_locator);
    }

    internal class ElementsFinder : SearchContext<ReadOnlyCollection<IWebElement>>
    {
        private readonly ISearchContext _context;
        private readonly By _locator;

        public ElementsFinder(By locator, string name, ISearchContext context) : base(name)
        {
            _locator = locator;
            _context = context;
        }

        public override string Description => $"FindWithCache elements '{Name}' by ({_context}).FindElements({_locator})";
        public override ReadOnlyCollection<IWebElement> Find() => _context.FindElements(_locator);
    }


    internal class ElementFinderFromCollectionByCondition : SearchContext<IWebElement>
    {
        private readonly RWebElementCollection _collection;
        private readonly string _conditionDescription;
        private readonly Func<RWebElementCollection, IWebElement> _condition;

        public ElementFinderFromCollectionByCondition(RWebElementCollection collection, Func<RWebElementCollection, IWebElement> condition, string conditionDescription, string name) : base(name)
        {
            _collection = collection;
            _condition = condition;
            _conditionDescription = conditionDescription;
        }

        public override string Description => $"FindWithCache element '{Name}' by ({_collection}).FindElement(by condition: {_conditionDescription})'";
        public override IWebElement Find() => _condition(_collection);
    }

    internal class ElementsFinderFromCollectionByCondition : SearchContext<ReadOnlyCollection<IWebElement>>
    {
        private readonly RWebElementCollection _collection;
        private readonly string _conditionDescription;
        private readonly Func<RWebElementCollection, ReadOnlyCollection<IWebElement>> _condition;

        public ElementsFinderFromCollectionByCondition(RWebElementCollection collection, Func<RWebElementCollection, ReadOnlyCollection<IWebElement>> condition, string conditionDescription, string name) : base(name)
        {
            _collection = collection;
            _condition = condition;
            _conditionDescription = conditionDescription;
        }

        public override string Description => $"FindWithCache elements '{Name}' by ({_collection}).FindElements(by condition: {_conditionDescription})'";
        public override ReadOnlyCollection<IWebElement> Find() => _condition(_collection);
    }
}
