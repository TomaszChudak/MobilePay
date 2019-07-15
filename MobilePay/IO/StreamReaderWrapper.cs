using System.IO;
using Microsoft.Extensions.Options;
using MobilePay.Configuration;

namespace MobilePay.IO
{
    interface IStreamReaderWrapper
    {
        TextReader GetStreamReader();
    }

    internal class StreamReaderWrapper : IStreamReaderWrapper
    {
        private readonly TextReader _textReader;

        public StreamReaderWrapper(IOptions<AppSettings> config)
        {
            var fileName = config.Value.TransactionFilePath;
            _textReader = new StreamReader(fileName);
        }

        public TextReader GetStreamReader()
        {
            return _textReader;
        }
    }
}