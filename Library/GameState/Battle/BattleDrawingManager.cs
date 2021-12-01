using Library.Domain;
using Library.GameState.Battle;
using Library.GameState.Battle.BattleDrawingObjects;
using Library.GameState.Battle.GamePadHelpers;
using Library.GameState.Menu;
using Library.Graphics;
using Library.Menu;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Library.Battle
{
    public static class BattleDrawingManager
    {
        public static void DrawPokemonBatch(SpriteBatch spriteBatch, IEnumerable<BattlePokemon> Pokemon)
        {
            int index = 0;
            foreach (BattlePokemon pokemon in Pokemon)
            {
                PokemonDrawingObject pokemonDrawingObject = BattleGraphicsHelper.GetPokemonDrawingObject(pokemon, index);
                PokemonShadowDrawingObject pokemonShadowDrawingObject = BattleGraphicsHelper.GetShadowDrawingObject(pokemonDrawingObject);
                HealthBarShell healthBarShell = BattleGraphicsHelper.GetHealthBarShell(pokemonDrawingObject);

                DrawingManager.DrawSingle(spriteBatch, BattleGraphicsHelper.GetShadowDrawingObject(pokemonDrawingObject));
                DrawingManager.DrawSingle(spriteBatch, pokemonDrawingObject);
                DrawingManager.DrawSingle(spriteBatch, healthBarShell);
                DrawingManager.DrawSingle(spriteBatch, BattleGraphicsHelper.GetHealthBar(healthBarShell));
                DrawingManager.DrawSingle(spriteBatch, BattleGraphicsHelper.GetExpBar(healthBarShell));

                index++;
            }
        }

        public static void DrawWallpaper(SpriteBatch spriteBatch)
        {
            DrawingManager.DrawSingle(spriteBatch, new Wallpaper());
        }

        public static void DrawAsh(SpriteBatch spriteBatch)
        {
            DrawingManager.DrawSingle(spriteBatch, new BattleAsh());
        }

        public static void DrawBattleMessage(SpriteBatch spriteBatch, string message)
        {
            DrawingManager.DrawSingle(spriteBatch, new BattleMessageMenu());
            DrawingManager.DrawString(spriteBatch, new BattleMessageStringObject { Position = new Vector(11, 6) * Constants.Scaler + BattleGraphicsHelper.BattleMenuLocation, String = message });
        }

        public static void DrawMoveSelectMenu(SpriteBatch spriteBatch)
        {
            List<Vector> moveStringPositions = new List<Vector> { new Vector(11, 6), new Vector(75, 6), new Vector(11, 17), new Vector(75, 17) };
            DrawingManager.DrawSingle(spriteBatch, new BattleMessageMenu());
            int index = 0;
            foreach (Move move in MoveSelectHelper.MoveList) { 
                DrawingManager.DrawString(spriteBatch, new BattleMessageStringObject { Position = moveStringPositions[index++] * Constants.Scaler + BattleGraphicsHelper.BattleMenuLocation, String = move.FormattedName});
            }

            Rectangle targetRectangle = BattleGraphicsHelper.PointerTargetRectangle;
            targetRectangle.Location = (moveStringPositions[MoveSelectHelper.SelectedIndex] * Constants.Scaler).ToPoint() - new Vector(targetRectangle.Size.X / 1.5f , 1f * Constants.Scaler).ToPoint() + BattleGraphicsHelper.BattleMenuLocation.ToPoint();
            MenuDrawingManager.DrawMenuItem(spriteBatch, MenuGraphicsHelper.PointerSourceRectangle, targetRectangle);
        }

        public static void DrawMenu(SpriteBatch spriteBatch)
        {
            switch (BattleStateManager.Battle.State) {
                case BattleState.AshSelect:
                    DrawingManager.DrawSingle(spriteBatch, new AshMenu());
                    MenuDrawingManager.DrawMenuItem(spriteBatch, MenuGraphicsHelper.PointerSourceRectangle, BattleGraphicsHelper.PointerTargetRectangle);
                    break;
                case BattleState.MoveSelect:
                    DrawMoveSelectMenu(spriteBatch);
                    break;
            }
        }
    }
}