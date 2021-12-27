using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    public class DenominationException : Exception
    {
        public DenominationException() { }
        public DenominationException(
            string message = "Not a valid denomination")
            : base(message) { }
    }

    public class OutOfMoneyException : Exception
    {
        public OutOfMoneyException() { }
        public OutOfMoneyException(
            string message = "Out of money")
            : base(message) { }
    }
}
