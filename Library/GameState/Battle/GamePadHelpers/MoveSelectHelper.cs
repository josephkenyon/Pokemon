using Library.Domain;
using Library.GameState.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace Library.GameState.Battle.GamePadHelpers
{
    public static class MoveSelectHelper
    {
        public static int SelectedIndex { get; set; }
        public static List<Move> MoveList => BattleStateManager.Battle.BattleCharacterStates[Direction.Left].Pokemon[BattleStateManager.Battle.BattleCharacterStates[Direction.Left].SelectedPokemonIndex].Moves;
        public static Move SelectedMove => MoveList[SelectedIndex];

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
                        SelectedIndex = MoveList.Count - 1;
                    }
                    GameStateManager.Instance.InputDebounceTimer = Constants.ItemDebounce;
                }
                else if ((Direction)direction == Direction.Right)
                {
                    SelectedIndex++;
                    if (SelectedIndex >= MoveList.Count)
                    {
                        SelectedIndex = 0;
                    }
                    GameStateManager.Instance.InputDebounceTimer = Constants.ItemDebounce;
                }
                else if ((Direction)direction == Direction.Up)
                {
                    if (SelectedIndex > 1)
                    {
                        SelectedIndex -= 2;
                    }
                    GameStateManager.Instance.InputDebounceTimer = Constants.ItemDebounce;
                }
                else if ((Direction)direction == Direction.Down)
                {
                    if (SelectedIndex < 2 && MoveList.Count > 1)
                    {
                        SelectedIndex += 2;
                    }
                    GameStateManager.Instance.InputDebounceTimer = Constants.ItemDebounce;
                }
            }

            if (gamePadState.Buttons.A == ButtonState.Pressed)
            {
                BattleStateManager.Battle.SwitchToState(BattleState.EnemySelect);
                GameStateManager.Instance.InputDebounceTimer = Constants.MenuActivationDebounce;
            }
        }
    }
}