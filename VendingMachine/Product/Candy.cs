using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    public class Candy : Product
    {
        public Candy(string name, int price) :
            base(name, price) { }

        public override string Use
        {
            get
            { 
                return "Eat the " + Name + ", sugar rush.";
            }
        }
    }
}
