using System;
using System.Collections.Generic;
using System.Diagnostics;
using Library.Domain;
using Newtonsoft.Json;

namespace Library.Assets
{
    public class Bag
    {
        [JsonProperty]
        public Dictionary<ItemName, int> ItemsDictionary { get; private set; }

        public Bag()
        {
            ItemsDictionary = new Dictionary<ItemName, int>();
        }

        public void AddItem(ItemName item)
        {
            AddItems(item, 1);
        }

        public void AddItems(ItemName item, int amount)
        {
            if (!ItemsDictionary.ContainsKey(item))
            {
                ItemsDictionary.Add(item, 0);
            }

            ItemsDictionary[item] = ItemsDictionary[item] + amount;
        }

        public void RemoveItem(ItemName item)
        {
            RemoveItems(item, 1);
        }

        public void RemoveItems(ItemName item, int amount)
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
