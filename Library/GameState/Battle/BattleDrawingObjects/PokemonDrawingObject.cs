using Library.Domain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library.GameState.Battle.BattleDrawingObjects
{
    public class PokemonDrawingObject : BattleDrawingObject
    {
        public BattlePokemon Pokemon { get; set; }
        public int Index { get; set; }
        public override Vector2 GetPosition() => Position * Constants.ScaledTileSize - GetSourceRectangle().Size.ToVector2() * Constants.Scaler / 2;
        public override Vector2 GetScale() => new Vector2(BattleGraphicsHelper.Scale, BattleGraphicsHelper.Scale);
        public override SpriteEffects GetSpriteEffects() => Direction == Direction.Left ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
        public override Color GetColor() => Pokemon.UsedMove || Pokemon.IsFainted ? new Color(155, 155, 155, 255) : Color.White * (
            (((BattleStateManager.Battle.State == BattleState.PokemonSelect || BattleStateManager.Battle.State == BattleState.MoveSelect) && Direction == Direction.Left)
            || BattleStateManager.Battle.State == BattleState.EnemySelect && Direction == Direction.Right)
            && Index == BattleStateManager.Battle.BattleCharacterStates[Direction].SelectedPokemonIndex ? BattleStateManager.Battle.Timer : 1f);
        public override bool WhiteFlash() => (((BattleStateManager.Battle.State == BattleState.PokemonSelect || BattleStateManager.Battle.State == BattleState.MoveSelect)
            && Direction == Direction.Left) || BattleStateManager.Battle.State == BattleState.EnemySelect && Direction == Direction.Right)
            && Index == BattleStateManager.Battle.BattleCharacterStates[Direction].SelectedPokemonIndex;
    }
}
