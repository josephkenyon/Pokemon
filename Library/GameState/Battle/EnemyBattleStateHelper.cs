using Library.Domain;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using System;

namespace Library.GameState.Battle
{
    public class EnemyBattleStateHelper
    {
        internal static void Update()
        {
            foreach (BattlePokemon battlePokemon in BattleStateManager.Battle.BattleCharacterStates[Direction.Right].Pokemon.Where(poke => !poke.IsFainted))
            {
                List<BattlePokemon> availableTargets = BattleStateManager.Battle.BattleCharacterStates[Direction.Left].Pokemon.Where(poke => !poke.IsFainted).ToList();

                BattlePokemon target = availableTargets[new Random().Next(1, availableTargets.Count) - 1];

                MoveResult moveResult = MoveManager.GetMoveResult(battlePokemon.Moves.OrderByDescending(move => move.Power).First(), battlePokemon, target, BattleStateManager.Battle);

                float moveDamage = battlePokemon.CurrentHealth > moveResult.Damage ? moveResult.Damage / 30f : battlePokemon.CurrentHealth / 30f;

                BattleStateManager.Battle.QueueNewTransaction(
                    () =>
                    {
                        target.Damage(moveDamage);
                    },
                    () =>
                    {
                        bool allFainted = true;
                        foreach (BattlePokemon faintedPokemon in BattleStateManager.Battle.BattleCharacterStates[Direction.Left].Pokemon)
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

                        battlePokemon.UsedMove = true;

                        bool allUsedMove = true;

                        BattleStateManager.Battle.BattleCharacterStates[Direction.Right].Pokemon.ForEach(pokemon => allUsedMove = (pokemon.UsedMove || pokemon.IsFainted) && allUsedMove);

                        BattleStateManager.Battle.ConsoleText = moveResult.MoveResultType.ToString();

                        Debug.WriteLine(BattleStateManager.Battle.ConsoleText);

                        if (allUsedMove)
                        {
                            BattleStateManager.Battle.ClearStateStack();

                            BattleStateManager.Battle.BattleCharacterStates[Direction.Right].Pokemon.ForEach(poke => poke.UsedMove = false);

                            BattleStateManager.Battle.SwitchToState(BattleState.AshSelect);
                        }
                    },
                    30
                );
            }
        }
    }
}