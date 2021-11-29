using Library.Content;
using Library.Domain;
using Library.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library.Menu
{
    public static class MenuDrawingManager
    {
        public static void DrawMenuItem(SpriteBatch spriteBatch, Rectangle sourceRectangle, Rectangle targetRectangle)
        {
            DrawingManager.DrawSingle(spriteBatch, new MenuDrawingObject { SourceRectangle = sourceRectangle, Position = new Vector(targetRectangle.Location) });
        }

        public class MenuDrawingObject : IDrawingObject
        {
            public Rectangle SourceRectangle { get; set; }
            public Vector Position { get; set; }

            public Rectangle GetSourceRectangle() => SourceRectangle;
            public Vector2 GetPosition() => Position.ToVector2();
            public Texture2D GetTexture() => TextureManager.BasicTextures[TextureName.Effects];
        }
    }
}
 