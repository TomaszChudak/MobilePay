using System;
using MobilePay.Models;

namespace MobilePay.UserStories.UserStory3
{
    internal class UserStoryMobilePay3 : IUserStoryMobilePay
    {
        public int UserStoryNo => 3;

        public void CalculateFee(Transaction transaction)
        {
            if (transaction.MerchantName.Equals("TELIA", StringComparison.InvariantCultureIgnoreCase))
                transaction.Fee.BasicFee *= 0.9m;
        }
    }
}