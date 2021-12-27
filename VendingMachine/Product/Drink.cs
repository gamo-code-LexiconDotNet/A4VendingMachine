using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    public class Drink : Product
    {
        public Drink(string name, int price) :
           base(name, price) { }

        public override string Use
        {
            get
            {
                return "Drink the " + Name + ", refreshing.";
            }
        }
    }
}
