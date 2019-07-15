using System;
using System.Collections.Generic;
using FluentAssertions;
using MobilePay.Domain;
using MobilePay.Formatters;
using MobilePay.IO;
using MobilePay.Models;
using Moq;
using Xunit;

namespace MobilePay.Tests.Domain
{
    public class TransactionReaderTests
    {
        private readonly ITransactionReader _sut;

        public TransactionReaderTests()
        {
            _fileReaderMock = new Mock<IFileReader>(MockBehavior.Strict);
            _transactionFormatterMock = new Mock<ITransactionFormatter>(MockBehavior.Strict);
            _sut = new TransactionReader(_fileReaderMock.Object, _transactionFormatterMock.Object);
        }

        private Mock<IFileReader> _fileReaderMock;
        private Mock<ITransactionFormatter> _transactionFormatterMock;

        [Fact]
        public void GetNextTransaction_ThereIsNextTransaction_Transaction()
        {
            var transaction = new Transaction {Date = new DateTime(2019, 07, 11), MerchantName = "TELIA", Amount = 13.45m};
            _fileReaderMock.Setup(x => x.ReadTransactions())
                .Returns(new List<string> {"2019-07-11 TELIA\t13.45", "2019-07-22 TELIA\t22.22"});
            _transactionFormatterMock.Setup(x => x.Format("2019-07-11 TELIA\t13.45"))
                .Returns(transaction);

            var result = _sut.GetNextTransaction();

            result.Should().NotBeNull();
            result.Should().Be(transaction);

            _fileReaderMock.Verify(x => x.ReadTransactions(), Times.Once);
            _transactionFormatterMock.Verify(x => x.Format("2019-07-11 TELIA\t13.45"), Times.Once);
        }

        [Fact]
        public void GetNextTransaction_EmptyLine_ReturnEmptyTransaction()
        {
            var transaction = new Transaction {EmptyLine = true};
            _fileReaderMock.Setup(x => x.ReadTransactions())
                .Returns(new List<string> {"  ", "2019-07-22 TELIA\t22.22"});
            _transactionFormatterMock.Setup(x => x.Format("  "))
                .Returns(transaction);

            var result = _sut.GetNextTransaction();

            result.Should().NotBeNull();
            result.Should().Be(transaction);

            _fileReaderMock.Verify(x => x.ReadTransactions(), Times.Once);
            _transactionFormatterMock.Verify(x => x.Format("  "), Times.Once);
        }

        [Fact]
        public void GetNextTransaction_EndOfFile_ReturnNull()
        {
            _fileReaderMock.Setup(x => x.ReadTransactions())
                .Returns(new List<string> {});
            _transactionFormatterMock.Setup(x => x.Format("2019-07-11 TELIA\t13.45"))
                .Returns((Transaction)null);

            var result = _sut.GetNextTransaction();

            result.Should().BeNull();

            _fileReaderMock.Verify(x => x.ReadTransactions(), Times.Once);
            _transactionFormatterMock.Verify(x => x.Format(It.IsAny<string>()), Times.Never);
        }
    }
}
