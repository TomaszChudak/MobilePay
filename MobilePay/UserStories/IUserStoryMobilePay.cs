using MobilePay.Models;

namespace MobilePay.UserStories
{
    interface IUserStoryMobilePay
    {
        int UserStoryNo { get; }
        void CalculateFee(Transaction transaction);
    }
}
