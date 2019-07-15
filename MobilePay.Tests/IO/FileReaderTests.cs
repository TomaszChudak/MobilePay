using System.IO;
using FluentAssertions;
using MobilePay.IO;
using Moq;
using Xunit;

namespace MobilePay.Tests.IO
{
    public class FileReaderTests
    {
        public FileReaderTests()
        {
            _streamReaderWrapperMock = new Mock<IStreamReaderWrapper>();
            _sut = new FileReader(_streamReaderWrapperMock.Object);
        }

        private readonly IFileReader _sut;

        private readonly Mock<IStreamReaderWrapper> _streamReaderWrapperMock;

        [Fact]
        public void ReadTransactions_ThereIsContent_ReturnLine()
        {
            _streamReaderWrapperMock.Setup(x => x.GetStreamReader())
                .Returns(new StreamReader(Stream.Null));
            _streamReaderWrapperMock.Setup(x => x.GetStreamReader().ReadLine())
                .Returns("2018-09-01 7-ELEVEN 100");

            var result = "aaaa";

            var enumerator = _sut.ReadTransactions().GetEnumerator();
            using (enumerator)
            {
                enumerator.MoveNext();
                result = enumerator.Current;
            }

            result.Should().Be("2018-09-01 7-ELEVEN 100");
        }

        [Fact]
        public void ReadTransactions_ThereIsNoContent_ReturnNull()
        {
            _streamReaderWrapperMock.Setup(x => x.GetStreamReader())
                .Returns(new StreamReader(Stream.Null));
            _streamReaderWrapperMock.Setup(x => x.GetStreamReader().ReadLine())
                .Returns((string) null);

            var result = "aaaa";

            var enumerator = _sut.ReadTransactions().GetEnumerator();
            using (enumerator)
            {
                enumerator.MoveNext();
                result = enumerator.Current;
            }

            result.Should().BeNull();
        }
    }
}