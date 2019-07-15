using MobilePay.Models;

namespace MobilePay.UserStories.UserStory2
{
    internal class UserStoryMobilePay2 : IUserStoryMobilePay
    {
        public int UserStoryNo => 2;

        public void CalculateFee(Transaction transaction)
        {
            transaction.Fee.BasicFee = transaction.Amount * 0.01m;
        }
    }
}