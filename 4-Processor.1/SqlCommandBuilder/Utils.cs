using System;
using System.Linq.Expressions;

namespace SqlCommandBuilder
{
    public static class Utils
    {
        public static T CastExpressionWithTypeCheck<T>(Expression expression) where T : Expression
        {
            var typedExpression = expression as T;
            if (typedExpression == null)
                throw NotSupportedExpression(expression);
            return typedExpression;
        }

        public static Exception NotSupportedExpression(Expression expression)
        {
            return new NotSupportedException(String.Format("Not supported expression of type {0} ({1}): {2}",
                expression.GetType(), expression.NodeType, expression));
        }
    }
}