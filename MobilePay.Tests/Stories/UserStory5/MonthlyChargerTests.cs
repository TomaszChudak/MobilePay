using System;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MobilePay.Configuration;
using MobilePay.Models;
using MobilePay.UserStories;
using MobilePay.UserStories.UserStory5;
using Moq;
using Xunit;

namespace MobilePay.Tests.Stories.UserStory5
{
    public class MonthlyChargerTests
    {
        public MonthlyChargerTests()
        {
            _configMock = new Mock<IOptions<AppSettings>>(MockBehavior.Strict);
        }

        private Mock<IOptions<AppSettings>> _configMock;
        private IMonthlyCharger _sut;

        [Fact]
        public void GetMonthlyFee_NewMonth_FixedFee()
        {
            _configMock.Setup(x => x.Value)
                .Returns(new AppSettings{ InvoiceFixedFee = 15});

            var transaction = new Transaction { Date = new DateTime(2019, 7, 15), MerchantName = "CIRCLE_K", Amount = 100m};

            _sut = new MonthlyCharger(_configMock.Object);
            var result = _sut.GetMonthlyFee(transaction);

            result.Should().Be(15);
        }

        [Fact]
        public void GetMonthlyFee_TwoNewMonths_TwoFixedFees()
        {
            _configMock.Setup(x => x.Value)
                .Returns(new AppSettings{ InvoiceFixedFee = 15});

            var transaction1 = new Transaction { Date = new DateTime(2019, 7, 15), MerchantName = "CIRCLE_K", Amount = 100m};
            var transaction2 = new Transaction { Date = new DateTime(2019, 8, 15), MerchantName = "CIRCLE_K", Amount = 100m};

            _sut = new MonthlyCharger(_configMock.Object);
            var result1 = _sut.GetMonthlyFee(transaction1);
            var result2 = _sut.GetMonthlyFee(transaction2);

            result1.Should().Be(15);
            result2.Should().Be(15);
        }

        [Fact]
        public void GetMonthlyFee_TheSameMonth_OnlyFirstHasFixedFee()
        {
            _configMock.Setup(x => x.Value)
                .Returns(new AppSettings{ InvoiceFixedFee = 15});

            var transaction1 = new Transaction { Date = new DateTime(2019, 7, 15), MerchantName = "CIRCLE_K", Amount = 100m};
            var transaction2 = new Transaction { Date = new DateTime(2019, 7, 16), MerchantName = "CIRCLE_K", Amount = 100m};

            _sut = new MonthlyCharger(_configMock.Object);
            var result1 = _sut.GetMonthlyFee(transaction1);
            var result2 = _sut.GetMonthlyFee(transaction2);

            result1.Should().Be(15);
            result2.Should().Be(0);
        }

        [Fact]
        public void GetMonthlyFee_DifferentMerchant_TwoHasFixedFee()
        {
            _configMock.Setup(x => x.Value)
                .Returns(new AppSettings{ InvoiceFixedFee = 15});

            var transaction1 = new Transaction { Date = new DateTime(2019, 7, 15), MerchantName = "CIRCLE_K", Amount = 100m};
            var transaction2 = new Transaction { Date = new DateTime(2019, 7, 16), MerchantName = "TELIA", Amount = 100m};

            _sut = new MonthlyCharger(_configMock.Object);
            var result1 = _sut.GetMonthlyFee(transaction1);
            var result2 = _sut.GetMonthlyFee(transaction2);

            result1.Should().Be(15);
            result2.Should().Be(15);
        }

        [Fact]
        public void GetMonthlyFee_SixTransactions_FourWithFixedFee()
        {
            _configMock.Setup(x => x.Value)
                .Returns(new AppSettings{ InvoiceFixedFee = 15});

            var transaction1 = new Transaction { Date = new DateTime(2019, 7, 15), MerchantName = "CIRCLE_K", Amount = 100m};
            var transaction2 = new Transaction { Date = new DateTime(2019, 7, 16), MerchantName = "TELIA", Amount = 100m};
            var transaction3 = new Transaction { Date = new DateTime(2019, 7, 18), MerchantName = "CIRCLE_K", Amount = 100m};
            var transaction4 = new Transaction { Date = new DateTime(2019, 8, 10), MerchantName = "TELIA", Amount = 100m};
            var transaction5 = new Transaction { Date = new DateTime(2019, 8, 15), MerchantName = "CIRCLE_K", Amount = 100m};
            var transaction6 = new Transaction { Date = new DateTime(2019, 8, 16), MerchantName = "TELIA", Amount = 100m};

            _sut = new MonthlyCharger(_configMock.Object);
            var result1 = _sut.GetMonthlyFee(transaction1);
            var result2 = _sut.GetMonthlyFee(transaction2);
            var result3 = _sut.GetMonthlyFee(transaction3);
            var result4 = _sut.GetMonthlyFee(transaction4);
            var result5 = _sut.GetMonthlyFee(transaction5);
            var result6 = _sut.GetMonthlyFee(transaction6);

            result1.Should().Be(15);
            result2.Should().Be(15);
            result3.Should().Be(0);
            result4.Should().Be(15);
            result5.Should().Be(15);
            result6.Should().Be(0);
        }
    }
}