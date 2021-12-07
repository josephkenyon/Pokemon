using Library.Content;
using Library.Domain;
using Library.GameState;
using Library.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Library.Base
{
    public class BaseDrawingManager : IBaseDrawingManager
    {
        public static BaseDrawingManager Instance { get; private set; }
        public static Vector CameraTranslation(Vector Vector) => Vector - GameStateManager.Instance.CameraLocation + (new Vector(Constants.ResolutionWidth, Constants.ResolutionHeight) / 2);
        
        public static void Initialize()
        {
            Instance = new BaseDrawingManager();
        }

        public void DrawBatch(SpriteBatch spriteBatch, IEnumerable<IBaseDrawableObject> drawingObjects)
        {
            List<BaseDrawingObject> baseDrawingObjects = new List<BaseDrawingObject>();

            foreach (IBaseDrawableObject drawingObject in drawingObjects)
            {
                baseDrawingObjects.Add(ConvertBaseDrawableObject(drawingObject));
            }

            DrawingManager.DrawBatch(spriteBatch, baseDrawingObjects);
        }

        public void DrawSingle(SpriteBatch spriteBatch, IBaseDrawableObject drawingObject)
        {
            DrawingManager.DrawSingle(spriteBatch, ConvertBaseDrawableObject(drawingObject));
        }

        public static BaseDrawingObject ConvertBaseDrawableObject(IBaseDrawableObject drawingObject) {
            return new BaseDrawingObject
            {
                Position = drawingObject.GetPosition(),
                SourceRectangle = drawingObject.GetSourceRectangle(),
                TextureName = drawingObject.GetTextureName(),
                SpriteEffects = drawingObject.GetSpriteEffects()
            };
        }
    }

    public class BaseDrawingObject : IDrawingObject
    {
        public TextureName TextureName { get; set; }
        public Rectangle SourceRectangle { get; set; }
        public Vector Position { get; set; }
        public SpriteEffects SpriteEffects { get; set; }
        public Vector2 GetPosition() => BaseDrawingManager.CameraTranslation(Position).ToVector2();
        public Rectangle GetSourceRectangle() => SourceRectangle;
        public Texture2D GetTexture() => TextureManager.BasicTextures[TextureName];
        public SpriteEffects GetSpriteEffects() => SpriteEffects;
    }

    public interface IBaseDrawableObject
    {
        TextureName GetTextureName();
        Rectangle GetSourceRectangle();
        Vector GetPosition();
        SpriteEffects GetSpriteEffects() => SpriteEffects.None;
    }
}
