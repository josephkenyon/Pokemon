using System.Collections.Generic;
using System.Diagnostics;
using Library.Domain;

namespace Library.Assets
{
    public class Bag
    {
        public Dictionary<ItemType, int> ItemsDictionary { get; private set; }

        public Bag()
        {
            ItemsDictionary = new Dictionary<ItemType, int>();
        }

        public void AddItem(ItemType item)
        {
            AddItems(item, 1);
        }

        public void AddItems(ItemType item, int amount)
        {
            if (!ItemsDictionary.ContainsKey(item))
            {
                ItemsDictionary.Add(item, 0);
            }

            ItemsDictionary[item] = ItemsDictionary[item] + amount;
        }

        public void RemoveItem(ItemType item)
        {
            RemoveItems(item, 1);
        }

        public void RemoveItems(ItemType item, int amount)
        {
            if (!ItemsDictionary.ContainsKey(item))
            {
                ItemsDictionary.Add(item, 0);
            }

            if (ItemsDictionary[item] - amount < 0)
            {
                ItemsDictionary[item] = 0;
                Trace.TraceError("Cannot have less than 0 of an item.");
            }
            else
            {
                ItemsDictionary[item] = ItemsDictionary[item] - amount;
            }
        }
    }
}
