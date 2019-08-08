using System;
using System.Collections.Generic;
using System.Text;

namespace Ratel.Web.Helpers
{
    public class ConditionBuilder
    {
        public string Description { get; private set; }
        public string ShortDescription { get; private set; }

        public bool ExpectedCondition { get; set; }

        public Func<object> Result { get; set; }

        public ConditionBuilder(string text)
        {
            Description = $"{Description} {text}";
            ShortDescription = $"{Description} {text}";
        }

        public ConditionBuilder(string description, string shortDescription)
        {
            Description = $"{Description} {description}";
            ShortDescription = $"{Description} {shortDescription}";
        }

        public ConditionBuilder Append(string text)
        {
            Description = $"{Description} {text}";
            ShortDescription = $"{Description} {text}";
            return this;
        }

        public ConditionBuilder AppendDescription(string text)
        {
            Description = $"{Description} {text}";
            return this;
        }

        public ConditionBuilder AppendShortDescription(string text)
        {
            ShortDescription = $"{Description} {text}";
            return this;
        }

        public ConditionBuilder SetExpectedCondition(bool condition)
        {
            ExpectedCondition = condition;
            return this;
        }

        public ConditionBuilder SetResultFunc(Func<object> result)
        {
            Result = result;
            return this;
        }
    }
}
