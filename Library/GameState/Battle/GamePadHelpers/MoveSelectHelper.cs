using Library.Controls;
using Library.Domain;
using Library.GameState.Input;
using System.Collections.Generic;

namespace Library.GameState.Battle.GamePadHelpers
{
    public static class MoveSelectHelper
    {
        public static int SelectedIndex { get; set; }
        public static List<Move> MoveList => BattleStateManager.Battle.BattleCharacterStates[Direction.Left].Pokemon[BattleStateManager.Battle.BattleCharacterStates[Direction.Left].SelectedPokemonIndex].Moves;
        public static Move SelectedMove => MoveList[SelectedIndex];

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
                        SelectedIndex = MoveList.Count - 1;
                    }
                }
                else if ((Direction)direction == Direction.Right)
                {
                    SelectedIndex++;
                    if (SelectedIndex >= MoveList.Count)
                    {
                        SelectedIndex = 0;
                    }
                }
                else if ((Direction)direction == Direction.Up)
                {
                    if (SelectedIndex > 1)
                    {
                        SelectedIndex -= 2;
                    }
                }
                else if ((Direction)direction == Direction.Down)
                {
                    if (SelectedIndex < 2 && MoveList.Count > 1)
                    {
                        SelectedIndex += 2;
                    }
                }
            }

            if (ControlsManager.ControlPressed(Control.A))
            {
                BattleStateManager.Battle.SwitchToState(BattleState.EnemySelect);
            }
        }
    }
}