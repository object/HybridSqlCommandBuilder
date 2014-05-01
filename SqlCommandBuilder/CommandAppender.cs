using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SqlCommandBuilder
{
    class CommandAppender<T> : ICommandAppender<T>
    {
        private readonly Command _command = new Command();

        public CommandAppender(string tableName)
        {
            _command.From(tableName);
        }

        public Command Build()
        {
            return _command;
        }

        public ICommandAppender<T> Where(string condition)
        {
            _command.Where(condition);
            return this;
        }

        public ICommandAppender<T> Where(CommandExpression expression)
        {
            throw new NotImplementedException();
        }

        public ICommandAppender<T> Where(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ICommandAppender<T> Select(IEnumerable<string> columns)
        {
            _command.Select(columns);
            return this;
        }

        public ICommandAppender<T> Select(params string[] columns)
        {
            _command.Select(columns);
            return this;
        }

        public ICommandAppender<T> Select(params CommandExpression[] columns)
        {
            throw new NotImplementedException();
        }

        public ICommandAppender<T> Select(Expression<Func<T, object>> expression)
        {
            throw new NotImplementedException();
        }

        public ICommandAppender<T> OrderBy(params string[] columns)
        {
            _command.OrderBy(columns);
            return this;
        }

        public ICommandAppender<T> OrderBy(params CommandExpression[] columns)
        {
            throw new NotImplementedException();
        }

        public ICommandAppender<T> OrderBy(Expression<Func<T, object>> expression)
        {
            throw new NotImplementedException();
        }

        public ICommandAppender<T> OrderByDescending(params string[] columns)
        {
            _command.OrderByDescending(columns);
            return this;
        }

        public ICommandAppender<T> OrderByDescending(params CommandExpression[] columns)
        {
            throw new NotImplementedException();
        }

        public ICommandAppender<T> OrderByDescending(Expression<Func<T, object>> expression)
        {
            throw new NotImplementedException();
        }
    }
}