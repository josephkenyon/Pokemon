using Library.Controls;
using Library.Domain;
using Library.GameState.Battle;

namespace Library.GameState.BagState
{
    public static class BagStateGamePadHandler
    {
        public static void Update()
        {
            if (ControlsManager.APressed() && GameStateManager.Instance.InputDebounceTimer == 0)
            {
                if (BattleStateManager.Battle != null)
                {
                    BattleStateManager.Battle.SwitchToState(BattleState.PokemonSelect);
                }

                GameStateManager.Instance.UIStateStack.Pop();
            }
            else if (ControlsManager.BPressed() && GameStateManager.Instance.InputDebounceTimer == 0)
            {
                if (BattleStateManager.Battle != null)
                {
                    BattleStateManager.Battle.SwitchToPreviousState();
                }

                GameStateManager.Instance.UIStateStack.Pop();
            }
            else if (ControlsManager.DownPressed() && GameStateManager.Instance.InputDebounceTimer == 0)
            {
                if (BagStateManager.ItemIndex < BagStateManager.AvailableItems.Count - 1)
                {
                    BagStateManager.ItemIndex++;
                }
            }
            else if (ControlsManager.UpPressed() && GameStateManager.Instance.InputDebounceTimer == 0)
            {
                if (BagStateManager.ItemIndex > 0)
                {
                    BagStateManager.ItemIndex--;
                }
            }
            else if (ControlsManager.LeftPressed() && GameStateManager.Instance.InputDebounceTimer == 0)
            {
                int index = (int)BagStateManager.BagState;
                if (index != 0)
                {
                    ItemType potentialBagState = BagStateManager.AllBagStates[index - 1];
                    if (BagStateManager.GetAllowedBagStates().Contains(potentialBagState))
                    {
                        BagStateManager.ItemIndex = 0;
                        BagStateManager.BagState = potentialBagState;
                    }
                }
            }
            else if (ControlsManager.RightPressed() && GameStateManager.Instance.InputDebounceTimer == 0)
            {
                int index = (int)BagStateManager.BagState;
                if (index != 2)
                {
                    ItemType potentialBagState = BagStateManager.AllBagStates[index + 1];
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