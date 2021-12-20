using Library.Assets;
using Library.Battle;
using Library.Domain;
using Library.Graphics;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace Library.GameState.Battle
{
    public class BattleStateManager : IStateManager
    {
        public static Battle Battle { get; set; }

        public static bool IsWildPokemonBattle => Battle != null && Battle.RightCharacterState.ParentAsset == null;

        public static void CreateNewBattle(CharacterState character1, CharacterState character2)
        {
            Battle = new Battle(character1, character2);
            GameStateManager.Instance.UIStateStack.Push(UIState.Battle);
        }

        public static void EndBattle()
        {
            BattleCharacterState battleCharacterState = Battle.BattleCharacterStates[Direction.Left];
            foreach (Pokemon pokemon in Battle.LeftCharacterState.Pokemon)
            {
                BattlePokemon battlePokemon = battleCharacterState.Pokemon.Where(poke => poke.OriginalPokemon == pokemon).FirstOrDefault();

                if (battlePokemon != null)
                {
                    pokemon.UpdateAfterBattle(battlePokemon);
                }
            }

            Battle = null;
            GameStateManager.Instance.UIStateStack.Pop();
        }

        public bool Update()
        {
            Battle.UpdateTimer();

            if (Battle.HasActiveTransation)
            {
                Battle.UpdateTransaction();
            }
            else
            {
                if (Battle.State == BattleState.EnemyAttack)
                {
                    EnemyBattleStateHelper.Update();
                }
                else
                {
                    BattleGamePadHandler.Update();
                }
            }

            return true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            GraphicsHelper.DrawWallpaper(spriteBatch, TextureName.BattleWallpaper);
            BattleDrawingManager.DrawAsh(spriteBatch);
            BattleDrawingManager.DrawMenu(spriteBatch);

            BattleDrawingManager.DrawPokemonBatch(spriteBatch, Battle.BattleCharacterStates[Direction.Left].Pokemon);
            BattleDrawingManager.DrawPokemonBatch(spriteBatch, Battle.BattleCharacterStates[Direction.Right].Pokemon);
        }

        public static Vector GetBattlePositions(Direction direction, int index)
        {
            int shortY = 3; int mediumY = 6; int largeY = 9;
            Vector vector = new Vector(9 - ((index - 1) / 2 * 3), index == 1 || index == 3 ? mediumY : index == 0 ? shortY : largeY);
            vector.X += direction == Direction.Right ? vector.Y == mediumY ? 9 : vector.Y == shortY ? 6 : 12 : 0;
            return vector;
        }

        public static void AdvanceStateAfterMoveUsage()
        {
            BattleCharacterState leftCharacterState = Battle.BattleCharacterStates[Direction.Left];
            bool allUsedMove = true;

            leftCharacterState.Pokemon.ForEach(pokemon => allUsedMove = (pokemon.UsedMove || pokemon.IsFainted) && allUsedMove);

            if (allUsedMove)
            {
                Battle.ClearStateStack();

                leftCharacterState.Pokemon.ForEach(poke => poke.UsedMove = false);

                Battle.SwitchToState(BattleState.EnemyAttack);
            }
            else
            {
                Battle.ClearStateStack();
                Battle.SwitchToState(BattleState.AshSelect);
            }
        }

        public static void EndBattleIfAppropriate()
        {
            if (Battle != null)
            {
                BattleCharacterState rightCharacterState = Battle.BattleCharacterStates[Direction.Right];

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
                    EndBattle();
                    return;
                }
            }
        }
    }
}