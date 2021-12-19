using Library.Assets;
using Library.Domain;
using Library.GameState.Battle;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Library.GameState.Base.GamePadHandling
{
    public static class BaseGamePadHandler
    {
        public static void Update()
        {
            BaseButtonsHandler.Update();
            BaseMovementHandler.Update();
        }
    }
}
