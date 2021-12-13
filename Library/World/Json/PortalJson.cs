using Library.Domain;
using Microsoft.Xna.Framework;

namespace Library.World.Json
{
    public class PortalJson
    {
        public LocationName ToLocationName { get; set; }
        public Point Position { get; set; }
        public Point Coordinate { get; set; }
        public bool HasRug { get; set; }

        public RugDrawingObject GetDrawingObject() => new RugDrawingObject { Position = Position };
    }
}