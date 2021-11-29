using Library.Domain;
using System.Linq;
using System.Diagnostics;

namespace Library.GameState.Battle
{
    public class EnemyBattleStateHelper
    {
        internal static void Update()
        {
            foreach (BattlePokemon battlePokemon in BattleStateManager.Battle.BattleCharacterStates[Direction.Right].Pokemon.Where(poke => !poke.IsFainted))
            {
                BattlePokemon target = BattleStateManager.Battle.BattleCharacterStates[Direction.Left].Pokemon.Where(poke => !poke.IsFainted).OrderBy(poke => poke.CurrentHealth).First();
                MoveResult moveResult = MoveManager.GetMoveResult(battlePokemon.Moves.OrderByDescending(move => move.Power).First(), battlePokemon, target, BattleStateManager.Battle);

                float moveDamage = battlePokemon.CurrentHealth > moveResult.Damage ? moveResult.Damage / 30f : battlePokemon.CurrentHealth / 30f;

                BattleStateManager.Battle.QueueNewTransaction(
                    () =>
                    {
                        target.Damage(moveDamage);
                    },
                    () =>
                    {
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