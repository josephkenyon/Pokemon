using Library.Content;
using Library.Domain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library.GameState.Battle.BattleDrawingObjects
{
    public class ExpBarDrawingObject : BattleDrawingObject
    {
        public HealthBarShell HealthBarShell { get; set; }
        public Rectangle PokemonSourceRectangle { get; set; }
        public override Texture2D GetTexture() => TextureManager.BasicTextures[TextureName.EmptyWhiteTexture];
        public override Vector2 GetPosition() => Position + BattleGraphicsHelper.ExperienceBarLocation * Constants.Scaler * BattleGraphicsHelper.Scale;
        public override Color GetColor() => Color.CornflowerBlue;
        public override Vector2 GetScale() => new Vector2(47.25f * BattleGraphicsHelper.Scale * HealthBarShell.PokemonDrawingObject.Pokemon.Experience / HealthBarShell.PokemonDrawingObject.Pokemon.ExperienceToLevelUp, 3.75f * BattleGraphicsHelper.Scale);
    }
}
