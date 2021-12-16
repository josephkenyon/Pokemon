using Library.Assets;
using Library.Battle;
using Library.Domain;
using Library.Graphics;
using Microsoft.Xna.Framework.Graphics;

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
            foreach(Pokemon pokemon in Battle.LeftCharacterState.Pokemon)
            {
                int index = Battle.LeftCharacterState.Pokemon.FindIndex(poke => poke == pokemon);
                pokemon.UpdateAfterBattle(battleCharacterState.Pokemon[index]);
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
    }
}