using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace LocationDesigner.World
{
    public class NPCJson
    {
        public Point SpriteLocation { get; set; }
        public Point Position { get; set; }
        public List<string> Messages { get; set; }
    }
}