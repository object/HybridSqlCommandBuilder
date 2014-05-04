using NUnit.Framework;

namespace SqlCommandBuilder.Tests
{
    public abstract class TestBase
    {
        protected readonly CommandBuilder _commandBuilder;

        protected TestBase()
        {
            _commandBuilder = new CommandBuilder();
        }

        protected abstract Command SelectAllCommand();
        protected abstract Command SelectAllWhereCommand();
        protected abstract Command SelectAllWhereFunctionCommand();
        protected abstract Command SelectColumnsWhereCommand();
        protected abstract Command SelectAllWhereOrderByCommand();
        protected abstract Command SelectColumnsWhereOrderByCommand();

        [Test]
        public void SelectAll()
        {
            Assert.AreEqual(
                "SELECT * FROM Companies", 
                SelectAllCommand().ToString());
        }

        [Test]
        public void SelectAllWhere()
        {
            Assert.AreEqual(
                "SELECT * FROM Companies WHERE CompanyName='DynamicSoft'", 
                SelectAllWhereCommand().ToString());
        }

        [Test]
        public void SelectAllWhereFunction()
        {
            Assert.AreEqual(
                "SELECT * FROM Companies WHERE LEN(CompanyName)<10 AND CompanyName=UPPER(CompanyName)",
                SelectAllWhereFunctionCommand().ToString());
        }

        [Test]
        public void SelectColumnsWhere()
        {
            Assert.AreEqual(
                "SELECT CompanyName,Country,City FROM Companies WHERE CompanyName='DynamicSoft'", 
                SelectColumnsWhereCommand().ToString());
        }

        [Test]
        public void SelectAllWhereOrderBy()
        {
            Assert.AreEqual(
                "SELECT * FROM Companies WHERE CompanyName='DynamicSoft' ORDER BY Country",
                SelectAllWhereOrderByCommand().ToString());
        }

        [Test]
        public void SelectColumnsWhereOrderBy()
        {
            Assert.AreEqual(
                "SELECT CompanyName,Country,City FROM Companies WHERE YearEstablished>2000 AND NumberOfEmployees<100 ORDER BY Country,YearEstablished DESC",
                SelectColumnsWhereOrderByCommand().ToString());
        }
    }
}