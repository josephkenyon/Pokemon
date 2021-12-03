using Library.Content;
using Library.Domain;
using Library.GameState;
using Library.GameState.Battle;
using Library.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Library.GameState.Base.MessageState
{
    public static class MessageStateDrawingManager
    {
        public static void Draw(SpriteBatch spriteBatch) {
            DrawingManager.DrawSingle(spriteBatch, new MessageObject());
            DrawingManager.DrawString(spriteBatch, new MessageStringObject { Position = new Vector(24, 24) * Constants.Scaler, String = MessageStateManager.GetMessage() });
        }

        public class MessageObject : IDrawingObject
        {
            public Rectangle GetSourceRectangle() => BattleGraphicsHelper.BattleMessageSourceRectangle;
            public Texture2D GetTexture() => TextureManager.BasicTextures[TextureName.Effects];

            public Vector2 GetPosition() => (Vector.One * Constants.ScaledTileSize).ToVector2();
            public Color GetColor() => Color.White;
            public bool WhiteFlash() => false;
            public Vector2 GetScale() => new Vector2(0.75f, 0.75f);
            public SpriteEffects GetSpriteEffects() => SpriteEffects.None;
        }

        public class MessageStringObject : IDrawingString
        {
            public string String { get; set; }
            public Vector Position { get; set; }

            public string GetString() => String;
            public Vector2 GetPosition() => Position.ToVector2();
        }
    }
}
