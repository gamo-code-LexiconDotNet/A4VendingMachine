using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    public class Food : Product
    {
        public Food(string name, int price) :
            base(name, price) { }

        public override string Use
        {
            get
            {
                return "Eat the " + Name + ", nourishing.";
            }
        }
    }
}
