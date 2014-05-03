using System.Collections.Generic;

namespace SqlCommandBuilder.Dynamic
{
    public class DynamicCommand
    {
        static DynamicCommand()
        {
            DictionaryExtensions.CreateDynamicResultRow = (x) => new DynamicResultRow(x);
            EnumerableExtensions.CreateDynamicResultCollection = (x) => new DynamicResultCollection(x);
            CommandProcessor.EnableDynamics = true;
        }

        public static dynamic Expression
        {
            get { return new DynamicCommandExpression(); }
        }

        public static CommandExpression ExpressionFromReference(string reference)
        {
            return DynamicCommandExpression.FromReference(reference);
        }

        public static CommandExpression ExpressionFromValue(object value)
        {
            return DynamicCommandExpression.FromValue(value);
        }

        public static CommandExpression ExpressionFromFunction(string functionName, string targetName, IEnumerable<object> arguments)
        {
            return DynamicCommandExpression.FromFunction(functionName, targetName, arguments);
        }
    }
}