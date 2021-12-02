using Library.Assets;
using Library.Content;
using Library.Domain;

namespace Library.GameState
{
    public static class CollisionHandler
    {
        public static bool IsValidMove(Character character, Vector movement) {
            Vector newTilePosition = movement / Constants.ScaledTileSize;

            if (LocationManager.LocationLayouts[character.CharacterState.CurrentLocation].ForegroundTiles.ContainsKey(newTilePosition.ToPoint()))
            {
                return false;
            }
            else {
                return true;
            }
        }

        public static bool AreColliding(ICollidable first, ICollidable second) => first.GetCollisionRectangle().Intersects(second.GetCollisionRectangle());
    }
}
