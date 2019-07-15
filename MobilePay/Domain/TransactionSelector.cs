namespace MobilePay.Domain
{
    internal interface ITransactionSelector
    {
        void Proceed();
    }

    internal class TransactionSelector : ITransactionSelector
    {
        private readonly ITransactionFeeWriter _transactionFeeWriter;
        private readonly ITransactionReader _transactionReader;
        
        private readonly ITransactionCharger _transactionCharger;

        public TransactionSelector(ITransactionReader transactionReader, ITransactionFeeWriter transactionFeeWriter, ITransactionCharger transactionCharger)
        {
            _transactionReader = transactionReader;
            _transactionFeeWriter = transactionFeeWriter;
            _transactionCharger = transactionCharger;
        }

        public void Proceed()
        {
            while (true)
            {
                var transaction = _transactionReader.GetNextTransaction();

                if (transaction == null)
                    break;
                
                if(!transaction.EmptyLine)
                    _transactionCharger.Charge(transaction);

                _transactionFeeWriter.WriteNextTransactionFee(transaction);
            }
        }
    }
}