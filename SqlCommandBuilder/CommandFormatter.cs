using System;

namespace SqlCommandBuilder
{
    public class CommandFormatter
    {
        public IFluentCommand<DataTable> For(string tableName)
        {
            throw new NotImplementedException();
        }

        public IFluentCommand<T> For<T>()
        {
            throw new NotImplementedException();
        }

        public IFluentCommand<DataTable> For(CommandExpression expression)
        {
            throw new NotImplementedException();
        }
    }
}