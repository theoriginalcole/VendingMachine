using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.models;
using VendingMachine.Modules;

namespace VendingMachine
{
    class Program
    {
        private static ChangeMaker changeMaker;
        private static CoinReceiver coinReceiver;
        private static ItemSelector itemSelector;

        static void Main(string[] args)
        {
            //Initialize Modules
            changeMaker = new ChangeMaker();
            coinReceiver = new CoinReceiver();
            itemSelector = new ItemSelector();

            adminPanelSelection();
        }

        public static void  adminPanelSelection()
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("To load change, press 1.");
            Console.WriteLine("To empty change, press 2.");
            Console.WriteLine("To load a specific item, press 3");
            Console.WriteLine("To load a list of items, press 4");
            Console.WriteLine("To place the machine in vend mode, press 5");
            int selection = 0;
            Int32.TryParse(Console.ReadLine(), out selection);
            if (selection < 1 || selection > 5)
            {
                Console.WriteLine("Invalid Selection. Please try again.");
                adminPanelSelection(); //Could throw a stack overflow execption here if user selects wrong item too many times
            }
            //all values being passed are being coded here instead of asking for input to simplify both coding and testing
            switch (selection)
            {
                case 1:
                    ChangeHolder change = new ChangeHolder();
                    change.Quarter.quantity = 25;
                    change.Dime.quantity = 30;
                    change.Nickel.quantity = 40;
                    change.Penny.quantity = 100;
                    loadChange(change);
                    break;
                case 2:
                    emptyChange();
                    break;
                case 3:
                    loadItem(new Item() { Name = "Pepsi", quantity = 10, Price = .89M });
                    break;
                case 4:
                    Items items = new Items();
                    items.items.Add(new Item() { Name = "CocaCola", Price = .87M, quantity = 12 });
                    items.items.Add(new Item() { Name = "Diet Pepsi", Price = .96M, quantity = 1 });
                    items.items.Add(new Item() { Name = "Diet Coke", Price = .96M, quantity = 3 });
                    loadItems(items);
                    break;
                case 5:
                    VendMode();
                    break;
            }
        }

        /// <summary>
        /// Loads Change to machine.  Assumes that only one of each item is in the list.
        /// </summary>
        /// <param name="change"></param>
        public static void loadChange(ChangeHolder change)
        {
            changeMaker.LoadChange(change);
            adminPanelSelection();
        }

        public static void emptyChange()
        {
            changeMaker.EmptyChange();
            adminPanelSelection();
        }

        public static void loadItem(Item item)
        {
            itemSelector.loadItem(item);
            adminPanelSelection();
        }

        public static void loadItems(Items items)
        {
            itemSelector.loadItems(items);
            adminPanelSelection();
        }

        /// <summary>
        /// Change must be inserted before being able to select an item.  Machine will check if there is enough change to make the purchase.
        /// </summary>
        public static void VendMode()
        {
            Items availableItems = new Items();
            availableItems = itemSelector.AvailableItems();

            Console.WriteLine("Please insert change. Q for quarter, D for dime, N for nickel, P for penny. Press enter when done");
            string changeInput = Console.ReadLine();
            List<Change> change = new List<Change>();
            ChangeHolder changeHolder = new ChangeHolder();
            decimal inserted = 0M;
            foreach (var character in changeInput)
            {
                switch(character)
                {
                    case 'Q':
                        changeHolder.Quarter.quantity++;
                        inserted += changeHolder.Quarter.value;
                        break;
                    case 'D':
                        changeHolder.Dime.quantity++;
                        inserted += changeHolder.Dime.value;
                        break;
                    case 'N':
                        changeHolder.Nickel.quantity++;
                        inserted += changeHolder.Nickel.value;
                        break;
                    case 'P':
                        changeHolder.Penny.quantity++;
                        inserted += changeHolder.Penny.value;
                        break;
                    default:
                        break;
                }
            }

            int index = 0;
            foreach(Item item in availableItems.items)
            {
                if(item.quantity > 0)
                {
                    Console.WriteLine(index + " - " + item.Name + " - " + item.Price);
                }
                index++;
            }
            Console.WriteLine("X to shutdown or A to enter admin panel.");
            int selectedIndex = -1;
            while (selectedIndex == -1)
            {
                string input = Console.ReadLine();
                if (input == "X")
                    return;
                if (input == "A")
                    adminPanelSelection();
                else
                {
                    Int32.TryParse(input, out selectedIndex);
                    if (selectedIndex == -1)
                    {
                        Console.WriteLine("invalid selection, please try again");
                    }
                }
            }
            Item selectedItem = new Item();
            selectedItem = availableItems.items[selectedIndex];
            try
            {
                if (changeMaker.EnoughChange(inserted, selectedItem.Price))
                {
                    itemSelector.VendItem(selectedItem);
                    ChangeHolder changeGiven = new ChangeHolder();
                    changeGiven = changeMaker.MakeChange(inserted, selectedItem.Price);
                    Console.WriteLine("Change given");
                    Console.WriteLine("Quarters: " + changeGiven.Quarter.quantity.ToString());
                    Console.WriteLine("Dimes: " + changeGiven.Dime.quantity.ToString());
                    Console.WriteLine("Nickels: " + changeGiven.Nickel.quantity.ToString());
                    Console.WriteLine("Pennies: " + changeGiven.Penny.quantity.ToString());
                    changeMaker.LoadChange(changeHolder); //load the coins inserted into the change maker
                    Console.WriteLine("Item vended");

                }

                else
                {
                    Console.WriteLine("Not enough change.  Returning inserted change.");
                    changeHolder = new ChangeHolder();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            VendMode();
        }

    }
}
