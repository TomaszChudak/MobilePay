using System;

namespace MobilePay.IO
{
    interface IConsoleWriter
    {
        void WriteLine(string line);
    }

    class ConsoleWriter : IConsoleWriter
    {
        public void WriteLine(string line)
        {
            Console.WriteLine(line);
        }
    }
}
