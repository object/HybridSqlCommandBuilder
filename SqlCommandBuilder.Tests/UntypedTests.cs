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
    }
}