using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace VendingMachine
{
    public class Vending : IVending
    {
        public int[] Denominations { get; private set; }
        public int MoneyPool { get; private set; }
        private List<Product> products = new List<Product>();

        public Vending()
        {
            Denominations = new int[] { 1, 5, 10, 20, 50, 100, 500, 1000 };
            
            // for return change in EndTransaction
            // denomination array must be ordered descending
            Array.Sort(Denominations);
            Array.Reverse(Denominations);
        }

        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public void AddProduct(Product[] products)
        {
            foreach (Product product in products)
            {
                AddProduct(product);
            }   
        }

        public string Purchase(int index)
        {
            if (MoneyPool >= products[index].Price)
            {
                MoneyPool -= products[index].Price;
                return products[index].Use;
            }
            else
            {
                throw new OutOfMoneyException();
            }
        }

        public string[] ShowAll()
        {
            string[] productsInfos = new string[products.Count];
            for (int i = 0; i < products.Count; i++)
            {
                productsInfos[i] = products[i].Examine;
            }
            return productsInfos;
        }
        public void InsertMoney(int money)
        {
            if (Denominations.Contains(money))
                MoneyPool += money;
            else
                throw new DenominationException();
        }
        public int[] EndTransaction()
        {
            int[] change = new int[Denominations.Length];

            for (int i = 0; i < Denominations.Length; i++)
            {
                int qoutient = MoneyPool / Denominations[i];
                MoneyPool -= Denominations[i] * qoutient;
                change[i] = qoutient;
            }

            return change;
        }
    }
}
