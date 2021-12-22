using Library.Content;
using Library.Domain;
using Library.GameState.Base;
using Library.Graphics;
using Library.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Library.Base
{
    public class StitchDrawingManager : IBaseDrawingManager
    {
        public static StitchDrawingManager Instance { get; private set; }
        public static StitchHelperObject StitchHelperObject { get; set; }

        public static void Initialize()
        {
            Instance = new StitchDrawingManager();
        }

        public void DrawBatch(SpriteBatch spriteBatch, IEnumerable<IBaseDrawableObject> drawingObjects)
        {
            List<StitchDrawingObject> stitchDrawingObjects = new List<StitchDrawingObject>();

            foreach (IBaseDrawableObject drawingObject in drawingObjects)
            {
                stitchDrawingObjects.Add(ConvertStitchDrawableObject(drawingObject));
            }

            DrawingManager.DrawBatch(spriteBatch, stitchDrawingObjects);
        }

        public void DrawSingle(SpriteBatch spriteBatch, IBaseDrawableObject drawingObject)
        {
            DrawingManager.DrawSingle(spriteBatch, ConvertStitchDrawableObject(drawingObject));
        }

        public static StitchDrawingObject ConvertStitchDrawableObject(IBaseDrawableObject drawingObject)
        {
            return new StitchDrawingObject
            {
                Position = drawingObject.GetPosition(),
                SourceRectangle = drawingObject.GetSourceRectangle(),
                TextureName = drawingObject.GetTextureName(),
                SpriteEffects = drawingObject.GetSpriteEffects()
            };
        }
    }

    public class StitchDrawingObject : IDrawingObject
    {
        public TextureName TextureName { get; set; }
        public Rectangle SourceRectangle { get; set; }
        public Vector Position { get; set; }
        public SpriteEffects SpriteEffects { get; set; }
        public Vector2 GetPosition() => BaseDrawingManager.CameraTranslation(Position).ToVector2() + StitchDrawingManager.StitchHelperObject.OffsetPoint.ToVector2() * Constants.ScaledTileSize;
        public Rectangle GetSourceRectangle() => SourceRectangle;
        public Texture2D GetTexture() => TextureManager.BasicTextures[TextureName];
        public SpriteEffects GetSpriteEffects() => SpriteEffects;
    }

    public class StitchHelperObject
    {
        public LocationStitch LocationStitch { get; set; }
        public LocationName Location { get; set; }
        public Direction Direction { get; set; }
        public Point OffsetPoint { get; set; }

        public StitchHelperObject(LocationStitch locationStitch)
        {
            LocationName currentLocation = BaseStateManager.Instance.GetPlayerLocation();
            Location = locationStitch.LocationA == currentLocation ? locationStitch.LocationB : locationStitch.LocationA;
            int offset = locationStitch.LocationAGreatest - locationStitch.LocationBLeast + 1;
            int secondOffset = locationStitch.LocationA == currentLocation ? -locationStitch.Offset : locationStitch.Offset;
            OffsetPoint = Point.Zero;

            if (locationStitch.Orientation == Orientation.Vertical)
            {
                Direction = locationStitch.LocationA == currentLocation ? Direction.Down : Direction.Up;
                OffsetPoint = new Point(secondOffset, Direction == Direction.Up ? -offset : offset);
            }
            else
            {
                Direction = locationStitch.LocationA == currentLocation ? Direction.Right : Direction.Left;
                OffsetPoint = new Point(Direction == Direction.Right ? -offset : offset, secondOffset);
            }

            LocationStitch = locationStitch;
        }
    }
}