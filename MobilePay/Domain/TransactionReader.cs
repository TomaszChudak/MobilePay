using MobilePay.Formatters;
using MobilePay.IO;
using MobilePay.Models;

namespace MobilePay.Domain
{
    internal interface ITransactionReader
    {
        Transaction GetNextTransaction();
    }

    internal class TransactionReader : ITransactionReader
    {
        private readonly IFileReader _fileReader;
        private readonly ITransactionFormatter _transactionFormatter;

        public TransactionReader(IFileReader fileReader, ITransactionFormatter transactionFormatter)
        {
            _fileReader = fileReader;
            _transactionFormatter = transactionFormatter;
        }

        public Transaction GetNextTransaction()
        {
            var fileReaderTransactionsEnumerator = _fileReader.ReadTransactions().GetEnumerator();
            if (!fileReaderTransactionsEnumerator.MoveNext())
                return null;

            var transaction = _transactionFormatter.Format(fileReaderTransactionsEnumerator.Current);

            return transaction;
        }
    }
}