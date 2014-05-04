using System.Collections.Generic;
using NUnit.Framework;

namespace SqlCommandBuilder.Tests
{
    public class FakeCommandProcessor : CommandProcessor
    {
        public FakeCommandProcessor(Command command) : base(command)
        {
        }

        protected override IEnumerable<ResultRow> Execute()
        {
            var row1 = new ResultRow();
            row1["CompanyName"] = "DynamicSoft";
            var row2 = new ResultRow();
            row2["CompanyName"] = "StaticSoft";

            return new List<ResultRow> {row1, row2};
        }
    }
}