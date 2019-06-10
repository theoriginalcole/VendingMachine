using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.models;

namespace VendingMachine.Modules
{
    /// <summary>
    /// Allows for the loading and vending of items.
    /// </summary>
    class ItemSelector
    {
        private Items Items;

        public ItemSelector()
        {
            Items = new Items();
        }

        /// <summary>
        /// Loads an item to the machine.  Assumes that there are unlimited slots.
        /// </summary>
        /// <param name="item"></param>
        public void loadItem(Item item)
        {
            if(Items.items.Exists(x => x.Name == item.Name && x.Price == item.Price))
            {
                var foundItem = Items.items.First(x => x.Name == item.Name && x.Price == item.Price);
                var index = Items.items.IndexOf(foundItem);
                foundItem.quantity += item.quantity;
                Items.items[index] = foundItem;
            }
            else
            {
                Items.items.Add(item);
            }
        }

        /// <summary>
        /// loads multiple items by calling loadItem.
        /// </summary>
        /// <param name="items"></param>
        public void loadItems(Items items)
        {
            foreach(var item in items.items)
            {
                if (item.quantity > 0 || item.Price > 0) //skips an item if quantity or price are not greater than 0 so we don't kick out of the loop.
                {
                    loadItem(item);
                }
            }
        }

        public Items AvailableItems()
        {
            return Items;
        }

        /// <summary>
        /// Locates the item selected, checks the quantity, and reduces quantity by 1.  Throws an exception if item is out of stock or doesn't exist.
        /// </summary>
        /// <param name="item"></param>
        public void VendItem(Item item)
        {
            if (Items.items.Exists(x => x.Name == item.Name && x.Price == item.Price))
            {
                var foundItem = Items.items.First(x => x.Name == item.Name && x.Price == item.Price);
                if (foundItem.quantity == 0)
                    throw new Exception("Out of stock.");
                var index = Items.items.IndexOf(foundItem);
                Items.items[index].quantity--;
            }
            else
            {
                throw new Exception("Invalid entry made");
            }
        }
    }
}
