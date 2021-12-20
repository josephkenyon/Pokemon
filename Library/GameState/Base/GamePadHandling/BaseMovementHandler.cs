using Library.Domain;
using Library.GameState.Input;

namespace Library.GameState.Base.GamePadHandling
{
    public static class BaseMovementHandler
    {
        public static void Update()
        {
            Direction? nullableDir = !GameStateManager.Instance.GetPlayer().CharacterState.IsMoving ? GamePadHelper.GetHeldDPadButton() : null;

            if (nullableDir != null)
            {
                Direction direction = (Direction)nullableDir;

                CharacterState playerState = GameStateManager.Instance.GetPlayer().CharacterState;

                Vector newLocation = MovementHandler.GetNewPath(direction, playerState.Position);

                if (CollisionHandler.IsValidMove(playerState, newLocation))
                {
                    playerState.StartMoving(direction);

                }
                else if (CollisionHandler.IsJumpableMove(playerState, newLocation))
                {
                    playerState.StartJumping(direction);
                }
                else
                {
                    playerState.Direction = direction;
                }
            }
        }
    }
}
