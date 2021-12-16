using Library.Assets;
using Library.Content;
using Library.Domain;
using Library.World;
using System.Linq;

namespace Library.GameState.Base
{

    public static class SpriteEffectsManager
    {
        public static void CharacterMovementCompleted(Character character)
        {
            LocationLayout locationLayout = LocationManager.LocationLayouts[BaseStateManager.Instance.GetPlayerLocation()];

            if (locationLayout.BackgroundGrassTiles.Values.Any(tile => CollisionHandler.AreColliding(character, tile)))
            {
                BaseStateManager.Instance.LocationStates[BaseStateManager.Instance.GetPlayerLocation()].SpriteEffects.Add(new SpriteEffect
                {
                    TextureName = TextureName.Animation,
                    SpritePosition = new Vector(0),
                    Position = character.GetPosition() / Constants.ScaledTileSize + new Vector(0, 1),
                    Size = new Vector(1),
                    NumFrames = 3
                });
            }
        }
    }
}
