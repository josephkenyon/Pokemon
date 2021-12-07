﻿using Microsoft.Xna.Framework;

namespace LocationDesigner.World
{
    public class PortalJson
    {
        public string ToLocationName { get; set; }
        public Point Position { get; set; }
        public Point Coordinate { get; set; }
        public bool HasRug { get; set; }
    }
}