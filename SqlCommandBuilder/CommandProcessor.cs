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

        public Record FindOne()
        {
            return FindOne<Record>();
        }

        public Record FindOne(CommandExpression expression)
        {
            return FindOne<Record>();
        }

        public IEnumerable<T> FindAll<T>() where T : class
        {
            return Execute().Select(x => x.AsDictionary().ToObject<T>(EnableDynamics));
        }

        public IEnumerable<Record> FindAll()
        {
            return FindAll<Record>();
        }

        public IEnumerable<Record> FindAll(CommandExpression expression)
        {
            return FindAll<Record>();
        }

        protected abstract IEnumerable<Record> Execute();
    }
}