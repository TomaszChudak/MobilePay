using System;
using FluentAssertions;
using MobilePay.Domain;
using MobilePay.Formatters;
using MobilePay.IO;
using MobilePay.Models;
using Moq;
using Xunit;

namespace MobilePay.Tests.Domain
{
    public class TransactionFeeWriterTests
    {
        private readonly ITransactionFeeWriter _sut;

        public TransactionFeeWriterTests()
        {
            _consoleWriterMock = new Mock<IConsoleWriter>(MockBehavior.Strict);
            _transactionFeeFormatterMock = new Mock<ITransactionFeeFormatter>(MockBehavior.Strict);
            _sut = new TransactionFeeWriter(_consoleWriterMock.Object, _transactionFeeFormatterMock.Object);
        }

        private readonly Mock<IConsoleWriter> _consoleWriterMock;
        private readonly Mock<ITransactionFeeFormatter> _transactionFeeFormatterMock;

        [Fact]
        public void WriteNextTransactionFee_SimpleTransaction_OK()
        {
            var transactionFee = new Transaction { Date = new DateTime(2019, 07, 13), MerchantName = "TELIA", Fee = new TransactionFee {BasicFee = 13.45m, AdditionalFee = 0} };
            var transactionFeeString = "2019-07-13 TELIA\t13.45";
            _transactionFeeFormatterMock.Setup(x => x.Format(transactionFee))
                .Returns(transactionFeeString);
            _consoleWriterMock.Setup(x => x.WriteLine(transactionFeeString));

            _sut.WriteNextTransactionFee(transactionFee);

            _transactionFeeFormatterMock.Verify(x => x.Format(transactionFee), Times.Once);
            _consoleWriterMock.Verify(x => x.WriteLine(transactionFeeString), Times.Once);
        }

        [Fact]
        public void WriteNextTransactionFee_Exception_ThrowException()
        {
            var transactionFee = new Transaction { Date = new DateTime(2019, 07, 13), MerchantName = "TELIA", Fee = new TransactionFee {BasicFee = 13.45m, AdditionalFee = 0} };
            _transactionFeeFormatterMock.Setup(x => x.Format(transactionFee))
                .Throws<ArgumentNullException>();

            Action act = () => _sut.WriteNextTransactionFee(transactionFee);

            act.Should().Throw<ArgumentNullException>();

            _transactionFeeFormatterMock.Verify(x => x.Format(transactionFee), Times.Once);
            _consoleWriterMock.Verify(x => x.WriteLine(It.IsAny<string>()), Times.Never);
        }
    }
}
