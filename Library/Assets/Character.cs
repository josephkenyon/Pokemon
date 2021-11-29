using Library.Base;
using Library.Domain;
using Library.GameState;
using Microsoft.Xna.Framework;

namespace Library.Assets
{
    public abstract class Character : IBaseDrawableObject
    {
        public string Name { get; set; }
        public TextureName TextureName { get; set; }
        public Vector SpriteSize { get; set; }
        public CharacterState CharacterState { get; set; }

        public void Update()
        {
            if (CharacterState.IsMoving)
            {
                CharacterState.IncrementFrame();
                MovementHandler.MoveCharacter(this);
            }

        }

        public void Move(Vector newPosition)
        {
            CharacterState.Position = newPosition;
        }

        public Vector GetPosition() => CharacterState.Position - new Vector(0, 1 * Constants.ScaledTileSize);
        public TextureName GetTextureName() => TextureName;
        public Rectangle GetSourceRectangle() => new Rectangle(
            location: (new Vector(SpriteSize.X * CharacterState.CurrentFrame, SpriteSize.Y * (int)CharacterState.Direction) * Constants.TileSize).ToPoint(),
            size: (SpriteSize * Constants.TileSize).ToPoint()
        );
    }
}
