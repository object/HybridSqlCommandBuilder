using System.Collections.Generic;

namespace SqlCommandBuilder
{
    public interface ICommandProcessor
    {
        T FindOne<T>() where T : class;
        ResultRow FindOne();
        ResultRow FindOne(CommandExpression expression);
        IEnumerable<T> FindAll<T>() where T : class;
        IEnumerable<ResultRow> FindAll();
        ResultCollection FindAll(CommandExpression expression);
    }
}