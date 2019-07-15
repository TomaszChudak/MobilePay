using System;
using FluentAssertions;
using MobilePay.Models;
using MobilePay.UserStories;
using MobilePay.UserStories.UserStory3;
using Xunit;

namespace MobilePay.Tests.Stories.UserStory3
{
    public class UserStoryMobilePay3Tests
    {
        public UserStoryMobilePay3Tests()
        {
            _sut = new UserStoryMobilePay3();
        }

        private readonly IUserStoryMobilePay _sut;

        [Fact]
        public void CalculateFee_DiscountMerchant_DiscountGiven()
        {
            var transaction = new Transaction {Date = new DateTime(2019, 7, 15), MerchantName = "TELIA", Amount = 100m};
            var transactionCopy = new Transaction {Date = transaction.Date, MerchantName = transaction.MerchantName, Amount = transaction.Amount};
            transaction.Fee = new TransactionFee {BasicFee = 1, AdditionalFee = 0};

            _sut.CalculateFee(transaction);

            transaction.Date.Should().Be(transactionCopy.Date);
            transaction.MerchantName.Should().Be(transactionCopy.MerchantName);
            transaction.Amount.Should().Be(transactionCopy.Amount);
            transaction.EmptyLine.Should().BeFalse();
            transaction.Fee.Should().NotBeNull();
            transaction.Fee.BasicFee.Should().Be(0.9m);
            transaction.Fee.AdditionalFee.Should().Be(0);
            transaction.Fee.Amount.Should().Be(0.9m);
        }

        [Fact]
        public void CalculateFee_OrdinaryMerchant_NoDiscount()
        {
            var transaction = new Transaction {Date = new DateTime(2019, 7, 15), MerchantName = "OTHER_MERCHANT", Amount = 100m};
            var transactionCopy = new Transaction {Date = transaction.Date, MerchantName = transaction.MerchantName, Amount = transaction.Amount};
            transaction.Fee = new TransactionFee {BasicFee = 1, AdditionalFee = 0};

            _sut.CalculateFee(transaction);

            transaction.Date.Should().Be(transactionCopy.Date);
            transaction.MerchantName.Should().Be(transactionCopy.MerchantName);
            transaction.Amount.Should().Be(transactionCopy.Amount);
            transaction.EmptyLine.Should().BeFalse();
            transaction.Fee.Should().NotBeNull();
            transaction.Fee.BasicFee.Should().Be(1);
            transaction.Fee.AdditionalFee.Should().Be(0);
            transaction.Fee.Amount.Should().Be(1);
        }
    }
}