using Microsoft.Xna.Framework;
using System;

namespace LocationDesigner.World
{
    public class TileJson : IComparable
    {
        public Point Position { get; set; }
        public Point SpritePosition { get; set; }

        public int CompareTo(object obj)
        {
            if (obj == null) 
                return 1;

            TileJson otherTile = obj as TileJson;

            if (otherTile.Position.X > Position.X)
            {
                return -1;
            }
            else if (otherTile.Position.Y > Position.Y)
            {
                return -1;
            }
            else if (otherTile.Position.X < Position.X)
            {
                return 1;
            }
            else if (otherTile.Position.Y < Position.Y)
            {
                return 1;
            }

            return 0;
        }
    }
}