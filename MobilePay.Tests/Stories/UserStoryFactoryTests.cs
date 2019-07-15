using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.Options;
using MobilePay.Configuration;
using MobilePay.UserStories;
using MobilePay.UserStories.UserStory2;
using MobilePay.UserStories.UserStory3;
using MobilePay.UserStories.UserStory4;
using MobilePay.UserStories.UserStory5;
using Moq;
using Xunit;

namespace MobilePay.Tests.Stories
{
    public class UserStoryFactoryTests
    {
        public UserStoryFactoryTests()
        {
            _configMock = new Mock<IOptions<AppSettings>>(MockBehavior.Strict);
        }

        private IUserStoryFactory _sut;
        private readonly Mock<IOptions<AppSettings>> _configMock;

        [Fact]
        public void CreateList_AllUserStories_OK()
        {
            _configMock.Setup(x => x.Value)
                .Returns(new AppSettings{ RequiredUserStories = (IEnumerable<int>) null});
            var allUserStories = new List<IUserStoryMobilePay>
            {
                new UserStoryMobilePay2(), 
                new UserStoryMobilePay3(),
                new UserStoryMobilePay4(), 
                new UserStoryMobilePay5(new MonthlyCharger(_configMock.Object))
            };

            _sut = new UserStoryFactory(_configMock.Object, allUserStories);

            var result = _sut.CreateList().ToList();

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(allUserStories);
        }

        [Fact]
        public void CreateList_TwoUserStories_OK()
        {
            _configMock.Setup(x => x.Value)
                .Returns(new AppSettings{ RequiredUserStories = new List<int>{ 2, 4}});
            var allUserStories = new List<IUserStoryMobilePay>
            {
                new UserStoryMobilePay2(), 
                new UserStoryMobilePay3(),
                new UserStoryMobilePay4(), 
                new UserStoryMobilePay5(new MonthlyCharger(_configMock.Object))
            };

            _sut = new UserStoryFactory(_configMock.Object, allUserStories);

            var result = _sut.CreateList().ToList();

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().Contain(x => x.UserStoryNo == 2);
            result.Should().Contain(x => x.UserStoryNo == 4);
        }
    }
}