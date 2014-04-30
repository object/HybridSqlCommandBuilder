using NUnit.Framework;

namespace SqlCommandFormatter.Tests
{
    [TestFixture]
    public class UntypedTests
    {
        private readonly CommandFormatter _commandFormatter;

        public UntypedTests()
        {
            _commandFormatter = new CommandFormatter();
        }

        [Test]
        public void SelectAll()
        {
            var commandText = _commandFormatter
                .For("Companies")
                .Where("CompanyName == 'Microsoft'")
                .Format();

            Assert.AreEqual("SELECT * FROM Companies WHERE Name=='Microsoft'", commandText);
        }
    }
}