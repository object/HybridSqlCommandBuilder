using System;
using System.Collections.Generic;
using System.Linq;

namespace SqlCommandBuilder
{
    public abstract class CommandProcessor
    {
        protected readonly Command _command;

        internal static bool EnableDynamics { get; set; }

        protected CommandProcessor(Command command)
        {
            _command = command;
        }

        public T FindOne<T>() where T : class
        {
            return FindAll<T>().FirstOrDefault();
        }

        public ResultRow FindOne()
        {
            return FindOne<ResultRow>();
        }

        public ResultRow FindOne(CommandExpression expression)
        {
            return FindOne<ResultRow>();
        }

        public IEnumerable<T> FindAll<T>() where T : class
        {
            return Execute().AsEnumerable().Select(x => x.AsDictionary().ToObject<T>(EnableDynamics));
        }

        public IEnumerable<ResultRow> FindAll()
        {
            return FindAll<ResultRow>();
        }

        public ResultCollection FindAll(CommandExpression expression)
        {
            return Execute().ToEnumerable().ToObject<ResultCollection>(EnableDynamics);
        }

        protected abstract IEnumerable<ResultRow> Execute();
    }
}