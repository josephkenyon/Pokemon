using System.Linq;
using Library.Base;
using Library.Content;
using Library.Domain;
using Library.GameState.Base;
using Library.World;
using Microsoft.Xna.Framework;

namespace Library.GameState
{
    public static class CollisionHandler
    {
        public static LocationName? NewStitchLocationName { get; set; }
        public static Point NewStitchLocation { get; set; }

        public static bool IsValidMove(CharacterState characterState, Vector movement)
        {
            Point newTilePosition = (movement / Constants.ScaledTileSize).ToPoint();
            if (IsValidMove(characterState.CurrentLocation, characterState, newTilePosition))
            {
                return true;
            }
            else if (IsValidMove(StitchDrawingManager.StitchHelperObject.Location, characterState, WorldManager.GetStitchLocation(newTilePosition)))
            {
                NewStitchLocationName = StitchDrawingManager.StitchHelperObject.Location;
                NewStitchLocation = WorldManager.GetStitchLocation(newTilePosition);
                return true;
            }

            return false;
        }

        private static bool IsValidMove(LocationName locationName, CharacterState characterState, Point newTilePosition)
        {
            Vector movement = new Vector(newTilePosition) * Constants.ScaledTileSize;

            LocationLayout location = LocationManager.LocationLayouts[locationName];
            LocationState locationState = BaseStateManager.Instance.LocationStates[locationName];
            if (locationState.Characters.Exists(character => character.CharacterState.Position == movement))
            {
                return false;
            }
            else if (location.Portals.ContainsKey(newTilePosition))
            {
                return true;
            }
            else if (location.ForegroundTiles.ContainsKey(newTilePosition) && location.ForegroundTiles[newTilePosition].SpritePosition.Y > 0)
            {
                return false;
            }
            else if (location.BackgroundTiles.ContainsKey(newTilePosition))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool AreColliding(ICollidable first, ICollidable second) => first.GetCollisionRectangle().Intersects(second.GetCollisionRectangle());
    }
}
