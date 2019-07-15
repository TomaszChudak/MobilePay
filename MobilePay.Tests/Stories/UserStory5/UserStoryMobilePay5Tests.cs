using System;
using FluentAssertions;
using MobilePay.Models;
using MobilePay.UserStories;
using MobilePay.UserStories.UserStory5;
using Moq;
using Xunit;

namespace MobilePay.Tests.Stories.UserStory5
{
    public class UserStoryMobilePay5Tests
    {
        public UserStoryMobilePay5Tests()
        {
            _monthlyChargerMock = new Mock<IMonthlyCharger>(MockBehavior.Strict);
            _sut = new UserStoryMobilePay5(_monthlyChargerMock.Object);
        }

        private readonly Mock<IMonthlyCharger> _monthlyChargerMock;
        private IUserStoryMobilePay _sut;

        [Fact]
        public void CalculateFee_NoMonthlyFee_NoAdditionalFee()
        {
            var transaction = new Transaction { Date = new DateTime(2019, 7, 15), MerchantName = "CIRCLE_K", Amount = 100m};
            var transactionCopy = new Transaction { Date = transaction.Date, MerchantName = transaction.MerchantName, Amount = transaction.Amount};
            transaction.Fee = new TransactionFee { BasicFee = 10, AdditionalFee = 0};

            _monthlyChargerMock.Setup(x => x.GetMonthlyFee(transaction))
                .Returns(0);

            _sut.CalculateFee(transaction);

            transaction.Date.Should().Be(transactionCopy.Date);
            transaction.MerchantName.Should().Be(transactionCopy.MerchantName);
            transaction.Amount.Should().Be(transactionCopy.Amount);
            transaction.EmptyLine.Should().BeFalse();
            transaction.Fee.Should().NotBeNull();
            transaction.Fee.BasicFee.Should().Be(10);
            transaction.Fee.AdditionalFee.Should().Be(0);
            transaction.Fee.Amount.Should().Be(10);
        }

        [Fact]
        public void CalculateFee_MonthlyFee_AdditionalFeeChanged()
        {
            var transaction = new Transaction { Date = new DateTime(2019, 7, 15), MerchantName = "CIRCLE_K", Amount = 100m};
            var transactionCopy = new Transaction { Date = transaction.Date, MerchantName = transaction.MerchantName, Amount = transaction.Amount};
            transaction.Fee = new TransactionFee { BasicFee = 10, AdditionalFee = 0};

            _monthlyChargerMock.Setup(x => x.GetMonthlyFee(transaction))
                .Returns(20);

            _sut.CalculateFee(transaction);

            transaction.Date.Should().Be(transactionCopy.Date);
            transaction.MerchantName.Should().Be(transactionCopy.MerchantName);
            transaction.Amount.Should().Be(transactionCopy.Amount);
            transaction.EmptyLine.Should().BeFalse();
            transaction.Fee.Should().NotBeNull();
            transaction.Fee.BasicFee.Should().Be(10);
            transaction.Fee.AdditionalFee.Should().Be(20);
            transaction.Fee.Amount.Should().Be(30);
        }
    }
}