using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SqlCommandBuilder
{
    public class CommandBuilder
    {
        public ICommandBuilder<Table> From(string tableName)
        {
            return new CommandBuilder<Table>(tableName);
        }

        public ICommandBuilder<T> From<T>()
        {
            throw new NotImplementedException();
        }

        public ICommandBuilder<Table> From(CommandExpression expression)
        {
            throw new NotImplementedException();
        }
    }

    class CommandBuilder<T> : ICommandBuilder<T>
    {
        private readonly Command _command = new Command();

        public CommandBuilder(string tableName)
        {
            _command.From(tableName);
        }

        public Command Build()
        {
            return _command;
        }

        public ICommandBuilder<T> Where(string condition)
        {
            _command.Where(condition);
            return this;
        }

        public ICommandBuilder<T> Where(CommandExpression expression)
        {
            throw new NotImplementedException();
        }

        public ICommandBuilder<T> Where(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public ICommandBuilder<T> Select(IEnumerable<string> columns)
        {
            _command.Select(columns);
            return this;
        }

        public ICommandBuilder<T> Select(params string[] columns)
        {
            _command.Select(columns);
            return this;
        }

        public ICommandBuilder<T> Select(params CommandExpression[] columns)
        {
            throw new NotImplementedException();
        }

        public ICommandBuilder<T> Select(Expression<Func<T, object>> expression)
        {
            throw new NotImplementedException();
        }

        public ICommandBuilder<T> OrderBy(params string[] columns)
        {
            _command.OrderBy(columns);
            return this;
        }

        public ICommandBuilder<T> OrderBy(params CommandExpression[] columns)
        {
            throw new NotImplementedException();
        }

        public ICommandBuilder<T> OrderBy(Expression<Func<T, object>> expression)
        {
            throw new NotImplementedException();
        }

        public ICommandBuilder<T> OrderByDescending(params string[] columns)
        {
            _command.OrderByDescending(columns);
            return this;
        }

        public ICommandBuilder<T> OrderByDescending(params CommandExpression[] columns)
        {
            throw new NotImplementedException();
        }

        public ICommandBuilder<T> OrderByDescending(Expression<Func<T, object>> expression)
        {
            throw new NotImplementedException();
        }
    }
}