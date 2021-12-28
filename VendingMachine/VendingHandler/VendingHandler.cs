using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    public class VendingHandler : IVendingHandler
    {
        private readonly IConsoleWrapper consoleWrapper;
        private readonly IVending vending;

        public VendingHandler(IConsoleWrapper consoleWrapper, IVending vending)
        {
            this.consoleWrapper = consoleWrapper;
            this.vending = vending;
        }

        public void Purchase(int index)
        {
            try
            {
                string use = vending.Purchase(index);
                consoleWrapper.WriteLine("\n" + use);
            }
            catch (OutOfMoneyException e)
            {
                consoleWrapper.WriteLine(e.Message);
            }
            catch
            {
                consoleWrapper.WriteLine("Nope, try something else.");
            }
        }

        // present for adherence to spec, not really used
        public void ShowAll()
        {
            foreach (string s in vending.ShowAll())
            {
                consoleWrapper.WriteLine(s);
            }
        }

        public string[] GetInfos()
        {
            return vending.ShowAll();
        }

        public void MoneyPool()
        {
            consoleWrapper.WriteLine("\n$" + vending.MoneyPool.ToString());
        }

        public void InsertMoney()
        {
            consoleWrapper.Write("\nInsert money: ");
            try
            {
                int money = int.Parse(consoleWrapper.ReadLine());
                vending.InsertMoney(money);
            }
            catch (DenominationException)
            {
                consoleWrapper.WriteLine("Insert only money in valid denominations.");
            }
            catch (FormatException)
            {
                consoleWrapper.WriteLine("Money must be a number.");
            }
            catch
            {
                consoleWrapper.WriteLine("Nope, try something else.");
            }

        }

        public void EndTransaction()
        {
            if (vending.MoneyPool > 0)
            {
                consoleWrapper.WriteLine($"\nYour change: ${vending.MoneyPool}");
                int[] denominations = vending.Denominations;
                int[] change = vending.EndTransaction();
                for (int i = 0; i < denominations.Length; i++)
                {
                    if (change[i] != 0)
                    {
                        consoleWrapper.WriteLine("[${0}] x {1}", 
                            denominations[i], change[i]);
                    }
                }
            }

            consoleWrapper.WriteLine("\nThank you for your bussiness, come back soon.");
        }
    }

}
