using System.Collections.Generic;
using Library.Assets;
using Library.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace Library.World
{
    public class LocationLayout
    {
        public Dictionary<Point, Tile> BackgroundTiles { get; set; }
        public Dictionary<Point, Tile> ForegroundTiles { get; set; }
        public Dictionary<Point, Tile> BackgroundGrassTiles { get; set; }
        public Dictionary<Point, Tile> ForegroundGrassTiles { get; set; }
        public Dictionary<Point, SignJson> Signs { get; set; }
        public Dictionary<Point, PortalJson> Portals { get; set; }
        public List<Character> InitialCharacters { get; set; }

        public void DrawBackground(SpriteBatch spriteBatch, StitchHelperObject stitchHelperObject = null)
        {
            IBaseDrawingManager drawingManager = GetDrawingManager(stitchHelperObject);

            drawingManager.DrawBatch(spriteBatch, BackgroundTiles.Values);
            drawingManager.DrawBatch(spriteBatch, ForegroundTiles.Values.Where(tile => tile.SpritePosition.Y > 0));
            drawingManager.DrawBatch(spriteBatch, BackgroundGrassTiles.Values);
            drawingManager.DrawBatch(spriteBatch, ForegroundGrassTiles.Values.Where(tile => tile.GetPosition().Y < GameState.GameStateManager.Instance.GetPlayer().CharacterState.Position.Y));
            drawingManager.DrawBatch(spriteBatch, Portals.Values.Where(portal => portal.HasRug).Select(portal => portal.GetDrawingObject()));
        }

        public void DrawForeground(SpriteBatch spriteBatch, StitchHelperObject stitchHelperObject = null)
        {
            IBaseDrawingManager drawingManager = GetDrawingManager(stitchHelperObject);

            drawingManager.DrawBatch(spriteBatch, ForegroundTiles.Values.Where(tile => tile.SpritePosition.Y == 0));
            drawingManager.DrawBatch(spriteBatch, ForegroundGrassTiles.Values.Where(tile => tile.GetPosition().Y >= GameState.GameStateManager.Instance.GetPlayer().CharacterState.Position.Y));
        }

        public static IBaseDrawingManager GetDrawingManager(StitchHelperObject stitchHelperObject)
        {
            if (stitchHelperObject == null)
            {
                return BaseDrawingManager.Instance;
            }
            else
            {
                return StitchDrawingManager.Instance;
            }
        }
    }
}
