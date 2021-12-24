using LocationDesigner.Domain;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace LocationDesigner.World
{
    public class NPCJson
    {
        public Point SpriteLocation { get; set; }
        public Point Position { get; set; }
        public string CharacterName { get; set; }
        public List<Message> Messages { get; set; }
        public string ItemName { get; set; }
        public int? ItemCount { get; set; }
    }
}