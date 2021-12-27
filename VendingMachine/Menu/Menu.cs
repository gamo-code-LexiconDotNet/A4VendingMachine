using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    namespace Calculator
    {
        public class Menu
        {
            private readonly Dictionary<string, MenuItem> menu = new Dictionary<string, MenuItem>();
            private readonly IConsoleWrapper consoleWrapper;
            public string Title { get; set; }
            public string Heading { get; set; }

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
                    PrintMenu();

                    string input = consoleWrapper.ReadLine();

                    if (input.Equals("0"))
                    {
                        show = false;
                        break;
                    }

                    try
                    {
                        menu[input].Act();
                    }
                    catch (KeyNotFoundException)
                    {
                        continue;
                    }
                    catch (Exception ex)
                    {
                        consoleWrapper.WriteLine(ex.Message);
                    }
                    HoldAndClear();
                }
            }

            private void PrintMenu()
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
                sb.Append("0) Exit.\n> ");

                consoleWrapper.Write(sb.ToString());
            }

            private void HoldAndClear(string message = "\n\n\tPress any key to continue...")
            {
                consoleWrapper.Write(message);
                consoleWrapper.ReadKey();
                consoleWrapper.Clear();
            }

            public void AddItem(string option, string text, Action action)
            {
                menu.Add(option, new MenuItem(text, action));
            }

            private class MenuItem
            {
                public string Text { get; private set; }
                public Action Act { get; private set; }
                public MenuItem(string text, Action action)
                {
                    Text = text;
                    Act = action;
                }
            }
        }
    }
}
