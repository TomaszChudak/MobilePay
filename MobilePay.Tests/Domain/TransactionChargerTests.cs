using System;
using System.Collections.Generic;
using FluentAssertions;
using MobilePay.Domain;
using MobilePay.Models;
using MobilePay.UserStories;
using MobilePay.UserStories.UserStory2;
using MobilePay.UserStories.UserStory4;
using Moq;
using Xunit;

namespace MobilePay.Tests.Domain
{
    public class TransactionChargerTests
    {
        public TransactionChargerTests()
        {
            _userStoryFactoryMock = new Mock<IUserStoryFactory>(MockBehavior.Strict);
        }

        private ITransactionCharger _sut;

        private readonly Mock<IUserStoryFactory> _userStoryFactoryMock;

        [Fact]
        public void Charge_NoUserStories_FeeIsZero()
        {
            _userStoryFactoryMock.Setup(x => x.CreateList())
                .Returns(new List<IUserStoryMobilePay>());

            var transaction = new Transaction {Date = new DateTime(2019, 07, 13), MerchantName = "TELIA", Amount = 13.45m};

            _sut = new TransactionCharger(_userStoryFactoryMock.Object);
            _sut.Charge(transaction);

            transaction.Fee.BasicFee.Should().Be(0);
            transaction.Fee.AdditionalFee.Should().Be(0);
            transaction.Fee.Amount.Should().Be(0);

            _userStoryFactoryMock.Verify(x => x.CreateList(), Times.Once);
        }

        [Fact]
        public void Charge_SimpleUserStory_FeeIsCounted()
        {
            _userStoryFactoryMock.Setup(x => x.CreateList())
                .Returns(new List<IUserStoryMobilePay> {new UserStoryMobilePay2()});

            var transaction = new Transaction {Date = new DateTime(2019, 07, 13), MerchantName = "TELIA", Amount = 13.45m};

            _sut = new TransactionCharger(_userStoryFactoryMock.Object);
            _sut.Charge(transaction);

            transaction.Fee.BasicFee.Should().Be(0.1345m);
            transaction.Fee.AdditionalFee.Should().Be(0);
            transaction.Fee.Amount.Should().Be(0.13m);

            _userStoryFactoryMock.Verify(x => x.CreateList(), Times.Once);
        }

        [Fact]
        public void Charge_TwoUserStories_FeeIsCounted()
        {
            _userStoryFactoryMock.Setup(x => x.CreateList())
                .Returns(new List<IUserStoryMobilePay> {new UserStoryMobilePay2(), new UserStoryMobilePay4()});

            var transaction = new Transaction {Date = new DateTime(2019, 07, 13), MerchantName = "CIRCLE_K", Amount = 20m};

            _sut = new TransactionCharger(_userStoryFactoryMock.Object);
            _sut.Charge(transaction);

            transaction.Fee.BasicFee.Should().Be(0.16m);
            transaction.Fee.AdditionalFee.Should().Be(0);
            transaction.Fee.Amount.Should().Be(0.16m);

            _userStoryFactoryMock.Verify(x => x.CreateList(), Times.Once);
        }
    }
}