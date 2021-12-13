using Library.Domain;
using Library.GameState.Input;
using Microsoft.Xna.Framework.Input;

namespace Library.GameState.Base.GamePadHandling
{
    public static class BaseMovementHandler
    {
        public static void Update(GamePadState gamePadState)
        {
            Direction? nullableDir = !GameStateManager.Instance.GetPlayer().CharacterState.IsMoving ? GamePadHelper.GetDPadDirection(gamePadState) : null;

            if (nullableDir != null)
            {
                Direction direction = (Direction)nullableDir;
                Vector newLocation = MovementHandler.GetNewPath(direction, GameStateManager.Instance.GetPlayer().CharacterState.Position);
                if (CollisionHandler.IsValidMove(GameStateManager.Instance.GetPlayer().CharacterState, newLocation))
                {
                    GameStateManager.Instance.GetPlayer().CharacterState.StartMoving(direction);
                }
                GameStateManager.Instance.GetPlayer().CharacterState.Direction = direction;
            }
        }
    }
}
