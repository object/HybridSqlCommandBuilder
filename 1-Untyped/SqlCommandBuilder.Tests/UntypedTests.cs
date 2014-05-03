using System.Linq;
using NUnit.Framework;

namespace SqlCommandBuilder.Tests
{
    [TestFixture]
    public class UntypedTests
    {
        [Test]
        public void SelectAll()
        {
            var command = new CommandBuilder()
                .From("Companies")
                .Build();

            Assert.AreEqual(
                "SELECT * FROM Companies",
                command.ToString());
        }

        [Test]
        public void SelectAllWhere()
        {
            var command = new CommandBuilder()
                .From("Companies")
                .Where("CompanyName='DynamicSoft'")
                .Build();

            Assert.AreEqual(
                "SELECT * FROM Companies WHERE CompanyName='DynamicSoft'",
                command.ToString());
        }

        [Test]
        public void SelectAllWhereFunction()
        {
            var command = new CommandBuilder()
                .From("Companies")
                .Where("LEN(CompanyName)<10 AND CompanyName=UPPER(CompanyName)")
                .Build();

            Assert.AreEqual(
                "SELECT * FROM Companies WHERE LEN(CompanyName)<10 AND CompanyName=UPPER(CompanyName)",
                command.ToString());
        }

        [Test]
        public void SelectColumnsWhere()
        {
            var command = new CommandBuilder()
                .From("Companies")
                .Where("CompanyName='DynamicSoft'")
                .Select("CompanyName", "Country", "City")
                .Build();

            Assert.AreEqual(
                "SELECT CompanyName,Country,City FROM Companies WHERE CompanyName='DynamicSoft'",
                command.ToString());
        }

        [Test]
        public void SelectAllWhereOrderBy()
        {
            var command = new CommandBuilder()
                .From("Companies")
                .Where("CompanyName='DynamicSoft'")
                .OrderBy("Country")
                .Build();

            Assert.AreEqual(
                "SELECT * FROM Companies WHERE CompanyName='DynamicSoft' ORDER BY Country",
                command.ToString());
        }

        [Test]
        public void SelectColumnsWhereOrderBy()
        {
            var command = new CommandBuilder()
                .From("Companies")
                .Where("YearEstablished>2000 AND NumberOfEmployees<100")
                .Select("CompanyName", "Country", "City")
                .OrderBy("Country")
                .OrderByDescending("YearEstablished")
                .Build();

            Assert.AreEqual(
                "SELECT CompanyName,Country,City FROM Companies WHERE YearEstablished>2000 AND NumberOfEmployees<100 ORDER BY Country,YearEstablished DESC",
                command.ToString());
        }
    }
}