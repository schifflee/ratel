﻿using System;

namespace Ratel.Web.Asserts
{
    public class AssertRWebElement
    {
        private readonly RWebElement _element;
        public AssertRWebElement(RWebElement element, AutomationManager automationManager)
        {
            _element = element;
        }

        public StringConditions Text 
            => new StringConditions(() => _element.Text, $"Text of {_element}");

        public StringConditions Value 
            => new StringConditions(() => _element.Value, $"Value of {_element}");


        public StringConditions Attribute(string attributeName) 
            => new StringConditions(() => _element.GetAttribute(attributeName), $"Attribute({attributeName}) of {_element}");

        public StringConditions Style
            => new StringConditions(() => _element.Style, $"Style of {_element}");

        public StringConditions TagName
            => new StringConditions(() => _element.TagName, $"TagName of {_element}");

        public StringConditions CssValue(string propertyName)
            => new StringConditions(() => _element.GetCssValue(propertyName), $"Attribute({propertyName}) of {_element}");

        public StringConditions Property(string propertyName)
            => new StringConditions(() => _element.GetProperty(propertyName), $"Attribute({propertyName}) of {_element}");

        public AssertConditions Displayed
            => new AssertConditions(() => _element.Displayed, $"{_element} Displayed");

        public AssertConditions Enabled
            => new AssertConditions(() => _element.Enabled, $"{_element} Enabled");

        public AssertConditions Exist
            => new AssertConditions(() => _element.Exist, $"{_element} Exist");

        public AssertConditions Selected
            => new AssertConditions(() => _element.Selected, $"{_element} Selected");
    }
}
