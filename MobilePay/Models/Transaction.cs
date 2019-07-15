    using System;

    namespace MobilePay.Models
    {
        class Transaction
        {
            public DateTime Date { get; set; }
            public string MerchantName { get; set; }
            public decimal Amount { get; set; }
            public TransactionFee Fee { get; set; }
            public bool EmptyLine { get; set; }
        }
    }
