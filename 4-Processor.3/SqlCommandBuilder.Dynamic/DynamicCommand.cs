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
    }
}