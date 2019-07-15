using System;
using System.Globalization;
using MobilePay.Models;

namespace MobilePay.Formatters
{
    internal interface ITransactionFormatter
    {
        Transaction Format(string transaction);
    }

    internal class TransactionFormatter : ITransactionFormatter
    {
        public Transaction Format(string transaction)
        {
            if(transaction == null)
                throw new ArgumentNullException();

            if (string.IsNullOrWhiteSpace(transaction))
                return new Transaction {EmptyLine = true};

            var transactionParts = transaction.Split(new [] {' ', '\t'}, StringSplitOptions.RemoveEmptyEntries);

            var date = DateTime.ParseExact(transactionParts[0], "yyyy-MM-dd", CultureInfo.InvariantCulture);
            var merchantName = transactionParts[1];
            var amount = decimal.Parse(transactionParts[2], CultureInfo.InvariantCulture);

            return new Transaction {Date = date, MerchantName = merchantName, Amount = amount};
        }
    }
}
