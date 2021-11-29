using Library.Domain;
using Microsoft.Xna.Framework.Input;

namespace Library.GameState.Input
{
    public static class GamePadHelper
    {
        public static Direction? GetDPadDirection(GamePadState gamePadState)
        {

            if (gamePadState.DPad.Up == ButtonState.Pressed)
            {
                return Direction.Up;
            }
            else if (gamePadState.DPad.Down == ButtonState.Pressed)
            {
                return Direction.Down;
            }
            else if (gamePadState.DPad.Left == ButtonState.Pressed)
            {
                return Direction.Left;
            }
            else if (gamePadState.DPad.Right == ButtonState.Pressed)
            {
                return Direction.Right;
            }

            return null;
        }
    }
}
