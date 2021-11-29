using Library.Content;
using Library.Domain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library.GameState.Battle.BattleDrawingObjects
{
    public class HealthBarShell : BattleDrawingObject
    {
        public PokemonDrawingObject PokemonDrawingObject { get; set; }
        public override Vector2 GetScale() => new Vector2(BattleGraphicsHelper.Scale, BattleGraphicsHelper.Scale);
        public override Texture2D GetTexture() => TextureManager.BasicTextures[TextureName.HealthExpBar];
        public override Vector2 GetPosition() =>
            PokemonDrawingObject.Pokemon.Position.ToVector2() * Constants.ScaledTileSize
            - new Vector2(PokemonDrawingObject.GetSourceRectangle().Width * Constants.Scaler / 2, -PokemonDrawingObject.GetSourceRectangle().Height / 2 * Constants.Scaler)
            - new Vector2(GetTexture().Width / 8 * Constants.Scaler, GetTexture().Height * Constants.Scaler);
    }
}
