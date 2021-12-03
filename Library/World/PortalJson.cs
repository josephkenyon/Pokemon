using Library.Domain;
using Microsoft.Xna.Framework;

namespace Library.World
{
    public class PortalJson
    {
        public LocationName ToLocationName { get; set; }
        public Point Position { get; set; }
        public Point Coordinate { get; set; }
    }
}