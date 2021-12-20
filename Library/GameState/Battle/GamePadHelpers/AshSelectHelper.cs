using Library.Controls;
using Library.Domain;
using Library.GameState.Input;
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
            Direction? direction = GamePadHelper.GetPressedDPadButton();

            if (direction != null)
            {
                if ((Direction)direction == Direction.Left)
                {
                    SelectedIndex--;
                    if (SelectedIndex < 0)
                    {
                        SelectedIndex = ItemList.Length - 1;
                    }
                }
                else if ((Direction)direction == Direction.Right)
                {
                    SelectedIndex++;
                    if (SelectedIndex == ItemList.Length)
                    {
                        SelectedIndex = 0;
                    }

                }
            }

            if (ControlsManager.ControlPressed(Control.A))
            {
                switch (ItemList[SelectedIndex])
                {
                    case BattleMenuItem.Fight:
                        BattleStateManager.Battle.SwitchToState(BattleState.PokemonSelect);
                        break;
                    case BattleMenuItem.Bag:
                        BattleStateManager.Battle.SwitchToState(BattleState.ItemSelect);
                        GameStateManager.Instance.UIStateStack.Push(UIState.Bag);
                        break;
                    case BattleMenuItem.Run:
                        BattleStateManager.EndBattle();
                        break;
                };
            }
        }
    }
}