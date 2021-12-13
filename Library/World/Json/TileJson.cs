using Library.Domain;
using Microsoft.Xna.Framework;

namespace Library.World.Json
{
    public class TileJson
    {
        public Point Position { get; set; }
        public LocationDoodad LocationDoodad { get; set; }
        public Point SpritePosition { get; set; }
    }
}
