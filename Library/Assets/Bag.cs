using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Library.Domain;

namespace Library.Assets
{
    public class Bag
    {
        public Dictionary<Item, int> ItemsDictionary { get; private set; }

        public Bag()
        {
            foreach (Item item in Enum.GetValues(typeof(Item)))
            {
                ItemsDictionary.Add(item, 0);
            }
        }

        public void AddItems(Item item)
        {
            AddItems(item, 1);
        }

        public void RemoveItems(Item item, int amount)
        {
            if (ItemsDictionary[item] - amount < 0) {
                ItemsDictionary[item] = 0;
                Trace.TraceError("Cannot have less than 0 of an item.");
            }
            else
            {
                ItemsDictionary[item] = ItemsDictionary[item] - amount;
            }
        }

        public void RemoveItem(Item item)
        {
            RemoveItems(item, 1);
        }

        public void AddItems(Item item, int amount)
        {
            ItemsDictionary[item] = ItemsDictionary[item] + amount;
        }
    }
}
