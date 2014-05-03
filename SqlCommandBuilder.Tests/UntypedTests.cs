using System.Linq;
using NUnit.Framework;

namespace SqlCommandBuilder.Tests
{
    [TestFixture]
    public class UntypedTests : TestBase
    {
        protected override Command SelectAllCommand()
        {
            return _commandBuilder
                .From("Companies")
                .Build();
        }

        protected override Command SelectAllWhereCommand()
        {
            return _commandBuilder
                .From("Companies")
                .Where("CompanyName='DynamicSoft'")
                .Build();
        }

        protected override Command SelectAllWhereFunctionCommand()
        {
            return _commandBuilder
                .From("Companies")
                .Where("LEN(CompanyName)<10 AND CompanyName=UPPER(CompanyName)")
                .Build();
        }

        protected override Command SelectColumnsWhereCommand()
        {
            return _commandBuilder
                .From("Companies")
                .Where("CompanyName='DynamicSoft'")
                .Select(new [] {"CompanyName","Country","City"})
                .Build();
        }

        protected override Command SelectAllWhereOrderByCommand()
        {
            return _commandBuilder
                .From("Companies")
                .Where("CompanyName='DynamicSoft'")
                .OrderBy("Country")
                .Build();
        }

        protected override Command SelectColumnsWhereOrderByCommand()
        {
            return _commandBuilder
                .From("Companies")
                .Where("YearEstablished>2000 AND NumberOfEmployees<100")
                .Select(new[] { "CompanyName", "Country", "City" })
                .OrderBy("Country")
                .OrderByDescending("YearEstablished")
                .Build();
        }

        [Test]
        public void ExecuteFindOne()
        {
            var command = SelectAllCommand();
            var commandProcessor = new FakeCommandProcessor(command);
            var result = commandProcessor.FindOne();
            Assert.AreEqual("DynamicSoft", result["CompanyName"]);
        }

        [Test]
        public void ExecuteFindAll()
        {
            var command = SelectAllCommand();
            var commandProcessor = new FakeCommandProcessor(command);
            var result = commandProcessor.FindAll();
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("DynamicSoft", result.First()["CompanyName"]);
            Assert.AreEqual("StaticSoft", result.Last()["CompanyName"]);
        }
    }
}