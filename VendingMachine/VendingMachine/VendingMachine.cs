using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    public class VendingMachine
    {
        private readonly VendingHandler vendingHandler;
        private readonly Menu menu;

        public VendingMachine()
        {
            menu = new Menu(
                new ConsoleWrapper());

            vendingHandler = new VendingHandler(
                new ConsoleWrapper(),
                CreateVending());

            SetupMenu();
        }

        public void Start()
        {
            menu.Run();
        }

        public void SetupMenu()
        {
            string[] menuItems = vendingHandler.GetInfos();

            menu.Heading = "[Lexicon C#/.Net Programming] Assignment 4 - Vending Machine]";
            menu.Title = "Buy:";
            menu.ExitOption = "e";

            for (int i = 0; i < menuItems.Length; i++)
            {
                menu.AddItem(
                    i.ToString(),
                    menuItems[i],
                    new Action<int>((i) => vendingHandler.Purchase(i)));
            }
            menu.AddItem("i", "Insert Money", new Action(vendingHandler.InsertMoney));
            menu.AddItem("m", "Money in machine", new Action(vendingHandler.MoneyPool));
            menu.AddItem("e", "End transaction", new Action(vendingHandler.EndTransaction));
        }

        private Vending CreateVending()
        {
            Vending vending = new Vending();

            vending.AddProduct(new IceCream("Gelato Chocolato", 25));
            vending.AddProduct(new IceCream("Cherry Ice", 18));
            vending.AddProduct(new Toy("Rubber Ball", 35));
            vending.AddProduct(new Toy("Tux :)", -10));
            vending.AddProduct(new Drink("Coffee", 25));
            vending.AddProduct(new Drink("Tea", 20));
            vending.AddProduct(new Drink("Soda Water", 18));
            vending.AddProduct(new Food("BLT Sandwich", 45));
            vending.AddProduct(new Food("Crisps", 22));
            vending.AddProduct(new Candy("Jelly Beans", 9));
            vending.AddProduct(new Candy("Gummy Bears", 12));

            return vending;
        }
    }
}
