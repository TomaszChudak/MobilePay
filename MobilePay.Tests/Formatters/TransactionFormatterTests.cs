using System;
using FluentAssertions;
using MobilePay.Formatters;
using Xunit;

namespace MobilePay.Tests.Formatters
{
    public class TransactionFormatterTests
    {
        private ITransactionFormatter _sut;

        public TransactionFormatterTests()
        {
            _sut = new TransactionFormatter();
        }

        [Fact]
        public void Format_SimpleTransactionWithSpaces_ReturnTransaction()
        {
            var inputTransaction = "2018-09-01 TELIA 30.00";

            var result = _sut.Format(inputTransaction);

            result.Should().NotBeNull();
            result.Date.Should().Be(new DateTime(2018, 9, 1));
            result.MerchantName.Should().Be("TELIA");
            result.Amount.Should().Be(30);
            result.EmptyLine.Should().BeFalse();
        }

        [Fact]
        public void Format_SimpleTransactionWithManySpaces_ReturnTransaction()
        {
            var inputTransaction = "2018-09-11   7-ELEVEN   10.00";

            var result = _sut.Format(inputTransaction);

            result.Should().NotBeNull();
            result.Date.Should().Be(new DateTime(2018, 9, 11));
            result.MerchantName.Should().Be("7-ELEVEN");
            result.Amount.Should().Be(10);
            result.EmptyLine.Should().BeFalse();
        }

        [Fact]
        public void Format_SimpleTransactionWithTabs_ReturnTransaction()
        {
            var inputTransaction = "2018-09-01\t7-ELEVEN\t30.30";

            var result = _sut.Format(inputTransaction);

            result.Should().NotBeNull();
            result.Date.Should().Be(new DateTime(2018, 9, 1));
            result.MerchantName.Should().Be("7-ELEVEN");
            result.Amount.Should().Be(30.3m);
            result.EmptyLine.Should().BeFalse();
        }

        [Fact]
        public void Format_EmptyLine_ReturnsNull()
        {
            var inputTransaction = "";

            var result = _sut.Format(inputTransaction);

            result.Should().NotBeNull();
            result.EmptyLine.Should().BeTrue();
        }

        [Fact]
        public void Format_OnlySpacesInLine_ReturnsNull()
        {
            var inputTransaction = "    ";

            var result = _sut.Format(inputTransaction);

            result.Should().NotBeNull();
            result.EmptyLine.Should().BeTrue();
        }

        [Fact]
        public void Format_Null_ThrowsException()
        {
            var inputTransaction = (string)null;

            Action act = () => _sut.Format(inputTransaction);

            act.Should().Throw<ArgumentNullException>();
        }
    }
}
