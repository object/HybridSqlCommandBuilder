using System.Collections.Generic;
using NUnit.Framework;

namespace SqlCommandBuilder.Tests
{
    public class FakeCommandProcessor : CommandProcessor
    {
        public FakeCommandProcessor(Command command) : base(command)
        {
        }

        protected override IEnumerable<IDictionary<string, object>> Execute()
        {
            var row1 = new Dictionary<string, object>();
            row1["CompanyName"] = "DynamicSoft";
            var row2 = new Dictionary<string, object>();
            row2["CompanyName"] = "StaticSoft";

            return new List<IDictionary<string, object>> { row1, row2 };
        }
    }
}