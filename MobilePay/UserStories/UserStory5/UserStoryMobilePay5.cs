using MobilePay.Models;

namespace MobilePay.UserStories.UserStory5
{
    internal class UserStoryMobilePay5 : IUserStoryMobilePay
    {
        private readonly IMonthlyCharger _monthlyCharger;
        
        public UserStoryMobilePay5(IMonthlyCharger monthlyCharger)
        {
            _monthlyCharger = monthlyCharger;
        }

        public int UserStoryNo => 5;

        public void CalculateFee(Transaction transaction)
        {
            transaction.Fee.AdditionalFee += _monthlyCharger.GetMonthlyFee(transaction);
        }
    }
}