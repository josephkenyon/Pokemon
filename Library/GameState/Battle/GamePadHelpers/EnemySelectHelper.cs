using Library.Assets;
using Library.Battle;
using Library.Content;
using Library.Controls;
using Library.Domain;
using Library.GameState.BagState;
using Library.GameState.Input;
using System.Diagnostics;
using System.Linq;

namespace Library.GameState.Battle.GamePadHelpers
{
    public static class EnemySelectHelper
    {
        public static void Update()
        {
            BattleCharacterState leftCharacterState = BattleStateManager.Battle.BattleCharacterStates[Direction.Left];
            BattleCharacterState rightCharacterState = BattleStateManager.Battle.BattleCharacterStates[Direction.Right];

            int unselectedIndex = 0;

            if (rightCharacterState.GetSelectedPokemon().IsFainted)
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

            Direction? direction = GamePadHelper.GetPressedDPadButton();

            if (direction != null)
            {
                int? index = PokemonSelectHelper.GetIndexOf((Direction)direction, rightCharacterState.Pokemon, rightCharacterState.SelectedPokemonIndex, false);

                if (index != null)
                {
                    BattleStateManager.Battle.BattleCharacterStates[Direction.Right].SelectedPokemonIndex = (int)index;
                    BattleStateManager.Battle.ResetFadeTimer();
                }
            }

            if (ControlsManager.ControlPressed(Control.A))
            {
                BattlePokemon attackedPokemon = rightCharacterState.Pokemon[rightCharacterState.SelectedPokemonIndex];

                ItemName? selectedItemName = BagStateManager.GetSelectedItem();
                if (selectedItemName != null && BagStateManager.BagState == ItemType.Poke_Ball)
                {
                    leftCharacterState.Pokemon.Where(pokemon => !pokemon.UsedMove).First().UsedMove = true;
                    BagStateManager.UseSelectedItem();

                    int itemLevel = ItemManager.GetItemLevel((ItemName)selectedItemName);
                    bool isCaught = CatchManager.IsCaught(itemLevel, attackedPokemon);

                    if (isCaught)
                    {
                        BattleStateManager.Battle.LeftCharacterState.Pokemon.Add(new Pokemon(attackedPokemon));
                        rightCharacterState.Pokemon.Remove(attackedPokemon);
                    }

                    BattleStateManager.AdvanceStateAfterMoveUsage();
                }
                else
                {
                    BattlePokemon PokemonThatsAttacking = BattleStateManager.Battle.BattleCharacterStates[Direction.Left].GetSelectedPokemon();

                    if (MoveSelectHelper.SelectedMove.Power != null)
                    {
                        MoveResult moveResult = MoveManager.GetMoveResult(MoveSelectHelper.SelectedMove, PokemonThatsAttacking, attackedPokemon, BattleStateManager.Battle);

                        float moveDamage = (float)(moveResult.Damage / 30f);
                        BattleStateManager.Battle.QueueNewTransaction(
                            () =>
                            {
                                attackedPokemon.Damage(moveDamage);
                            },
                            () =>
                            {
                                OnMoveComplete(attackedPokemon, moveResult);
                            },
                            30
                        );
                    }
                    else if (MoveSelectHelper.SelectedMove.StatInteraction != null)
                    {
                        attackedPokemon.ApplyStatDebuff(MoveSelectHelper.SelectedMove);
                        OnMoveComplete(attackedPokemon, new MoveResult(MoveSelectHelper.SelectedMove));
                    }

                }
            }
        }

        private static void OnMoveComplete(BattlePokemon attackedPokemon, MoveResult moveResult)
        {
            BattleCharacterState leftCharacterState = BattleStateManager.Battle.BattleCharacterStates[Direction.Left];

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
            }

            BattleStateManager.EndBattleIfAppropriate();


            if (BattleStateManager.Battle != null)
            {
                leftCharacterState.Pokemon[leftCharacterState.SelectedPokemonIndex].UsedMove = true;
                BattleStateManager.AdvanceStateAfterMoveUsage();
            }
        }
    }
}