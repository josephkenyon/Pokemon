using Library.Controls;
using Library.Domain;
using Library.GameState.Battle;
using System.Collections.Generic;

namespace Library.GameState.BagState
{
    public static class BagStateGamePadHandler
    {
        public static void Update()
        {
            if (ControlsManager.ControlPressed(Control.A))
            {
                if (BattleStateManager.Battle != null)
                {
                    if (BagStateManager.BagState == ItemType.Generic_Item)
                    {
                        BattleStateManager.Battle.SwitchToState(BattleState.PokemonSelect);
                    }
                    else if (BagStateManager.BagState == ItemType.Poke_Ball)
                    {
                        BattleStateManager.Battle.SwitchToState(BattleState.EnemySelect);
                    }
                }

                GameStateManager.Instance.UIStateStack.Pop();
            }
            else if (ControlsManager.ControlPressed(Control.B))
            {
                if (BattleStateManager.Battle != null)
                {
                    BattleStateManager.Battle.SwitchToPreviousState();
                }

                BagStateManager.ItemIndex = -1;
                GameStateManager.Instance.UIStateStack.Pop();
            }
            else if (ControlsManager.ControlPressed(Control.Down))
            {
                if (BagStateManager.ItemIndex < BagStateManager.AvailableItems.Count - 1)
                {
                    BagStateManager.ItemIndex++;
                }
            }
            else if (ControlsManager.ControlPressed(Control.Up))
            {
                if (BagStateManager.ItemIndex > 0)
                {
                    BagStateManager.ItemIndex--;
                }
            }
            else if (ControlsManager.ControlPressed(Control.Left))
            {
                List<ItemType> allowedBagStates = BagStateManager.GetAllowedBagStates();
                int index = allowedBagStates.IndexOf(BagStateManager.BagState);
                if (index > 0)
                {
                    BagStateManager.ItemIndex = 0;
                    BagStateManager.BagState = allowedBagStates[index - 1];
                }
            }
            else if (ControlsManager.ControlPressed(Control.Right))
            {
                List<ItemType> allowedBagStates = BagStateManager.GetAllowedBagStates();
                int index = allowedBagStates.IndexOf(BagStateManager.BagState);
                if (index + 1 < allowedBagStates.Count)
                {
                    BagStateManager.ItemIndex = 0;
                    BagStateManager.BagState = allowedBagStates[index + 1];
                }
            }
            else
            {
                return;
            }
        }
    }
}