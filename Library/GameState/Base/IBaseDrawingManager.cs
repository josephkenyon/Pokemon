using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Library.Base
{
    public interface IBaseDrawingManager
    {
        void DrawBatch(SpriteBatch spriteBatch, IEnumerable<IBaseDrawableObject> drawingObjects);
        void DrawSingle(SpriteBatch spriteBatch, IBaseDrawableObject drawingObject);
    }
}