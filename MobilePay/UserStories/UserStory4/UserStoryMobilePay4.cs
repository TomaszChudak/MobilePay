using System;
using MobilePay.Models;

namespace MobilePay.UserStories.UserStory4
{
    internal class UserStoryMobilePay4 : IUserStoryMobilePay
    {
        public int UserStoryNo => 4;

        public void CalculateFee(Transaction transaction)
        {
            if (transaction.MerchantName.Equals("CIRCLE_K", StringComparison.InvariantCultureIgnoreCase))
                transaction.Fee.BasicFee *= 0.8m;
        }
    }
}