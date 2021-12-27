using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    abstract public class Product
    {
        public string Name { get; protected set; }
        public int Price { get; protected set; }
        public string Examine { get; protected set; }
        abstract public string Use { get; }

        protected Product(string name, int price)
        {
            Name = name;
            Price = price;
            Examine = name + ": $" + price;
        }
    }
}
