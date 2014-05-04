using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using SqlCommandBuilder.Dynamic;

namespace SqlCommandBuilder.Tests
{
    [TestFixture]
    public class DynamicTests : TestBase
    {
        protected override Command SelectAllCommand()
        {
            var x = DynamicCommand.Expression;
            return _commandBuilder
                .From(x.Companies)
                .Build();
        }

        protected override Command SelectAllWhereCommand()
        {
            var x = DynamicCommand.Expression;
            return _commandBuilder
                .From(x.Companies)
                .Where(x.CompanyName == "DynamicSoft")
                .Build();
        }

        protected override Command SelectAllWhereFunctionCommand()
        {
            var x = DynamicCommand.Expression;
            return _commandBuilder
                .From(x.Companies)
                .Where(x.CompanyName.Length() < 10 && x.CompanyName == x.CompanyName.ToUpper())
                .Build();
        }

        protected override Command SelectColumnsWhereCommand()
        {
            var x = DynamicCommand.Expression;
            return _commandBuilder
                .From(x.Companies)
                .Where(x.CompanyName == "DynamicSoft")
                .Select(x.CompanyName, x.Country, x.City)
                .Build();
        }

        protected override Command SelectAllWhereOrderByCommand()
        {
            var x = DynamicCommand.Expression;
            return _commandBuilder
                .From(x.Companies)
                .Where(x.CompanyName == "DynamicSoft")
                .OrderBy(x.Country)
                .Build();
        }

        protected override Command SelectColumnsWhereOrderByCommand()
        {
            var x = DynamicCommand.Expression;
            return _commandBuilder
                .From(x.Companies)
                .Where(x.YearEstablished > 2000 && x.NumberOfEmployees < 100)
                .Select(x.CompanyName, x.Country, x.City)
                .OrderBy(x.Country)
                .OrderByDescending(x.YearEstablished)
                .Build();
        }
    }
}