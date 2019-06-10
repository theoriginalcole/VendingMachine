using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.models
{
    public class Item
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int quantity { get; set; }
    }

    public class Items
    {
        public Items()
        {
            items = new List<Item>();
        }
        public List<Item> items { get; set; }
    }
}
