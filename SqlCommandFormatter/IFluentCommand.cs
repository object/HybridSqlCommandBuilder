using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SqlCommandFormatter
{
    public interface IFluentCommand<T>
    {
        string Format();

        IFluentCommand<T> Where(string filter);
        IFluentCommand<T> Where(CommandExpression expression);
        IFluentCommand<T> Where(Expression<Func<T, bool>> expression);
        IFluentCommand<T> Skip(int count);
        IFluentCommand<T> Top(int count);
        IFluentCommand<T> Select(IEnumerable<string> columns);
        IFluentCommand<T> Select(params string[] columns);
        IFluentCommand<T> Select(params CommandExpression[] columns);
        IFluentCommand<T> Select(Expression<Func<T, object>> expression);
        IFluentCommand<T> OrderBy(IEnumerable<KeyValuePair<string, bool>> columns);
        IFluentCommand<T> OrderBy(params string[] columns);
        IFluentCommand<T> OrderBy(params CommandExpression[] columns);
        IFluentCommand<T> OrderBy(Expression<Func<T, object>> expression);
        IFluentCommand<T> OrderByDescending(params string[] columns);
        IFluentCommand<T> OrderByDescending(params CommandExpression[] columns);
        IFluentCommand<T> OrderByDescending(Expression<Func<T, object>> expression);
    }
}
