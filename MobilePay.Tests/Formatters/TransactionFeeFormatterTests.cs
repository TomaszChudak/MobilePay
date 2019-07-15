using System;
using FluentAssertions;
using MobilePay.Formatters;
using MobilePay.Models;
using Xunit;

namespace MobilePay.Tests.Formatters
{
    public class TransactionFeeFormatterTests
    {
        private ITransactionFeeFormatter _sut;

        public TransactionFeeFormatterTests()
        {
            _sut = new TransactionFeeFormatter();
        }

        [Fact]
        public void Format_SimpleTransactionWithFee_OK()
        {
            var inputTransaction = new Transaction {Date = new DateTime(2019, 07, 13), MerchantName = "TELIA", Fee = new TransactionFee {BasicFee = 13.45m, AdditionalFee = 0}};

            var result = _sut.Format(inputTransaction);

            result.Should().NotBeNull();
            result.Should().Be("2019-07-13 TELIA\t13.45");
        }

        [Fact]
        public void Format_EmptyLine_ReturnEmptyLine()
        {
            var inputTransaction = new Transaction {EmptyLine = true};

            var result = _sut.Format(inputTransaction);

            result.Should().NotBeNull();
            result.Should().Be("");
        }

        [Fact]
        public void Format_NullTransaction_ThrowException()
        {
            var inputTransaction = (Transaction) null;

            Action act = () => _sut.Format(inputTransaction);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Format_NullFee_ThrowException()
        {
            var inputTransaction = new Transaction {Date = new DateTime(2019, 07, 13), MerchantName = "TELIA", Fee = null};

            Action act = () => _sut.Format(inputTransaction);

            act.Should().Throw<ArgumentNullException>();
        }
    }
}
