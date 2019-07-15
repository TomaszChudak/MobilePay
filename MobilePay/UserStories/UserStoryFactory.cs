using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Options;
using MobilePay.Configuration;

namespace MobilePay.UserStories
{
    internal interface IUserStoryFactory
    {
        IEnumerable<IUserStoryMobilePay> CreateList();
    }

    internal class UserStoryFactory : IUserStoryFactory
    {
        private readonly AppSettings _config;
        protected readonly IEnumerable<IUserStoryMobilePay> _userStories;

        public UserStoryFactory(IOptions<AppSettings> config, IEnumerable<IUserStoryMobilePay> userStories)
        {
            _config = config.Value;
            _userStories = userStories;
        }

        public IEnumerable<IUserStoryMobilePay> CreateList()
        {
            if (_config.RequiredUserStories == null)
                return _userStories;

            return _userStories.Where(x => _config.RequiredUserStories.Contains(x.UserStoryNo));
        }
    }
}