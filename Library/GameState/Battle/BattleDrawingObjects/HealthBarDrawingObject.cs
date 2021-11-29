using Library.Content;
using Library.Domain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library.GameState.Battle.BattleDrawingObjects
{
    public class HealthBarDrawingObject : BattleDrawingObject
    {
        public HealthBarShell HealthBarShell { get; set; }
        public override Texture2D GetTexture() => TextureManager.BasicTextures[TextureName.EmptyWhiteTexture];
        public override Vector2 GetPosition() => Position + BattleGraphicsHelper.HealthBarLocation * Constants.Scaler * BattleGraphicsHelper.Scale;
        public override Color GetColor() => Color.SpringGreen;
        public override Vector2 GetScale() => new Vector2(48f * BattleGraphicsHelper.Scale * HealthBarShell.PokemonDrawingObject.Pokemon.CurrentHealth / HealthBarShell.PokemonDrawingObject.Pokemon.GetStat(Stat.HP), 2f * BattleGraphicsHelper.Scale);
    }
}
