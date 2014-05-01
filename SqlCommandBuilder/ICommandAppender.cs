using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SqlCommandBuilder
{
    public interface ICommandAppender<T>
    {
        Command Build();

        ICommandAppender<T> Where(string condition);
        ICommandAppender<T> Where(CommandExpression expression);
        ICommandAppender<T> Where(Expression<Func<T, bool>> expression);
        ICommandAppender<T> Select(IEnumerable<string> columns);
        ICommandAppender<T> Select(params string[] columns);
        ICommandAppender<T> Select(params CommandExpression[] columns);
        ICommandAppender<T> Select(Expression<Func<T, object>> expression);
        ICommandAppender<T> OrderBy(params string[] columns);
        ICommandAppender<T> OrderBy(params CommandExpression[] columns);
        ICommandAppender<T> OrderBy(Expression<Func<T, object>> expression);
        ICommandAppender<T> OrderByDescending(params string[] columns);
        ICommandAppender<T> OrderByDescending(params CommandExpression[] columns);
        ICommandAppender<T> OrderByDescending(Expression<Func<T, object>> expression);
    }
}
