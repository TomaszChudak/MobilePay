using System.Collections.Generic;

namespace MobilePay.IO
{
    internal interface IFileReader
    {
        IEnumerable<string> ReadTransactions();
    }

    internal class FileReader : IFileReader
    {
        private readonly IStreamReaderWrapper _streamReaderWrapper;

        public FileReader(IStreamReaderWrapper streamReaderWrapper)
        {
            _streamReaderWrapper = streamReaderWrapper;
        }

        public IEnumerable<string> ReadTransactions()
        {
            var streamReader = _streamReaderWrapper.GetStreamReader();
            using (streamReader)
            {
                string currentLine;
                while ((currentLine = streamReader.ReadLine()) != null) yield return currentLine;
            }
        }
    }
}