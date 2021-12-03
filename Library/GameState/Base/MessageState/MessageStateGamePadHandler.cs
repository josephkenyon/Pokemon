using Library.Assets;
using Library.Content;
using Library.Domain;
using Library.GameState.Battle;
using Library.GameState.Input;
using Library.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Library.GameState.Base.MessageState
{
    public static class MessageStateGamePadHandler
    {
        public static void Update()
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

            if (gamePadState.Buttons.A == ButtonState.Pressed && GameStateManager.Instance.InputDebounceTimer == 0)
            {
                MessageStateManager.CompleteMessage();
                GameStateManager.Instance.InputDebounceTimer = Constants.MenuActivationDebounceLong;
                return;
            }
        }
    }
}
