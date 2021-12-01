using LocationDesigner.Domain;
using LocationDesigner.FileHandling;
using LocationDesigner.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LocationDesigner.Graphics
{
    public class DrawingManager
    {
        public static void DrawLayout(Point offset, SpriteBatch spriteBatch) {

            if (TextureManager.BackgroundTileSetTexture != null)
            {
                spriteBatch.Begin();
                LocationLayoutManager.LocationLayout.BackgroundTiles.ForEach(tile =>
                {
                    spriteBatch.Draw(
                        texture: TextureManager.BackgroundTileSetTexture,
                        destinationRectangle: new Rectangle(
                            (offset.X + tile.Position.X) * Constants.ScaledTileSize,
                            (offset.Y + tile.Position.Y) * Constants.ScaledTileSize,
                            Constants.ScaledTileSize,
                            Constants.ScaledTileSize
                        ),
                        sourceRectangle: new Rectangle(
                            tile.SpritePosition.X * Constants.TileSize,
                            tile.SpritePosition.Y * Constants.TileSize,
                            Constants.TileSize,
                            Constants.TileSize
                        ),
                        color: Color.White,
                        rotation: 0f,
                        origin: Vector2.Zero,
                        effects: SpriteEffects.None,
                        layerDepth: 0f
                    );
                });
                spriteBatch.End();
            }

            if (TextureManager.ForegroundTileSetTexture != null)
            {
                spriteBatch.Begin();
                LocationLayoutManager.LocationLayout.ForegroundTiles.ForEach(tile =>
                {
                    spriteBatch.Draw(
                        texture: TextureManager.ForegroundTileSetTexture,
                        position: new Vector2((offset.X + tile.Position.X) * Constants.ScaledTileSize, (offset.Y + tile.Position.Y) * Constants.ScaledTileSize),
                        sourceRectangle: new Rectangle(
                            tile.SpritePosition.X * Constants.TileSize,
                            tile.SpritePosition.Y * Constants.TileSize,
                            Constants.TileSize,
                            Constants.TileSize
                        ),
                        color: Color.LightCoral,
                        rotation: 0f,
                        origin: Vector2.Zero,
                        scale: Constants.Scaler,
                        effects: SpriteEffects.None,
                        layerDepth: 0f
                    );
                });
                spriteBatch.End();
            }
        }
    }
}
