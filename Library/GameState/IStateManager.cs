using Microsoft.Xna.Framework.Graphics;

namespace Library.GameState
{
    public interface IStateManager
    {
        bool Update();
        void Draw(SpriteBatch spriteBatch);
    }
}