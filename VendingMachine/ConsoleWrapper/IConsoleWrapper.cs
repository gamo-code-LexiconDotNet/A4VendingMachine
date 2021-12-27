using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    public interface IConsoleWrapper
    {
        string ReadLine();
        char ReadKey();
        void WriteLine(string value);
        public void WriteLine(string format, params object[] args);
        void Write(string value);
        void Write(string format, params object[] args);
        void Clear();
    }
}
