using Library.Assets;
using Library.Content;
using Library.Domain;
using Library.World;

namespace Library.GameState
{
    public static class CollisionHandler
    {
        public static bool IsValidMove(Character character, Vector movement)
        {
            Vector newTilePosition = movement / Constants.ScaledTileSize;

            LocationLayout location = LocationManager.LocationLayouts[character.CharacterState.CurrentLocation];
            if (location.Portals.ContainsKey(newTilePosition.ToPoint()))
            {
                return true;
            }
            else if (location.ForegroundTiles.ContainsKey(newTilePosition.ToPoint()) && location.ForegroundTiles[newTilePosition.ToPoint()].SpritePosition.Y > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool AreColliding(ICollidable first, ICollidable second) => first.GetCollisionRectangle().Intersects(second.GetCollisionRectangle());
    }
}
