using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VendingMachine
{
    public class Menu
    {
        private readonly Dictionary<string, MenuItem> menu = new Dictionary<string, MenuItem>();
        private readonly IConsoleWrapper consoleWrapper;
        public string Title { get; set; }
        public string Heading { get; set; }
        private string MenuString { get; set; }
        public string ExitOption { get; set; }

        public Menu(IConsoleWrapper consoleWrapper)
        {
            this.consoleWrapper = consoleWrapper;
        }

        public Menu(
            IConsoleWrapper consoleWrapper,
            string title,
            string heading)
            : this(consoleWrapper)
        {
            Title = title;
            Heading = heading;
        }

        public void Run()
        {
            bool show = true;
            while (show)
            {
                consoleWrapper.Clear();
                consoleWrapper.Write(MenuString);
                consoleWrapper.Write("Option> ");
                string input = consoleWrapper.ReadLine();

                if (input.Equals(ExitOption))
                {
                    menu[ExitOption].Invoke.DynamicInvoke();
                    show = false;
                    break;
                }

                try
                {
                    if (int.TryParse(input, out _))
                        menu[input].Invoke.DynamicInvoke(int.Parse(input));
                    else
                        menu[input].Invoke.DynamicInvoke();

                }
                catch (KeyNotFoundException)
                {
                    continue;
                }
                catch (Exception e)
                {
                    consoleWrapper.WriteLine(e.Message);
                }
                HoldAndClear();
            }
            HoldAndClear("\n\tPress any key to exit.");
        }

        private void BuildMenuString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"{Heading}\n\n");
            sb.Append($"{Title}\n");
            for (int i = 0; i < menu.Count; i++)
            {
                sb.AppendFormat("{0}) {1}.\n",
                    menu.ElementAt(i).Key,
                    menu.ElementAt(i).Value.Text);
            }

            MenuString = sb.ToString();
        }

        private void HoldAndClear(string message = "\n\n\tPress any key to continue...")
        {
            consoleWrapper.Write(message);
            consoleWrapper.ReadKey();
            consoleWrapper.Clear();
        }

        public void AddItem(string option, string text, Delegate action)
        {
            menu.Add(option, new MenuItem(text, action));
            BuildMenuString();
        }

        private class MenuItem
        {
            public string Text { get; private set; }
            public Delegate Invoke { get; private set; }
            public MenuItem(string text, Delegate method)
            {
                Text = text;
                Invoke = method;
            }
        }
    }
}
