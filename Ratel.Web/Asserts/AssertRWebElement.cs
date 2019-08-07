using System;

namespace Ratel.Web.Asserts
{
    public class AssertRWebElement
    {
        private readonly RWebElement _element;
        public AssertRWebElement(RWebElement element, AutomationManager automationManager)
        {
            _element = element;
        }

        public AssertCondition Text 
            => new AssertCondition(() => _element.Text, $"Text of {_element}");

        public AssertCondition Value 
            => new AssertCondition(() => _element.Value, $"Value of {_element}");


        public AssertCondition Attribute(string attributeName) 
            => new AssertCondition(() => _element.GetAttribute(attributeName), $"Attribute({attributeName}) of {_element}");

        public AssertCondition Style
            => new AssertCondition(() => _element.Style, $"Style of {_element}");

        public AssertCondition TagName
            => new AssertCondition(() => _element.TagName, $"TagName of {_element}");

        public AssertCondition CssValue(string propertyName)
            => new AssertCondition(() => _element.GetCssValue(propertyName), $"Attribute({propertyName}) of {_element}");

        public AssertCondition Property(string propertyName)
            => new AssertCondition(() => _element.GetProperty(propertyName), $"Attribute({propertyName}) of {_element}");
    }

    public class AssertCondition
    {
        private readonly Func<string> _value;
        private readonly string _description;

        public AssertCondition(Func<string> value, string description)
        {
            _value = value;
            _description = description;
        }

        public StringAsserts Is
            => new StringAsserts(_value, _description, true);

        public StringAsserts IsNot
            => new StringAsserts(_value, _description, false);
    }
}
