using Library.Battle;
using Library.Domain;
using Microsoft.Xna.Framework.Graphics;

namespace Library.GameState.Battle
{
    public class BattleStateManager : IStateManager
    {
        public static Battle Battle { get; set; }

        public void CreateNewBattle(CharacterState character1, CharacterState character2)
        {
            Battle = new Battle(character1, character2);
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
            BattleDrawingManager.DrawWallpaper(spriteBatch);
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