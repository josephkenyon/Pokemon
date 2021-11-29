using Library.Domain;
using Library.GameState.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Library.GameState.Battle.GamePadHelpers
{
    public static class EnemySelectHelper
    {
        public static void Update()
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

            int unselectedIndex = 0;
            if (BattleStateManager.Battle.BattleCharacterStates[Direction.Right].SelectedPokemon.IsFainted)
            {
                foreach (BattlePokemon pokemon in BattleStateManager.Battle.BattleCharacterStates[Direction.Right].Pokemon)
                {
                    if (!pokemon.IsFainted)
                    {
                        BattleStateManager.Battle.BattleCharacterStates[Direction.Right].SelectedPokemonIndex = unselectedIndex;
                        return;
                    }
                    unselectedIndex++;
                }
            }

            Direction? direction = GamePadHelper.GetDPadDirection(gamePadState);

            if (direction != null)
            {
                int? index = FightSelectHelper.GetIndexOf((Direction)direction, BattleStateManager.Battle.BattleCharacterStates[Direction.Right].Pokemon, BattleStateManager.Battle.BattleCharacterStates[Direction.Right].SelectedPokemonIndex, false);

                if (index != null)
                {
                    BattleStateManager.Battle.BattleCharacterStates[Direction.Right].SelectedPokemonIndex = (int)index;
                    GameStateManager.Instance.InputDebounceTimer = Constants.ItemDebounce;
                    BattleStateManager.Battle.ResetFadeTimer();
                }
            }

            if (gamePadState.Buttons.A == ButtonState.Pressed)
            {
                // move
                MoveResult moveResult = MoveManager.GetMoveResult(MoveSelectHelper.SelectedMove, BattleStateManager.Battle.BattleCharacterStates[Direction.Left].SelectedPokemon, BattleStateManager.Battle.BattleCharacterStates[Direction.Right].SelectedPokemon, BattleStateManager.Battle);
                
                BattlePokemon battlePokemon = BattleStateManager.Battle.BattleCharacterStates[Direction.Right].Pokemon[BattleStateManager.Battle.BattleCharacterStates[Direction.Right].SelectedPokemonIndex];

                float moveDamage = battlePokemon.CurrentHealth > moveResult.Damage ? moveResult.Damage / 30f : battlePokemon.CurrentHealth / 30f;

                BattleStateManager.Battle.QueueNewTransaction(
                    () => {
                        battlePokemon.Damage(moveDamage);
                    },
                    () => {
                        BattleStateManager.Battle.BattleCharacterStates[Direction.Left].Pokemon[BattleStateManager.Battle.BattleCharacterStates[Direction.Left].SelectedPokemonIndex].UsedMove = true;

                        bool allUsedMove = true;

                        BattleStateManager.Battle.BattleCharacterStates[Direction.Left].Pokemon.ForEach(pokemon => allUsedMove = (pokemon.UsedMove || pokemon.IsFainted) && allUsedMove);

                        BattleStateManager.Battle.ConsoleText = moveResult.MoveResultType.ToString();

                        Debug.WriteLine(BattleStateManager.Battle.ConsoleText);

                        if (allUsedMove)
                        {
                            BattleStateManager.Battle.ClearStateStack();

                            BattleStateManager.Battle.BattleCharacterStates[Direction.Left].Pokemon.ForEach(poke => poke.UsedMove = false);

                            BattleStateManager.Battle.SwitchToState(BattleState.EnemyAttack);
                        }
                        else
                        {
                            BattleStateManager.Battle.ClearStateStack();
                            BattleStateManager.Battle.SwitchToState(BattleState.AshSelect);
                        }
                    },
                    30
                );
                GameStateManager.Instance.InputDebounceTimer = Constants.MenuActivationDebounce;
            }
        }
    }



}