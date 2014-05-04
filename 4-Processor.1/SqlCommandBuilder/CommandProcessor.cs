using System;
using System.Collections.Generic;
using System.Linq;

namespace SqlCommandBuilder
{
    public abstract class CommandProcessor : ICommandProcessor
    {
        protected readonly Command _command;

        protected CommandProcessor(Command command)
        {
            _command = command;
        }

        public T FindOne<T>()
        {
            return FindAll<T>().FirstOrDefault();
        }

        public IDictionary<string, object> FindOne()
        {
            return FindOne<IDictionary<string, object>>();
        }

        public IEnumerable<T> FindAll<T>()
        {
            return Execute().ToObject<IEnumerable<T>>();
        }

        public IEnumerable<IDictionary<string, object>> FindAll()
        {
            return FindAll<IDictionary<string, object>>();
        }

        protected abstract IEnumerable<IDictionary<string, object>> Execute();
    }
}
