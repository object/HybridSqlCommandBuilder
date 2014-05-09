using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
//using SqlCommandBuilder.Dynamic;

namespace SqlCommandBuilder.Tests
{
    //[TestFixture]
    //public class DynamicTests : TestBase
    //{
    //    protected override Command SelectAllCommand()
    //    {
    //        dynamic x = new DynamicCommandExpression();
    //        return _commandBuilder
    //            .From(x.Companies)
    //            .Build();
    //    }

    //    protected override Command SelectAllWhereCommand()
    //    {
    //        dynamic x = new DynamicCommandExpression();
    //        return _commandBuilder
    //            .From(x.Companies)
    //            .Where(x.CompanyName == "DynamicSoft")
    //            .Build();
    //    }

    //    protected override Command SelectAllWhereFunctionCommand()
    //    {
    //        dynamic x = new DynamicCommandExpression();
    //        return _commandBuilder
    //            .From(x.Companies)
    //            .Where(x.CompanyName.Length() < 10 && x.CompanyName == x.CompanyName.ToUpper())
    //            .Build();
    //    }

    //    protected override Command SelectColumnsWhereCommand()
    //    {
    //        dynamic x = new DynamicCommandExpression();
    //        return _commandBuilder
    //            .From(x.Companies)
    //            .Where(x.CompanyName == "DynamicSoft")
    //            .Select(x.CompanyName, x.Country, x.City)
    //            .Build();
    //    }

    //    protected override Command SelectAllWhereOrderByCommand()
    //    {
    //        dynamic x = new DynamicCommandExpression();
    //        return _commandBuilder
    //            .From(x.Companies)
    //            .Where(x.CompanyName == "DynamicSoft")
    //            .OrderBy(x.Country)
    //            .Build();
    //    }

    //    protected override Command SelectColumnsWhereOrderByCommand()
    //    {
    //        dynamic x = new DynamicCommandExpression();
    //        return _commandBuilder
    //            .From(x.Companies)
    //            .Where(x.YearEstablished > 2000 && x.NumberOfEmployees < 100)
    //            .Select(x.CompanyName, x.Country, x.City)
    //            .OrderBy(x.Country)
    //            .OrderByDescending(x.YearEstablished)
    //            .Build();
    //    }

    //    [Test]
    //    public void ExecuteFindOne()
    //    {
    //        dynamic x = new DynamicCommandExpression();
    //        var command = SelectAllCommand();
    //        var commandProcessor = new FakeCommandProcessor(command);
    //        var result = commandProcessor.FindOne(x.Companies);
    //        Assert.AreEqual("DynamicSoft", result.CompanyName);
    //    }

    //    [Test]
    //    public void ExecuteFindAllAsEnumerable()
    //    {
    //        dynamic x = new DynamicCommandExpression();
    //        var command = SelectAllCommand();
    //        var commandProcessor = new FakeCommandProcessor(command);
    //        IEnumerable<dynamic> result = commandProcessor.FindAll(x.Companies);
    //        Assert.AreEqual(2, result.Count());
    //        Assert.AreEqual("DynamicSoft", result.First().CompanyName);
    //        Assert.AreEqual("StaticSoft", result.Last().CompanyName);
    //    }

    //    [Test]
    //    public void ExecuteFindAllAsList()
    //    {
    //        dynamic x = new DynamicCommandExpression();
    //        var command = SelectAllCommand();
    //        var commandProcessor = new FakeCommandProcessor(command);
    //        List<dynamic> result = commandProcessor.FindAll(x.Companies);
    //        Assert.AreEqual(2, result.Count());
    //        Assert.AreEqual("DynamicSoft", result.First().CompanyName);
    //        Assert.AreEqual("StaticSoft", result.Last().CompanyName);
    //    }

    //    [Test]
    //    public void ExecuteFindAllAsArray()
    //    {
    //        dynamic x = new DynamicCommandExpression();
    //        var command = SelectAllCommand();
    //        var commandProcessor = new FakeCommandProcessor(command);
    //        dynamic[] result = commandProcessor.FindAll(x.Companies);
    //        Assert.AreEqual(2, result.Count());
    //        Assert.AreEqual("DynamicSoft", result.First().CompanyName);
    //        Assert.AreEqual("StaticSoft", result.Last().CompanyName);
    //    }

    //    [Test]
    //    public void ExecuteFindOneAsTyped()
    //    {
    //        dynamic x = new DynamicCommandExpression();
    //        var command = SelectAllCommand();
    //        var commandProcessor = new FakeCommandProcessor(command);
    //        Companies result = commandProcessor.FindOne(x.Companies);
    //        Assert.AreEqual("DynamicSoft", result.CompanyName);
    //    }

    //    [Test]
    //    public void ExecuteFindAllAsTyped()
    //    {
    //        dynamic x = new DynamicCommandExpression();
    //        var command = SelectAllCommand();
    //        var commandProcessor = new FakeCommandProcessor(command);
    //        IEnumerable<Companies> result = commandProcessor.FindAll(x.Companies);
    //        Assert.AreEqual(2, result.Count());
    //        Assert.AreEqual("DynamicSoft", result.First().CompanyName);
    //        Assert.AreEqual("StaticSoft", result.Last().CompanyName);
    //    }
    //}
}