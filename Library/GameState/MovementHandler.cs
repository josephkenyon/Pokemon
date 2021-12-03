using Library.Assets;
using Library.Content;
using Library.Domain;
using Library.GameState.Base;
using Library.GameState.Base.TransitionState;
using Library.World;

namespace Library.GameState
{
    public static class MovementHandler
    {
        public static Vector GetNewPath(Direction direction, Vector position)
        {
            switch (direction)
            {
                case Direction.Up:
                    return new Vector(position.X, position.Y - Constants.ScaledTileSize);
                case Direction.Down:
                    return new Vector(position.X, position.Y + Constants.ScaledTileSize);
                case Direction.Left:
                    return new Vector(position.X - Constants.ScaledTileSize, position.Y);
                case Direction.Right:
                    return new Vector(position.X + Constants.ScaledTileSize, position.Y);
                default:
                    return Vector.Zero;
            }
        }

        public static void MoveCharacter(Character character)
        {
            Vector position = character.CharacterState.Position;
            Vector movementPath = character.CharacterState.MovementPath;

            int xDiff = position.X - movementPath.X;
            int yDiff = position.Y - movementPath.Y;

            if (xDiff < 0)
            {
                character.Move(new Vector(position.X + Constants.CharacterSpeed, position.Y));
            }
            else if (xDiff > 0)
            {
                character.Move(new Vector(position.X - Constants.CharacterSpeed, position.Y));
            }
            else if (yDiff < 0)
            {
                character.Move(new Vector(position.X, position.Y + Constants.CharacterSpeed));
            }
            else if (yDiff > 0)
            {
                character.Move(new Vector(position.X, position.Y - Constants.CharacterSpeed));
            }
            else
            {
                LocationLayout location = LocationManager.LocationLayouts[character.CharacterState.CurrentLocation];
                
                if (location.Portals.ContainsKey((movementPath / Constants.ScaledTileSize).ToPoint()))
                {
                    PortalJson portal = location.Portals[(movementPath / Constants.ScaledTileSize).ToPoint()];
                    TransitionStateManager.StartTransition(portal.ToLocationName, portal.Coordinate);
                }
                else
                {
                    SpriteEffectsManager.CharacterMovementCompleted(character);
                }

                character.CharacterState.IsMoving = false;
            }
        }
    }
}