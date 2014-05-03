using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SqlCommandBuilder
{
    public class CommandBuilder : ICommandBuilder
    {
        private readonly Command _command = new Command();

        public Command Build()
        {
            return _command;
        }

        public ICommandBuilder From(string tableName)
        {
            _command.From(tableName);
            return this;
        }

        public ICommandBuilder Where(string condition)
        {
            _command.Where(condition);
            return this;
        }

        public ICommandBuilder Select(params string[] columns)
        {
            _command.Select(columns);
            return this;
        }

        public ICommandBuilder OrderBy(params string[] columns)
        {
            _command.OrderBy(columns);
            return this;
        }

        public ICommandBuilder OrderByDescending(params string[] columns)
        {
            _command.OrderByDescending(columns);
            return this;
        }
    }
}