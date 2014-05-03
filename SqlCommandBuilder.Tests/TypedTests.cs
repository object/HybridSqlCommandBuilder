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

        protected override Command SelectAllWhereFunctionCommand()
        {
            return _commandBuilder
                .From<Companies>()
                .Where(x => x.CompanyName.Length < 10 && x.CompanyName == x.CompanyName.ToUpper())
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

        [Test]
        public void ExecuteFindOne()
        {
            var command = SelectAllCommand();
            var commandProcessor = new DummyCommandProcessor(command);
            var result = commandProcessor.FindOne<Companies>();
            Assert.AreEqual("DynamicSoft", result.CompanyName);
        }

        [Test]
        public void ExecuteFindAll()
        {
            var command = SelectAllCommand();
            var commandProcessor = new DummyCommandProcessor(command);
            var result = commandProcessor.FindAll<Companies>();
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("DynamicSoft", result.First().CompanyName);
            Assert.AreEqual("StaticSoft", result.Last().CompanyName);
        }
    }
}
