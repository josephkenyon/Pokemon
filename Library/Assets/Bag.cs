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
        public Dictionary<ItemType, int> ItemsDictionary { get; private set; }
        [JsonProperty]
        public Dictionary<KeyItemType, int> KeyItemsDictionary { get; private set; }
        [JsonProperty]
        public Dictionary<PokeBallType, int> PokeBallsDictionary { get; private set; }

        public Bag()
        {
            ItemsDictionary = new Dictionary<ItemType, int>();
            KeyItemsDictionary = new Dictionary<KeyItemType, int>();
            PokeBallsDictionary = new Dictionary<PokeBallType, int>();
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

        public void AddPokeBall(PokeBallType item)
        {
            AddPokeBalls(item, 1);
        }

        public void AddPokeBalls(PokeBallType item, int amount)
        {
            if (!PokeBallsDictionary.ContainsKey(item))
            {
                PokeBallsDictionary.Add(item, 0);
            }

            PokeBallsDictionary[item] = PokeBallsDictionary[item] + amount;
        }

        public void AddKeyItem(KeyItemType item)
        {
            AddKeyItems(item, 1);
        }

        public void AddKeyItems(KeyItemType item, int amount)
        {
            if (!KeyItemsDictionary.ContainsKey(item))
            {
                KeyItemsDictionary.Add(item, 0);
            }

            KeyItemsDictionary[item] = KeyItemsDictionary[item] + amount;
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

        public void RemovePokeBall(PokeBallType item)
        {
            RemovePokeBalls(item, 1);
        }

        public void RemovePokeBalls(PokeBallType item, int amount)
        {
            if (!PokeBallsDictionary.ContainsKey(item))
            {
                PokeBallsDictionary.Add(item, 0);
            }

            if (PokeBallsDictionary[item] - amount < 0)
            {
                PokeBallsDictionary[item] = 0;
                Trace.TraceError("Cannot have less than 0 of an item.");
            }
            else
            {
                PokeBallsDictionary[item] = PokeBallsDictionary[item] - amount;
            }
        }
    }
}
