using System.Collections.Generic;
using Microsoft.Extensions.Options;
using MobilePay.Configuration;
using MobilePay.Models;

namespace MobilePay.UserStories.UserStory5
{
    internal interface IMonthlyCharger
    {
        decimal GetMonthlyFee(Transaction transaction);
    }

    internal class MonthlyCharger : IMonthlyCharger
    {
        private readonly decimal _monthlyFee;
        private int _previousMonth;
        private HashSet<string> _chargedMerchants;

        public MonthlyCharger(IOptions<AppSettings> config)
        {
            _previousMonth = -1;
            _monthlyFee = config.Value.InvoiceFixedFee;
        }

        public decimal GetMonthlyFee(Transaction transaction)
        {
            if (_previousMonth != transaction.Date.Month)
            {
                _chargedMerchants = new HashSet<string>();
                _previousMonth = transaction.Date.Month;
            }

            if (_chargedMerchants.Contains(transaction.MerchantName))
                return 0;

            _chargedMerchants.Add(transaction.MerchantName);

            return _monthlyFee;
        }
    }
}