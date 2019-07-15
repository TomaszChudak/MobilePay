using System;

namespace MobilePay.Models
{
    class TransactionFee
    {
        public decimal? BasicFee { get; set; }
        public decimal? AdditionalFee { get; set; }
        public decimal Amount
        {
            get { return Math.Round(BasicFee.Value + AdditionalFee.Value, 2); }
        }
    }
}