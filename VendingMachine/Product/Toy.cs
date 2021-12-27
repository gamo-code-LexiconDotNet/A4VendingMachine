using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    public class Toy : Product
    {
        public Toy(string name, int price) :
            base(name, price) { }

        public override string Use
        {
            get
            {
                return "Play with the " + Name + ", fun.";
            }
        }
    }
}
