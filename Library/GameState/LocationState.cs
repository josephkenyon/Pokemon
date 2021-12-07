using System.Collections.Generic;
using System.Linq;
using Library.Assets;
using Library.Base;
using Library.Content;
using Library.Domain;
using Library.GameState.Base;
using Library.GameState.Base.TransitionState;
using Microsoft.Xna.Framework.Graphics;

namespace Library.GameState
{
    public class LocationState
    {
        public List<Character> Characters { get; set; }
        public List<SpriteEffect> SpriteEffects { get; set; }
        public LocationName Name { get; set; }

        public LocationState() {
            SpriteEffects = new List<SpriteEffect>();
        }

        public void Update()
        {
            if (BaseStateManager.Instance.BaseState != BaseState.Message)
            {
                Characters.ForEach(character => character.Update());
            }

            SpriteEffects.Where(effect => !effect.Update()).ToList().ForEach(effect => SpriteEffects.Remove(effect));
            
            LocationManager.LocationLayouts[Name].BackgroundGrassTiles.Select(tile => tile.Value).Where(tile => tile.NumFrames != null).ToList().ForEach(tile => tile.Update());
            LocationManager.LocationLayouts[Name].ForegroundGrassTiles.Select(tile => tile.Value).Where(tile => tile.NumFrames != null).ToList().ForEach(tile => tile.Update());

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Characters.OrderBy(character => character.CharacterState.Position.Y).ToList().ForEach(character => BaseDrawingManager.Instance.DrawSingle(spriteBatch, character));
            BaseDrawingManager.Instance.DrawBatch(spriteBatch, SpriteEffects);
        }
    }
}
