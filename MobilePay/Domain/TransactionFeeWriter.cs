using MobilePay.Formatters;
using MobilePay.IO;
using MobilePay.Models;

namespace MobilePay.Domain
{
    internal interface ITransactionFeeWriter
    {
        void WriteNextTransactionFee(Transaction transaction);
    }

    class TransactionFeeWriter : ITransactionFeeWriter
    {
        private readonly IConsoleWriter _consoleWriter;
        private readonly ITransactionFeeFormatter _transactionFeeFormatter;

        public TransactionFeeWriter(IConsoleWriter consoleWriter, ITransactionFeeFormatter transactionFeeFormatter)
        {
            _consoleWriter = consoleWriter;
            _transactionFeeFormatter = transactionFeeFormatter;
        }

        public void WriteNextTransactionFee(Transaction transaction)
        {
            var formattedTransactionFee = _transactionFeeFormatter.Format(transaction);
            _consoleWriter.WriteLine(formattedTransactionFee);
        }
    }
}
