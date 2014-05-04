using System.Collections.Generic;

namespace SqlCommandBuilder
{
    public interface ICommandProcessor
    {
        T FindOne<T>();
        IDictionary<string, object> FindOne();
        IEnumerable<T> FindAll<T>();
        IEnumerable<IDictionary<string, object>> FindAll();
    }
}