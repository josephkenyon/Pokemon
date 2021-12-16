using Library.Domain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Library.Graphics
{
    public static class DrawingManager
    {
        public static SpriteFont DefaultFont { get; private set; }
        public static SpriteFont SmallFont { get; private set; }
        private static void BeginSpriteBatch(SpriteBatch spriteBatch) => spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp);
        private static void EndSpriteBatch(SpriteBatch spriteBatch) => spriteBatch.End();

        public static void DrawBatch(SpriteBatch spriteBatch, IEnumerable<IDrawingObject> drawingObjects)
        {
            bool began = false;
            foreach (IDrawingObject drawingObject in drawingObjects)
            {
                if (!began)
                {
                    BeginSpriteBatch(spriteBatch);
                    began = true;
                }
                Draw(spriteBatch, drawingObject);
            }

            if (began)
            {
                EndSpriteBatch(spriteBatch);
            }
        }

        public static void DrawSingle(SpriteBatch spriteBatch, IDrawingObject drawingObject)
        {
            BeginSpriteBatch(spriteBatch);
            Draw(spriteBatch, drawingObject);
            EndSpriteBatch(spriteBatch);
        }

        public static void DrawSingleString(SpriteBatch spriteBatch, IDrawingString drawingString, bool small = false)
        {
            BeginSpriteBatch(spriteBatch);
            spriteBatch.DrawString(small ? SmallFont : DefaultFont, drawingString.GetString(), drawingString.GetPosition() + new Vector2(0, -3), Color.DarkGray);
            spriteBatch.DrawString(small ? SmallFont : DefaultFont, drawingString.GetString(), drawingString.GetPosition(), Color.DarkGray);
            spriteBatch.DrawString(small ? SmallFont : DefaultFont, drawingString.GetString(), drawingString.GetPosition() + new Vector2(4, 4), Color.DarkGray);
            EndSpriteBatch(spriteBatch);

            BeginSpriteBatch(spriteBatch);
            spriteBatch.DrawString(small ? SmallFont : DefaultFont, drawingString.GetString(), drawingString.GetPosition() + Vector2.One * 1, new Color(82, 82, 82));
            EndSpriteBatch(spriteBatch);
        }

        public static void DrawStringBatch(SpriteBatch spriteBatch, IEnumerable<IDrawingString> drawingStrings, bool small = false)
        {
            BeginSpriteBatch(spriteBatch);
            foreach (IDrawingString drawingString in drawingStrings)
            {
                spriteBatch.DrawString(small ? SmallFont : DefaultFont, drawingString.GetString(), drawingString.GetPosition() + new Vector2(0, -3), Color.DarkGray);
                spriteBatch.DrawString(small ? SmallFont : DefaultFont, drawingString.GetString(), drawingString.GetPosition(), Color.DarkGray);
                spriteBatch.DrawString(small ? SmallFont : DefaultFont, drawingString.GetString(), drawingString.GetPosition() + new Vector2(4, 4), Color.DarkGray);
            }
            EndSpriteBatch(spriteBatch);

            BeginSpriteBatch(spriteBatch);
            foreach (IDrawingString drawingString in drawingStrings)
            {
                spriteBatch.DrawString(small ? SmallFont : DefaultFont, drawingString.GetString(), drawingString.GetPosition() + Vector2.One * 1, new Color(82, 82, 82));
            }
            EndSpriteBatch(spriteBatch);
        }

        private static void Draw(SpriteBatch spriteBatch, IDrawingObject drawingObject)
        {
            Color color = drawingObject.GetColor();

            spriteBatch.Draw(
                texture: drawingObject.GetTexture(),
                position: drawingObject.GetPosition(),
                sourceRectangle: drawingObject.GetSourceRectangle(),
                color: color,
                rotation: 0f,
                origin: Vector2.Zero,
                scale: drawingObject.GetScale() * Constants.Scaler,
                effects: drawingObject.GetSpriteEffects(),
                layerDepth: 0f
            );

            if (drawingObject.WhiteFlash()) {
                spriteBatch.Draw(
                    texture: drawingObject.GetWhiteTexture(),
                    position: drawingObject.GetPosition(),
                    sourceRectangle: drawingObject.GetSourceRectangle(),
                    color: Color.White * ((255f - color.A) / 255f),
                    rotation: 0f,
                    origin: Vector2.Zero,
                    scale: drawingObject.GetScale() * Constants.Scaler,
                    effects: drawingObject.GetSpriteEffects(),
                    layerDepth: 0f
                );
            }
        }

        public static void Initialize(ContentManager content)
        {
            DefaultFont = content.Load<SpriteFont>("fonts/default");
            SmallFont = content.Load<SpriteFont>("fonts/small");
        }
    }

    public interface IDrawingObject
    {
        Texture2D GetTexture();
        Vector2 GetPosition();
        Rectangle GetSourceRectangle() => new Rectangle(0, 0, GetTexture().Width, GetTexture().Height);
        Color GetColor() => Color.White;
        SpriteEffects GetSpriteEffects() => SpriteEffects.None;
        Vector2 GetScale() => Vector2.One;
        bool WhiteFlash() => false;
        Texture2D GetWhiteTexture() => null;
    }

    public interface IDrawingString
    {
        string GetString();
        Vector2 GetPosition();
    }
}
