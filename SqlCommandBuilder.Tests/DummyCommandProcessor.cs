using System.Collections.Generic;
using NUnit.Framework;

namespace SqlCommandBuilder.Tests
{
    public class DummyCommandProcessor : CommandProcessor
    {
        public DummyCommandProcessor(Command command) : base(command)
        {
        }

        protected override IEnumerable<Record> Execute()
        {
            var record1 = new Record();
            record1["CompanyName"] = "DynamicSoft";
            var record2 = new Record();
            record2["CompanyName"] = "StaticSoft";

            return new List<Record> {record1, record2};
        }
    }
}