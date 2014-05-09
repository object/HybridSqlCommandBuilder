using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using SqlCommandBuilder.Dynamic;

namespace SqlCommandBuilder.Tests
{
    [TestClass]
    public class DynamicTests : TestBase
    {
        protected override Command SelectAllCommand()
        {
            dynamic x = new DynamicCommandExpression();
            return (_commandBuilder
                .From(x.Companies) as ICommandBuilder<ResultRow>)
                .Build();
        }

        protected override Command SelectAllWhereCommand()
        {
            dynamic x = new DynamicCommandExpression();
            return ((_commandBuilder
                .From(x.Companies) as ICommandBuilder<ResultRow>)
                .Where(x.CompanyName == "DynamicSoft") as ICommandBuilder<ResultRow>)
                .Build();
        }

        protected override Command SelectAllWhereFunctionCommand()
        {
            dynamic x = new DynamicCommandExpression();
            return ((_commandBuilder
                .From(x.Companies) as ICommandBuilder<ResultRow>)
                .Where(x.CompanyName.Length() < 10 && x.CompanyName == x.CompanyName.ToUpper()) as ICommandBuilder<ResultRow>)
                .Build();
        }

        protected override Command SelectColumnsWhereCommand()
        {
            dynamic x = new DynamicCommandExpression();
            return (((_commandBuilder
                .From(x.Companies) as ICommandBuilder<ResultRow>)
                .Where(x.CompanyName == "DynamicSoft") as ICommandBuilder<ResultRow>)
                .Select(x.CompanyName, x.Country, x.City) as ICommandBuilder<ResultRow>)
                .Build();
        }

        protected override Command SelectAllWhereOrderByCommand()
        {
            dynamic x = new DynamicCommandExpression();
            return (((_commandBuilder
                .From(x.Companies) as ICommandBuilder<ResultRow>)
                .Where(x.CompanyName == "DynamicSoft") as ICommandBuilder<ResultRow>)
                .OrderBy(x.Country) as ICommandBuilder<ResultRow>)
                .Build();
        }

        protected override Command SelectColumnsWhereOrderByCommand()
        {
            dynamic x = new DynamicCommandExpression();
            return (((((_commandBuilder
                .From(x.Companies) as ICommandBuilder<ResultRow>)
                .Where(x.YearEstablished > 2000 && x.NumberOfEmployees < 100) as ICommandBuilder<ResultRow>)
                .Select(x.CompanyName, x.Country, x.City) as ICommandBuilder<ResultRow>)
                .OrderBy(x.Country) as ICommandBuilder<ResultRow>)
                .OrderByDescending(x.YearEstablished) as ICommandBuilder<ResultRow>)
                .Build();
        }

        [TestMethod]
        public void ExecuteFindOne()
        {
            dynamic x = new DynamicCommandExpression();
            var command = SelectAllCommand();
            var commandProcessor = new FakeCommandProcessor(command);
            var result = commandProcessor.FindOne(x.Companies);
            Assert.AreEqual("DynamicSoft", result.CompanyName);
        }

        [TestMethod]
        public void ExecuteFindAllAsEnumerable()
        {
            dynamic x = new DynamicCommandExpression();
            var command = SelectAllCommand();
            var commandProcessor = new FakeCommandProcessor(command);
            IEnumerable<dynamic> result = commandProcessor.FindAll(x.Companies);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("DynamicSoft", result.First().CompanyName);
            Assert.AreEqual("StaticSoft", result.Last().CompanyName);
        }

        [TestMethod]
        public void ExecuteFindAllAsList()
        {
            dynamic x = new DynamicCommandExpression();
            var command = SelectAllCommand();
            var commandProcessor = new FakeCommandProcessor(command);
            List<dynamic> result = commandProcessor.FindAll(x.Companies);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("DynamicSoft", result.First().CompanyName);
            Assert.AreEqual("StaticSoft", result.Last().CompanyName);
        }

        [TestMethod]
        public void ExecuteFindAllAsArray()
        {
            dynamic x = new DynamicCommandExpression();
            var command = SelectAllCommand();
            var commandProcessor = new FakeCommandProcessor(command);
            dynamic[] result = commandProcessor.FindAll(x.Companies);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("DynamicSoft", result.First().CompanyName);
            Assert.AreEqual("StaticSoft", result.Last().CompanyName);
        }

        [TestMethod]
        public void ExecuteFindOneAsTyped()
        {
            dynamic x = new DynamicCommandExpression();
            var command = SelectAllCommand();
            var commandProcessor = new FakeCommandProcessor(command);
            Companies result = commandProcessor.FindOne(x.Companies);
            Assert.AreEqual("DynamicSoft", result.CompanyName);
        }

        [TestMethod]
        public void ExecuteFindAllAsTyped()
        {
            dynamic x = new DynamicCommandExpression();
            var command = SelectAllCommand();
            var commandProcessor = new FakeCommandProcessor(command);
            IEnumerable<Companies> result = commandProcessor.FindAll(x.Companies);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("DynamicSoft", result.First().CompanyName);
            Assert.AreEqual("StaticSoft", result.Last().CompanyName);
        }
    }
}