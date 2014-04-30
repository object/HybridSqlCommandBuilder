using System;

namespace SqlCommandFormatter
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