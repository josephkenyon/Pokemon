using Library.Domain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library.GameState.Battle.BattleDrawingObjects
{
    public class PokemonShadowDrawingObject : BattleDrawingObject
    {
        public BattlePokemon Pokemon { get; set; }
        public override Vector2 GetScale() => new Vector2(BattleGraphicsHelper.Scale, BattleGraphicsHelper.Scale / 2);
        public override Vector2 GetPosition() => Position * Constants.ScaledTileSize - GetSourceRectangle().Size.ToVector2() * Constants.Scaler / 2 + new Vector2(0f, GetSourceRectangle().Size.Y * 3.3f);
        public override SpriteEffects GetSpriteEffects() => SpriteEffects.FlipVertically | (Direction == Direction.Left ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
        public override Color GetColor() => Color.Black * 0.3f;
    }
}
