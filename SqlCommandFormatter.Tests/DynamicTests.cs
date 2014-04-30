using NUnit.Framework;
using SqlCommandFormatter.Dynamic;

namespace SqlCommandFormatter.Tests
{
    [TestFixture]
    public class DynamicTests
    {
        private readonly CommandFormatter _commandFormatter;

        public DynamicTests()
        {
            _commandFormatter = new CommandFormatter();
        }

        [Test]
        public void SelectAll()
        {
            var x = DynamicCommand.Expression;
            var commandText = _commandFormatter
                .For(x.Companies)
                .Where(x.CompanyName == "Microsoft")
                .Format();

            Assert.AreEqual("SELECT * FROM Companies WHERE Name=='Microsoft'", commandText);
        }
    }
}