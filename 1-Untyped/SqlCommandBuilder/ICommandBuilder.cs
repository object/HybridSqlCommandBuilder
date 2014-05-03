using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SqlCommandBuilder
{
    public interface ICommandBuilder
    {
        Command Build();

        ICommandBuilder Where(string condition);
        ICommandBuilder Select(params string[] columns);
        ICommandBuilder OrderBy(params string[] columns);
        ICommandBuilder OrderByDescending(params string[] columns);
    }
}
