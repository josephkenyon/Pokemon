using Library.Domain;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Library.World.Json
{
    public class NPCJson
    {
        public Point SpriteLocation { get; set; }
        public Point Position { get; set; }
        public CharacterName? CharacterName { get; set; }
        public List<Message> Messages { get; set; }
        public bool Stationary { get; set; }
        public ItemName? ItemName { get; set; }
        public int? ItemCount { get; set; }
    }
}