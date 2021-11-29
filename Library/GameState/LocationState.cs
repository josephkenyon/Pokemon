using System.Collections.Generic;
using Library.Assets;
using Library.Base;
using Library.Domain;
using Microsoft.Xna.Framework.Graphics;

namespace Library.GameState
{
    public class LocationState
    {
        public List<Character> Characters { get; set; }
        public LocationName Name { get; set; }

        public void Update()
        {
            Characters.ForEach(character => character.Update());
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            BaseDrawingManager.DrawBatch(spriteBatch, Characters);
        }
    }
}
