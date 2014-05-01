using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SqlCommandBuilder.Tests
{
    [TestFixture]
    public class TypedTests : TestBase
    {
        protected override Command SelectAllCommand()
        {
            return _commandBuilder
                .From<Companies>()
                .Build();
        }

        protected override Command SelectAllWhereCommand()
        {
            return _commandBuilder
                .From<Companies>()
                .Where(x => x.CompanyName == "DynamicSoft")
                .Build();
        }

        protected override Command SelectColumnsWhereCommand()
        {
            return _commandBuilder
                .From<Companies>()
                .Where(x => x.CompanyName == "DynamicSoft")
                .Select(x => new { x.CompanyName, x.Country, x.City})
                .Build();
        }

        protected override Command SelectAllWhereOrderByCommand()
        {
            return _commandBuilder.From<Companies>()
                .Where(x => x.CompanyName == "DynamicSoft")
                .OrderBy(x => x.Country)
                .Build();
        }

        protected override Command SelectColumnsWhereOrderByCommand()
        {
            return _commandBuilder.From<Companies>()
                .Where(x => x.YearEstablished > 2000 && x.NumberOfEmployees < 100)
                .Select(x => new {x.CompanyName, x.Country, x.City})
                .OrderBy(x => x.Country)
                .OrderByDescending(x => x.YearEstablished)
                .Build();
        }
    }
}
