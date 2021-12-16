using Library.Domain;
using Microsoft.Xna.Framework;

namespace Library.World
{
    public class TileHelper
    {
        public static bool TileIsJumpable(Tile tile)
        {
            Point spriteLocation = (new Vector(tile.GetSourceRectangle().Location) / Constants.TileSize).ToPoint();
            return spriteLocation.X > 2 && spriteLocation.X < 6 && spriteLocation.Y == 8; 
        }
    }
}