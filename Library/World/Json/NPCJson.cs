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
        public List<string> Messages { get; set; }
    }
}