using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace SqlCommandBuilder.Tests
{
    [TestFixture]
    public class TypedTests
    {
        private readonly CommandFormatter _commandFormatter;

        public TypedTests()
        {
            _commandFormatter = new CommandFormatter();
        }

        [Test]
        public void SelectAll()
        {
            var commandText = _commandFormatter
                .For<Companies>()
                .Where(x => x.CompanyName == "Microsoft")
                .Format();

            Assert.AreEqual("SELECT * FROM Companies WHERE Name=='Microsoft'", commandText);
        }
    }
}
