using System.Collections.Generic;

namespace MobilePay.Configuration
{
    class AppSettings
    {
        public string TransactionFilePath { get; set; }
        public decimal InvoiceFixedFee { get; set; }
        public IEnumerable<int> RequiredUserStories { get; set; }
    }
}
