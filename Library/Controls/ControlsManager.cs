using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Library.Controls
{
    public static class ControlsManager
    {
        private static GamePadState GamePadState => GamePad.GetState(PlayerIndex.One);
        public static bool APressed() => GamePadState.Buttons.A == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Space);
        public static bool BPressed() => GamePadState.Buttons.B == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.RightShift);
        public static bool StartPressed() => GamePadState.Buttons.Start == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape);
        public static bool LeftPressed() => GamePadState.DPad.Left == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Left);
        public static bool RightPressed() => GamePadState.DPad.Right == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Right);
        public static bool UpPressed() => GamePadState.DPad.Up == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Up);
        public static bool DownPressed() => GamePadState.DPad.Down == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Down);
    }
}
