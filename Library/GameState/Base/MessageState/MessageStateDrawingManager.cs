using Library.Content;
using Library.Domain;
using Library.GameState.Battle;
using Library.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library.GameState.Base.MessageState
{
    public static class MessageStateDrawingManager
    {
        public static void Draw(SpriteBatch spriteBatch)
        {
            DrawingManager.DrawSingle(spriteBatch, new MessageObject());

            string message = MessageStateManager.GetMessage();
            Vector2 messageSize = DrawingManager.DefaultFont.MeasureString(message);
            int maxWidth = (int)(BattleGraphicsHelper.BattleMessageTextureSize.X * Constants.ScaledTileSize - (4 * Constants.ScaledTileSize));
            int index = message.Length;
            if (messageSize.X > maxWidth)
            {
                while (DrawingManager.DefaultFont.MeasureString(message.Substring(0, index)).X > maxWidth || !message.Substring(0, index).EndsWith(" "))
                {
                    index--;
                }
            }

            if (index != message.Length)
            {
                DrawingManager.DrawString(spriteBatch, new MessageStringObject { Position = new Vector(22, 21) * Constants.Scaler, String = message.Substring(0, index) });
                DrawingManager.DrawString(spriteBatch, new MessageStringObject { Position = new Vector(22, 32) * Constants.Scaler, String = message.Substring(index, message.Length - index) });
            }
            else
            {
                DrawingManager.DrawString(spriteBatch, new MessageStringObject { Position = new Vector(22, 21) * Constants.Scaler, String = MessageStateManager.GetMessage() });
            }
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
