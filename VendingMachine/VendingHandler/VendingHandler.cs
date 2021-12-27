using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    public class VendingHandler
    {
        private readonly IConsoleWrapper consoleWrapper;
        private readonly IVending vending;

        public VendingHandler(IConsoleWrapper consoleWrapper, IVending vending)
        {
            this.consoleWrapper = consoleWrapper;
            this.vending = vending;
        }

        public void InsertMoney()
        {
            try
            {
                int money = int.Parse(consoleWrapper.ReadLine());
                vending.InsertMoney(money);
            } catch (DenominationException e)
            {
                consoleWrapper.WriteLine(e.Message);
            } catch (FormatException e)
            {
                consoleWrapper.WriteLine(e.Message);
            } catch 
            {
                consoleWrapper.WriteLine("Nope, try something else.");
            }
        }

        public void Purchase()
        {
            try
            {
                int index = int.Parse(consoleWrapper.ReadLine());
                string use = vending.Purchase(index);
                consoleWrapper.WriteLine(use);
            }
            catch (OutOfMoneyException e)
            {
                consoleWrapper.WriteLine(e.Message);
            }
            catch (FormatException e)
            {
                consoleWrapper.WriteLine(e.Message);
            }
            catch
            {
                consoleWrapper.WriteLine("Nope, try something else.");
            }
        }

        // not really used
        public void ShowAll()
        {
            foreach (string s in vending.ShowAll())
            {
                consoleWrapper.WriteLine(s);
            }
        }

        // for passing infos to menu to be menuitems
        public string[] getInfos()
        {
            return vending.ShowAll();
        }

        public void InserMoney()
        {
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
            int[] denominations = vending.Denominations;
            int[] change = vending.EndTransaction();

            consoleWrapper.WriteLine("You change:");
            for (int i = 0; i < denominations.Length; i++)
            {
                if (change[i] != 0)
                {
                    consoleWrapper.WriteLine("[${0}] x {1}", 
                        denominations[i], change[i]);
                }
            }
            consoleWrapper.WriteLine("Thank you for your bussiness, come back soon.");
        }
    }

}
