using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    public class DenominationException : Exception
    {
        public DenominationException()
            : base("Not a valid denomination.") { }
        public DenominationException(string message)
            : base(message) { }
    }

    public class OutOfMoneyException : Exception
    {
        public OutOfMoneyException()
            : base("Out of money, insert more.") { }
        public OutOfMoneyException(
            string message)
            : base(message) { }
    }
}
