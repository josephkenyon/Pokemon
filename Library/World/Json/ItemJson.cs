using Library.Domain;
using Microsoft.Xna.Framework;

namespace Library.World.Json
{
    public class ItemJson
    {
        public ItemName ItemName { get; set; }
        public int? Count { get; set; }
        public Point Position { get; set; }
    }
}
