using Library.Controls;
using Library.Domain;
using Library.GameState.Input;
using System.Diagnostics;

namespace Library.GameState.Battle.GamePadHelpers
{
    public static class EnemySelectHelper
    {
        public static void Update()
        {
            BattleCharacterState leftCharacterState = BattleStateManager.Battle.BattleCharacterStates[Direction.Left];
            BattleCharacterState rightCharacterState = BattleStateManager.Battle.BattleCharacterStates[Direction.Right];

            int unselectedIndex = 0;

            if (rightCharacterState.SelectedPokemon.IsFainted)
            {
                foreach (BattlePokemon pokemon in rightCharacterState.Pokemon)
                {
                    if (!pokemon.IsFainted)
                    {
                        rightCharacterState.SelectedPokemonIndex = unselectedIndex;
                        return;
                    }
                    unselectedIndex++;
                }
            }

            Direction? direction = GamePadHelper.GetDPadDirection();

            if (direction != null)
            {
                int? index = PokemonSelectHelper.GetIndexOf((Direction)direction, rightCharacterState.Pokemon, rightCharacterState.SelectedPokemonIndex, false);

                if (index != null)
                {
                    BattleStateManager.Battle.BattleCharacterStates[Direction.Right].SelectedPokemonIndex = (int)index;
                    GameStateManager.Instance.InputDebounceTimer = Constants.ItemDebounce;
                    BattleStateManager.Battle.ResetFadeTimer();
                }
            }

            if (ControlsManager.APressed())
            {
                BattlePokemon PokemonThatsAttacking = BattleStateManager.Battle.BattleCharacterStates[Direction.Left].SelectedPokemon;
                BattlePokemon attackedPokemon = rightCharacterState.Pokemon[rightCharacterState.SelectedPokemonIndex];

                MoveResult moveResult = MoveManager.GetMoveResult(MoveSelectHelper.SelectedMove, PokemonThatsAttacking, attackedPokemon, BattleStateManager.Battle);

                float moveDamage = moveResult.Damage / 30f;
                BattleStateManager.Battle.QueueNewTransaction(
                    () =>
                    {
                        attackedPokemon.Damage(moveDamage);
                    },
                    () =>
                    {
                        if (attackedPokemon.IsFainted)
                        {
                            int experienceReward = attackedPokemon.GetExperienceReward();

                            foreach (BattlePokemon rewardedPokemon in leftCharacterState.Pokemon)
                            {
                                if (!rewardedPokemon.IsFainted)
                                {
                                    rewardedPokemon.GainExperience(experienceReward);
                                }
                            }

                            bool allFainted = true;
                            foreach (BattlePokemon faintedPokemon in rightCharacterState.Pokemon)
                            {
                                if (!faintedPokemon.IsFainted)
                                {
                                    allFainted = false;
                                    break;
                                }
                            }

                            if (allFainted)
                            {
                                BattleStateManager.EndBattle();
                                return;
                            }
                        }

                        leftCharacterState.Pokemon[leftCharacterState.SelectedPokemonIndex].UsedMove = true;

                        bool allUsedMove = true;

                        leftCharacterState.Pokemon.ForEach(pokemon => allUsedMove = (pokemon.UsedMove || pokemon.IsFainted) && allUsedMove);

                        BattleStateManager.Battle.ConsoleText = moveResult.MoveResultType.ToString();

                        Debug.WriteLine(BattleStateManager.Battle.ConsoleText);

                        if (allUsedMove)
                        {
                            BattleStateManager.Battle.ClearStateStack();

                            leftCharacterState.Pokemon.ForEach(poke => poke.UsedMove = false);

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