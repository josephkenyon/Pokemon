using Library.Domain;
using Library.GameState.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;

namespace Library.GameState.Battle.GamePadHelpers
{
    public static class AshSelectHelper
    {
        public static int SelectedIndex { get; set; }
        public static BattleMenuItem[] ItemList => (BattleMenuItem[])Enum.GetValues(typeof(BattleMenuItem));

        public static void Update()
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

            Direction? direction = GamePadHelper.GetDPadDirection(gamePadState);

            if (direction != null)
            {
                if ((Direction)direction == Direction.Left)
                {
                    SelectedIndex--;
                    if (SelectedIndex < 0)
                    {
                        SelectedIndex = ItemList.Length - 1;
                    }
                    GameStateManager.Instance.InputDebounceTimer = Constants.ItemDebounce;
                }
                else if ((Direction)direction == Direction.Right)
                {
                    SelectedIndex++;
                    if (SelectedIndex == ItemList.Length)
                    {
                        SelectedIndex = 0;
                    }

                    GameStateManager.Instance.InputDebounceTimer = Constants.ItemDebounce;
                }
                Debug.WriteLine(ItemList[SelectedIndex].ToString());
            }

            if (gamePadState.Buttons.A == ButtonState.Pressed)
            {
                switch (ItemList[SelectedIndex])
                {
                    case BattleMenuItem.Fight:
                        BattleStateManager.Battle.SwitchToState(BattleState.FightSelect);
                        break;
                    case BattleMenuItem.Pokemon:
                        BattleStateManager.Battle.SwitchToState(BattleState.PokemonSelect);
                        break;
                    case BattleMenuItem.Bag:
                        BattleStateManager.Battle.SwitchToState(BattleState.ItemSelect);
                        break;
                    case BattleMenuItem.Run:
                        GameStateManager.Instance.UIState = UIState.Base;
                        break;
                };

                GameStateManager.Instance.InputDebounceTimer = Constants.MenuActivationDebounce;
            }
        }
    }
}