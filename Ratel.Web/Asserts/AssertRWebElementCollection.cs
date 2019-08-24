using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Ratel.Web.Asserts;
using Ratel.Web.RWebElementsCollections;

namespace Ratel.Web
{
    public class AssertRWebElementCollection
    {
        private readonly RWebElementCollection _elementCollection;

        private readonly AutomationManager _automationManager;

        public string Name { get; set; }

        public AssertRWebElementCollection(RWebElementCollection elementCollection, AutomationManager automationManager)
        {
            _elementCollection = elementCollection;
            _automationManager = automationManager;
            Name = elementCollection.Name;
        }

        public RWebElementCollection Count(int count)
        {
            var actualCount = _elementCollection.Count;
            Assert.IsTrue(actualCount == count, $"Actual elements '{Name}' count: {actualCount}. Expected {count}");
            return _elementCollection;
        }
    }

    public class AssertRWebElementCollectionStrings
    {
        private readonly RWebElementCollection _elementCollection;

        private readonly AutomationManager _automationManager;

        public string Name { get; set; }
        public AssertRWebElementCollectionStrings(RWebElementCollection elementCollection, AutomationManager automationManager)
        {
            _elementCollection = elementCollection;
            _automationManager = automationManager;
            Name = elementCollection.Name;
        }
    }
}
