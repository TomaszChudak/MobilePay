using System.Collections.Generic;
using MobilePay.Models;
using MobilePay.UserStories;

namespace MobilePay.Domain
{
    interface ITransactionCharger
    {
        void Charge(Transaction transaction);
    }

    class TransactionCharger : ITransactionCharger
    {
        private readonly IEnumerable<IUserStoryMobilePay> _requiredUserStories;

        public TransactionCharger(IUserStoryFactory userStoryFactory)
        {
            _requiredUserStories = userStoryFactory.CreateList();
        }

        public void Charge(Transaction transaction)
        {
            transaction.Fee = new TransactionFee {BasicFee = 0, AdditionalFee = 0};

            foreach (var userStory in _requiredUserStories)
            {
                userStory.CalculateFee(transaction);
            }
        }
    }
}
