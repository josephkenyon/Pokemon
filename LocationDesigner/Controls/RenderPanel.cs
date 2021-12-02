using LocationDesigner.Domain;
using LocationDesigner.FileHandling;
using LocationDesigner.Graphics;
using LocationDesigner.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Controls;
using System.Linq;
using System.Windows.Forms;

namespace LevelDesigner.Controls
{
    public class RenderPanel : MonoGameControl
    {
        public static Point TopLeftPoint { get; set; }

        protected override void Initialize()
        {
            base.Initialize();

            TopLeftPoint = new Point(Constants.RenderWidth / 2, Constants.RenderHeight / 2);

            GraphicsManager.Initialize(Editor.graphics);
            LocationLayoutManager.Initialize();

            TextureManager.BackgroundTileSetTexture = TextureManager.GetTexture2DFromBitmap(BitmapManager.BackgroundTileSetBitmap);
            TextureManager.ForegroundTileSetTexture = TextureManager.GetTexture2DFromBitmap(BitmapManager.ForegroundTileSetBitmap);
            TextureManager.GrassTileSetTexture = TextureManager.GetTexture2DFromBitmap(BitmapManager.GrassTileSetBitmap);

            MouseUp += RenderPanel_MouseUp;
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Draw()
        {
            base.Draw();

            GraphicsDevice.Clear(Color.CornflowerBlue);

            DrawingManager.DrawLayout(TopLeftPoint, Editor.spriteBatch);
        }

        private void RenderPanel_MouseUp(object sender, MouseEventArgs e)
        {
            int xPosition = e.X / Constants.ScaledTileSize - TopLeftPoint.X;
            int yPosition = e.Y / Constants.ScaledTileSize - TopLeftPoint.Y;

            TileJson tile = null;
            if (BackgroundTileBox.LastClicked != null)
            {
                tile = LocationLayoutManager.LocationLayout.BackgroundTiles.FirstOrDefault(t => t.Position.X == xPosition && t.Position.Y == yPosition);
                if (tile != null)
                {
                    tile.SpritePosition = new Point(BackgroundTileBox.LastClicked.SpriteLocation.X, BackgroundTileBox.LastClicked.SpriteLocation.Y);
                }
                else
                {
                    LocationLayoutManager.LocationLayout.BackgroundTiles.Add(new TileJson()
                    {
                        Position = new Point(xPosition, yPosition),
                        SpritePosition = new Point(BackgroundTileBox.LastClicked.SpriteLocation.X, BackgroundTileBox.LastClicked.SpriteLocation.Y)
                    });
                }
            }
            else if (ForegroundTileBox.LastClicked != null)
            {
                tile = LocationLayoutManager.LocationLayout.ForegroundTiles.FirstOrDefault(t => t.Position.X == xPosition && t.Position.Y == yPosition);
                if (tile != null)
                {
                    tile.SpritePosition = new Point(ForegroundTileBox.LastClicked.SpriteLocation.X, ForegroundTileBox.LastClicked.SpriteLocation.Y);
                }
                else
                {
                    LocationLayoutManager.LocationLayout.ForegroundTiles.Add(new TileJson()
                    {
                        Position = new Point(xPosition, yPosition),
                        SpritePosition = new Point(ForegroundTileBox.LastClicked.SpriteLocation.X, ForegroundTileBox.LastClicked.SpriteLocation.Y)
                    });
                }
            }
            else if (DoodadTileBox.LastClicked != null)
            {
                tile = LocationLayoutManager.LocationLayout.LocationDoodads.FirstOrDefault(t => t.Position.X == xPosition && t.Position.Y == yPosition);
                if (tile != null)
                {
                    tile.SpritePosition = new Point(ForegroundTileBox.LastClicked.SpriteLocation.X, ForegroundTileBox.LastClicked.SpriteLocation.Y);
                }
                else
                {
                    LocationLayoutManager.LocationLayout.LocationDoodads.Add(new TileJson()
                    {
                        Position = new Point(xPosition, yPosition),
                        LocationDoodad = (LocationDoodad) DoodadTileBox.LastClicked.SpriteLocation.Y
                    });
                }
            }
            else {
                tile = LocationLayoutManager.LocationLayout.ForegroundTiles.FirstOrDefault(t => t.Position.X == xPosition && t.Position.Y == yPosition);
                if (tile != null) {
                    LocationLayoutManager.LocationLayout.ForegroundTiles.Remove(tile);
                }

                tile = LocationLayoutManager.LocationLayout.LocationDoodads.FirstOrDefault(t => t.Position.X == xPosition && t.Position.Y == yPosition);
                if (tile != null) {
                    LocationLayoutManager.LocationLayout.LocationDoodads.Remove(tile);
                }
            }
        }
    }
}