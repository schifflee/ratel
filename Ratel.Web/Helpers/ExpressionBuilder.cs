using System;
using System.Collections.Generic;
using System.Text;
using Ratel.Web.Models;

namespace Ratel.Web.Helpers
{
    public abstract class ExpressionBuilderBase<T> where  T : ExpressionBuilderBase<T>
    {
        public ConditionModel Condition { get; set; }

        protected ExpressionBuilderBase()
        {
            if (Condition == null)
            {
                Condition = new ConditionModel();
            } 
        }

        public T Append(string text)
        {
            Condition.Description = $"{Condition.Description} {text}";
            Condition.ShortDescription = $"{Condition.ShortDescription} {text}";
            return (T)this;
        }

        public T AppendDescription(string text)
        {
            Condition.Description = $"{Condition.Description} {text}";
            return (T)this;
        }

        public T AppendShortDescription(string text)
        {
            Condition.ShortDescription = $"{Condition.ShortDescription} {text}";
            return (T)this;
        }

        public T SetOperator(bool @operator)
        {
            Condition.Operator = @operator;
            return (T)this;
        }
    }

    public class ExpressionBuilder : ExpressionBuilderBase<ExpressionBuilder>
    {

        public ExpressionBuilder(string text)
        {
            Condition.Description = $"{Condition.Description} {text}";
            Condition.ShortDescription = $"{Condition.ShortDescription} {text}";
        }

        public ExpressionBuilder(string description, string shortDescription)
        {
            Condition.Description = $"{Condition.Description} {description}";
            Condition.ShortDescription = $"{Condition.ShortDescription} {shortDescription}";
        }

        public ExpressionBuilder<T> SetCondition<T>(Func<T> result)
        {
            return new ExpressionBuilder<T>(result, Condition);
        }
    }

    public class ExpressionBuilder<T> : ExpressionBuilderBase<ExpressionBuilder<T>>
    {
        public ExpressionBuilder(Func<T> expression, ConditionModel condition)
        {
            Expression = expression;
            Condition = condition;
        }

        public ExpressionBuilder(Func<T> expression, string text)
        {
            Condition.Description = $"{Condition.Description} {text}";
            Condition.ShortDescription = $"{Condition.ShortDescription} {text}";
        }

        public ExpressionBuilder(Func<T> expression, string description, string shortDescription)
        {
            Condition.Description = $"{Condition.Description} {description}";
            Condition.ShortDescription = $"{Condition.ShortDescription} {shortDescription}";
        }

        public Func<T> Expression { get; set; }
    }
}
