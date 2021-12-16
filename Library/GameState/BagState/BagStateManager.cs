using Library.Assets;
using Library.Domain;
using Library.GameState.Battle;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Library.GameState.BagState
{
    public class BagStateManager : IStateManager
    {
        public static BagStateState BagState { get; set; }
        public static int ItemIndex { get; set; }
        public static BagStateManager Instance { get; set; }
        public static Bag Bag => GameStateManager.Instance.GetPlayer().CharacterState.Bag;

        public BagStateManager()
        {
            Instance = this;
        }

        public static ItemType? UseSelectedItem()
        {
            if (BagState == BagStateState.Items) {
                if (ItemIndex > -1 && ItemIndex < Bag.ItemsDictionary.Count)
                {
                    ItemType itemType = Bag.ItemsDictionary.Keys.ElementAt(ItemIndex);
                    if (Bag.ItemsDictionary[itemType] > 0)
                    {
                        Bag.ItemsDictionary[itemType]--;
                        return Bag.ItemsDictionary.Keys.ElementAt(ItemIndex);
                    }
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

        public static List<BagStateState> AllBagStates => new List<BagStateState>((IEnumerable<BagStateState>)Enum.GetValues(typeof(BagStateState)));

        public static List<BagStateState> GetAllowedBagStates()
        {
            List<BagStateState> list = new List<BagStateState>
            {
                BagStateState.Items
            };

            if (BattleStateManager.Battle == null)
            {
                list.Add(BagStateState.Key_Items);
            }

            if (BattleStateManager.Battle == null || BattleStateManager.IsWildPokemonBattle)
            {
                list.Add(BagStateState.Poke_Balls);
            }

            return list;
        }
    }
}
