using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace Ratel.Web.Asserts
{
    public class StringConditions
    {
        private readonly Func<string> _value;

        private readonly bool _condition;

        private readonly RWebElement _rWebElement;

        private readonly AutomationManager _automationManager;

        private readonly string _description;


        public StringConditions(Func<string> value,  bool condition, string description, AutomationManager automationManager, RWebElement rWebElement)
        {
            _value = value;
            _automationManager = automationManager;
            _condition = condition;
            _description = description;
            _rWebElement = rWebElement;
        }

        public RWebElement AreEqual(string value)
        {
            GetStringCondition(AreEqualCondition, value, nameof(EndsWith));
            return _rWebElement;
        }

        public RWebElement Contains(string value)
        {
            GetStringCondition(ContainsCondition, value, nameof(EndsWith));
            return _rWebElement;
        }

        public RWebElement StartsWith(string value)
        {
            GetStringCondition(StartsWithCondition, value, nameof(EndsWith));
            return _rWebElement;
        }

        public RWebElement EndsWith(string value)
        {
            GetStringCondition(EndsWithCondition, value, nameof(EndsWith));
            return _rWebElement;
        }

        public RWebElement IsEmpty()
        {
            GetStringCondition(AreEqualCondition, string.Empty, nameof(IsEmpty));
            return _rWebElement;
        }

        private bool AreEqualCondition(string expected, string actual)
        {
            return (actual == expected) == _condition;
        }

        private bool ContainsCondition(string expected, string actual)
        {
            return actual.Contains(expected) == _condition;
        }

        private bool StartsWithCondition(string expected, string actual)
        {
            return actual.StartsWith(expected) == _condition;
        }

        private bool EndsWithCondition(string expected, string actual)
        {
            return actual.EndsWith(expected) == _condition;
        }

        private string GetDescription(string propertyName)
        {
            return $"{_description} {propertyName}";
        }

        private void GetStringCondition(Func<string, string, bool> condition, string expected, string propertyName)
        {
            var actual = string.Empty;
            try
            {
                _automationManager.Wait(_value).Until(x =>
                {
                     actual = x();
                    if (condition(actual, expected))
                    {
                        return true;
                    }
                    return false;
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new WebDriverTimeoutException($"Condition is not a true: {GetDescription(propertyName)} Expected: {expected}. Actual: {actual} ", e);
            }
        }
    }
}
