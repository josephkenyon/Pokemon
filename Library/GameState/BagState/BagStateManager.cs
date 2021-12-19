using Library.Assets;
using Library.Domain;
using Library.GameState.Battle;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using System.Collections.Generic;
using Library.Content;

namespace Library.GameState.BagState
{
    public class BagStateManager : IStateManager
    {
        public static ItemType BagState { get; set; }
        public static int ItemIndex { get; set; }
        public static BagStateManager Instance { get; set; }
        public static Bag Bag => GameStateManager.Instance.GetPlayer().CharacterState.Bag;

        public BagStateManager()
        {
            Instance = this;
        }

        public static List<ItemName> AvailableItems => Bag.ItemsDictionary.Keys.Where(item => ItemManager.GetItemType(item) == BagState).ToList();

        public static ItemName? UseSelectedItem()
        {
            List<ItemName> itemNames = AvailableItems;
            if (ItemIndex > -1 && ItemIndex < itemNames.Count)
            {
                ItemName itemName = itemNames.ElementAt(ItemIndex);
                if (Bag.ItemsDictionary[itemName] > 0)
                {
                    Bag.ItemsDictionary[itemName]--;
                    return itemName;
                }
            }

            return null;
        }

        public bool Update()
        {
            BagStateGamePadHandler.Update();
            return true;
        }

        public void Draw(SpriteBatch spriteBatch) {
            BagStateDrawingManager.Draw(spriteBatch);
        }

        public static List<ItemType> AllBagStates => new List<ItemType>((IEnumerable<ItemType>)Enum.GetValues(typeof(ItemType)));

        public static List<ItemType> GetAllowedBagStates()
        {
            List<ItemType> list = new List<ItemType>
            {
                ItemType.Generic_Item
            };

            if (BattleStateManager.Battle == null)
            {
                list.Add(ItemType.Key_Item);
            }

            if (BattleStateManager.Battle == null || BattleStateManager.IsWildPokemonBattle)
            {
                list.Add(ItemType.Poke_Ball);
            }

            return list;
        }
    }
}
