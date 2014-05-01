using System;

namespace SqlCommandBuilder
{
    public class CommandBuilder
    {
        public ICommandAppender<DataTable> From(string tableName)
        {
            return new CommandAppender<DataTable>(tableName);
        }

        public ICommandAppender<T> From<T>()
        {
            throw new NotImplementedException();
        }

        public ICommandAppender<DataTable> From(CommandExpression expression)
        {
            throw new NotImplementedException();
        }
    }
}