using System;
using System.Globalization;
using MobilePay.Models;

namespace MobilePay.Formatters
{
    interface ITransactionFeeFormatter
    {
        string Format(Transaction transaction);
    }

    class TransactionFeeFormatter : ITransactionFeeFormatter
    {
        public string Format(Transaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException();

            if (transaction.EmptyLine)
                return "";

            if (transaction.Fee == null)
                throw new ArgumentNullException();

            return $"{transaction.Date:yyyy-MM-dd} {transaction.MerchantName}\t{transaction.Fee.Amount.ToString("N2", CultureInfo.InvariantCulture)}";
        }
    }
}
