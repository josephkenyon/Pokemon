using Library.Content;
using Library.Domain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library.Graphics
{
    public static class GraphicsHelper
    {
        public static string GetFormattedString(string input) => input.Replace("_", " ");

        public static Rectangle GetSourceRectangle(Vector position, Vector size)
            => new Rectangle((position * Constants.TileSize).ToPoint(), (size * Constants.TileSize).ToPoint());
       
        public static void DrawWallpaper(SpriteBatch spriteBatch, TextureName textureName)
        {
            DrawingManager.DrawSingle(spriteBatch, new Wallpaper { TextureName = textureName });
        }

        public class Cursor : IDrawingObject
        {
            public Vector Position { get; set; }

            public Rectangle GetSourceRectangle() => GraphicsHelper.GetSourceRectangle(new Vector(1, 0), Vector.One);
            public Texture2D GetTexture() => TextureManager.BasicTextures[TextureName.Effects];
            public Vector2 GetPosition() => Position.ToVector2();
        }

        public class Wallpaper : IDrawingObject
        {
            public TextureName TextureName { get; set; }

            public Rectangle GetSourceRectangle() => new Rectangle(Point.Zero, new Point(384, 216));
            public Texture2D GetTexture() => TextureManager.BasicTextures[TextureName];
            public Vector2 GetPosition() => Vector2.Zero;
        }

        public class StringObject : IDrawingString
        {
            public string String { get; set; }
            public Vector Position { get; set; }

            public string GetString() => String;
            public virtual Vector2 GetPosition() => Position.ToVector2();
        }

        public class CenteredStringObject : StringObject
        {
            public override Vector2 GetPosition() => new Vector2(Position.X - DrawingManager.DefaultFont.MeasureString(String).X / 2, Position.Y);
        }
    }
}
