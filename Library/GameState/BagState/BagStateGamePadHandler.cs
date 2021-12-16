using Library.Content;
using Library.Domain;
using Library.GameState.Battle;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Library.GameState.BagState
{
    public static class BagStateGamePadHandler
    {
        public static void Update()
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

            if (gamePadState.Buttons.A == ButtonState.Pressed && GameStateManager.Instance.InputDebounceTimer == 0)
            {
                if (BattleStateManager.Battle != null)
                {
                    BattleStateManager.Battle.SwitchToState(BattleState.PokemonSelect);
                }

                GameStateManager.Instance.UIStateStack.Pop();
            }
            else if (gamePadState.Buttons.B == ButtonState.Pressed && GameStateManager.Instance.InputDebounceTimer == 0)
            {
                if (BattleStateManager.Battle != null)
                {
                    BattleStateManager.Battle.SwitchToPreviousState();
                }

                GameStateManager.Instance.UIStateStack.Pop();
            }
            else if (gamePadState.DPad.Down == ButtonState.Pressed && GameStateManager.Instance.InputDebounceTimer == 0)
            {
                BagStateManager.ItemIndex++;
            }
            else if (gamePadState.DPad.Up == ButtonState.Pressed && GameStateManager.Instance.InputDebounceTimer == 0)
            {
                if (BagStateManager.ItemIndex > 0)
                {
                    BagStateManager.ItemIndex--;
                }
            }
            else if (gamePadState.DPad.Left == ButtonState.Pressed && GameStateManager.Instance.InputDebounceTimer == 0)
            {
                int index = (int)BagStateManager.BagState;
                if (index != 0)
                {
                    BagStateState potentialBagState = BagStateManager.AllBagStates[index - 1];
                    if (BagStateManager.GetAllowedBagStates().Contains(potentialBagState))
                    {
                        BagStateManager.ItemIndex = 0;
                        BagStateManager.BagState = potentialBagState;
                    }
                }
            }
            else if (gamePadState.DPad.Right == ButtonState.Pressed && GameStateManager.Instance.InputDebounceTimer == 0)
            {
                int index = (int)BagStateManager.BagState;
                if (index != 2)
                {
                    BagStateState potentialBagState = BagStateManager.AllBagStates[index + 1];
                    if (BagStateManager.GetAllowedBagStates().Contains(potentialBagState))
                    {
                        BagStateManager.ItemIndex = 0;
                        BagStateManager.BagState = potentialBagState;
                    }
                }
            }
            else
            {
                return;
            }

            GameStateManager.Instance.InputDebounceTimer = Constants.MenuActivationDebounce;
        }
    }
}