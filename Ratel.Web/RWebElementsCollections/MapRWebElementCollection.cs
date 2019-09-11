using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ratel.Web.RWebElementsCollections
{
    public class MapRWebElementCollection
    {
        private readonly RWebElementCollection _elementCollection;

        private readonly AutomationManager _automationManager;


        public MapRWebElementCollection(RWebElementCollection elementCollection, AutomationManager automationManager)
        {
            _elementCollection = elementCollection;
            _automationManager = automationManager;
        }

        public List<string> Text => _elementCollection.Select(x => x.Text).ToList();
        public List<string> TagName => _elementCollection.Select(x => x.TagName).ToList();

        public List<string> Value => _elementCollection.Select(x => x.GetAttribute("value")).ToList();

        public List<string> Style => _elementCollection.Select(x => x.GetAttribute("style")).ToList();

        public List<string> Attribute(string attributeName) 
            => _elementCollection.Select(x => x.GetAttribute(attributeName)).ToList();

        public List<string> CssValue(string propertyName) 
            => _elementCollection.Select(x => x.GetCssValue(propertyName)).ToList();
        public List<string> Property(string propertyName) 
            => _elementCollection.Select(x => x.GetProperty(propertyName)).ToList();

        public List<bool> Displayed => _elementCollection.Select(x => x.Displayed).ToList();

        public List<bool> Enabled => _elementCollection.Select(x => x.Enabled).ToList();

        public List<bool> Selected => _elementCollection.Select(x => x.Selected).ToList();

    }
}
