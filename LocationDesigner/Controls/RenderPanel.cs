using LocationDesigner.Controls;
using LocationDesigner.Domain;
using LocationDesigner.FileHandling;
using LocationDesigner.Graphics;
using LocationDesigner.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Controls;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LevelDesigner.Controls
{
    public class RenderPanel : MonoGameControl
    {
        public static Point TopLeftPoint { get; set; }
        public static Point DefaultSpot => new Point(Constants.RenderWidth / 2, Constants.RenderHeight / 2);
        private static bool MouseIsDown { get; set; }

        protected override void Initialize()
        {
            base.Initialize();

            TopLeftPoint = DefaultSpot;

            GraphicsManager.Initialize(Editor.graphics);
            LocationLayoutManager.Initialize();

            TextureManager.BackgroundTileSetTexture = TextureManager.GetTexture2DFromBitmap(BitmapManager.BackgroundTileSetBitmap);
            TextureManager.ForegroundTileSetTexture = TextureManager.GetTexture2DFromBitmap(BitmapManager.ForegroundTileSetBitmap);
            TextureManager.GrassTileSetTexture = TextureManager.GetTexture2DFromBitmap(BitmapManager.GrassTileSetBitmap);

            MouseDown += RenderPanel_MouseDown;
            MouseUp += RenderPanel_MouseUp;
            MouseMove += RenderPanel_MouseMove;
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

        private void RenderPanel_MouseDown(object sender, MouseEventArgs e)
        {
            MouseIsDown = true;
        }

        private void RenderPanel_MouseMove(object sender, MouseEventArgs e)
        {
            int xPosition = e.X / Constants.ScaledTileSize - TopLeftPoint.X;
            int yPosition = e.Y / Constants.ScaledTileSize - TopLeftPoint.Y;

            FormHelper.CoordinatesLabel.Text = xPosition + ", " + yPosition;

            List<BackgroundTileBox> backgroundTileBoxes = FormHelper.BackgroundTiles.Where(t => t.IsSelected()).ToList();

            if (backgroundTileBoxes.Count > 0 && MouseIsDown)
            {
                BackgroundTileBox tileBox = backgroundTileBoxes.First();
                TileJson tile = LocationLayoutManager.LocationLayout.BackgroundTiles.FirstOrDefault(t => t.Position.X == xPosition && t.Position.Y == yPosition);
                if (tile != null)
                {
                    tile.SpritePosition = new Point(tileBox.SpriteLocation.X, tileBox.SpriteLocation.Y);
                }
                else
                {
                    LocationLayoutManager.LocationLayout.BackgroundTiles.Add(new TileJson()
                    {
                        Position = new Point(xPosition, yPosition),
                        SpritePosition = new Point(tileBox.SpriteLocation.X, tileBox.SpriteLocation.Y)
                    });
                }
            }
        }

        private void RenderPanel_MouseUp(object sender, MouseEventArgs e)
        {
            MouseIsDown = false;

            int xPosition = e.X / Constants.ScaledTileSize - TopLeftPoint.X;
            int yPosition = e.Y / Constants.ScaledTileSize - TopLeftPoint.Y;

            List<BackgroundTileBox> backgroundTileBoxes = FormHelper.BackgroundTiles.Where(t => t.IsSelected()).ToList();
            List<ForegroundTileBox> foregroundTileBoxes = FormHelper.ForegroundTiles.Where(t => t.IsSelected()).ToList();
            List<DoodadTileBox> doodadTileBoxes = FormHelper.DoodadTiles.Where(t => t.IsSelected()).ToList();

            SignJson sign = null;
            if (FormHelper.SignCheckBox.Checked)
            {
                string message = Microsoft.VisualBasic.Interaction.InputBox("Input sign strings (\\ for next message):", "Sign String Input", "Hello Ash!");
                if (message != null)
                {
                    List<string> messages = message.Split('\\').ToList();
                    sign = LocationLayoutManager.LocationLayout.Signs.FirstOrDefault(t => t.Position.X == xPosition && t.Position.Y == yPosition);
                    if (sign != null)
                    {
                        sign.Messages = messages;
                    }
                    else
                    {
                        LocationLayoutManager.LocationLayout.Signs.Add(new SignJson()
                        {
                            Position = new Point(xPosition, yPosition),
                            Messages = messages
                        });
                    }
                }

            }
            else if (FormHelper.PortalCheckbox.Checked)
            {
                string message = Microsoft.VisualBasic.Interaction.InputBox("Input file name of location:", "Location String Input", "<locationName>");
                int xCoord = int.Parse(Microsoft.VisualBasic.Interaction.InputBox("Xcoordinate:", "Xcoordinate integer Input", "0"));
                int yCoord = int.Parse(Microsoft.VisualBasic.Interaction.InputBox("Ycoordinate:", "Ycoordinate integer Input", "0"));
                if (message != null)
                {
                    PortalJson portalJson = LocationLayoutManager.LocationLayout.Portals.FirstOrDefault(t => t.Position.X == xPosition && t.Position.Y == yPosition);
                    if (portalJson != null)
                    {
                        portalJson.ToLocationName = message;
                        portalJson.Position = new Point(xPosition, yPosition);
                        portalJson.Coordinate = new Point(xCoord, yCoord);
                    }
                    else
                    {
                        LocationLayoutManager.LocationLayout.Portals.Add(new PortalJson()
                        {
                            ToLocationName = message,
                            Position = new Point(xPosition, yPosition),
                            Coordinate = new Point(xCoord, yCoord)
                        });
                    }
                }
            }
            else if (backgroundTileBoxes.Count > 0)
            {
                BackgroundTileBox tileBox = backgroundTileBoxes.First();
                TileJson tile = LocationLayoutManager.LocationLayout.BackgroundTiles.FirstOrDefault(t => t.Position.X == xPosition && t.Position.Y == yPosition);
                if (tile != null)
                {
                    tile.SpritePosition = new Point(tileBox.SpriteLocation.X, tileBox.SpriteLocation.Y);
                }
                else
                {
                    LocationLayoutManager.LocationLayout.BackgroundTiles.Add(new TileJson()
                    {
                        Position = new Point(xPosition, yPosition),
                        SpritePosition = new Point(tileBox.SpriteLocation.X, tileBox.SpriteLocation.Y)
                    });
                }
            }
            else if (foregroundTileBoxes.Count > 0)
            {
                TileBox pivotBox = foregroundTileBoxes.OrderBy(t => t.SpriteLocation.X).ThenBy(t => t.SpriteLocation.Y).First();

                foregroundTileBoxes.ForEach(tileBox =>
                {
                    int positionX = xPosition;
                    int positionY = yPosition;
                    positionX += tileBox.SpriteLocation.X - pivotBox.SpriteLocation.X;
                    positionY += tileBox.SpriteLocation.Y - pivotBox.SpriteLocation.Y;

                    TileJson tile = LocationLayoutManager.LocationLayout.ForegroundTiles.FirstOrDefault(t => t.Position.X == positionX && t.Position.Y == positionY);
                    if (FormHelper.SuperForegroundCheckbox.Checked)
                    {
                        tile = LocationLayoutManager.LocationLayout.SuperForegroundTiles.FirstOrDefault(t => t.Position.X == positionX && t.Position.Y == positionY);
                    }

                    if (tile != null)
                    {
                        tile.SpritePosition = new Point(tileBox.SpriteLocation.X, tileBox.SpriteLocation.Y);
                    }
                    else
                    {
                        TileJson tileJson = new TileJson()
                        {
                            Position = new Point(positionX, positionY),
                            SpritePosition = new Point(tileBox.SpriteLocation.X, tileBox.SpriteLocation.Y)
                        };
                        if (FormHelper.SuperForegroundCheckbox.Checked)
                        {
                            LocationLayoutManager.LocationLayout.SuperForegroundTiles.Add(tileJson);
                        }
                        else
                        {
                            LocationLayoutManager.LocationLayout.ForegroundTiles.Add(tileJson);
                        }
                    }
                });
            }
            else if (doodadTileBoxes.Count > 0)
            {
                DoodadTileBox tileBox = doodadTileBoxes.First();
                TileJson tile = LocationLayoutManager.LocationLayout.LocationDoodads.FirstOrDefault(t => t.Position.X == xPosition && t.Position.Y == yPosition);
                if (tile != null)
                {
                    tile.SpritePosition = new Point(tileBox.SpriteLocation.X, tileBox.SpriteLocation.Y);
                }
                else
                {
                    LocationLayoutManager.LocationLayout.LocationDoodads.Add(new TileJson()
                    {
                        Position = new Point(xPosition, yPosition),
                        LocationDoodad = (LocationDoodad)tileBox.SpriteLocation.Y
                    });
                }
            }
            else
            {
                TileJson tile = LocationLayoutManager.LocationLayout.BackgroundTiles.FirstOrDefault(t => t.Position.X == xPosition && t.Position.Y == yPosition);
                if (tile != null)
                {
                    LocationLayoutManager.LocationLayout.BackgroundTiles.Remove(tile);
                }

                tile = LocationLayoutManager.LocationLayout.ForegroundTiles.FirstOrDefault(t => t.Position.X == xPosition && t.Position.Y == yPosition);
                if (tile != null)
                {
                    LocationLayoutManager.LocationLayout.ForegroundTiles.Remove(tile);
                }

                tile = LocationLayoutManager.LocationLayout.LocationDoodads.FirstOrDefault(t => t.Position.X == xPosition && t.Position.Y == yPosition);
                if (tile != null)
                {
                    LocationLayoutManager.LocationLayout.LocationDoodads.Remove(tile);
                }
            }

            Draw();
        }
    }
}