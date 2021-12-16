using Library.Base;
using Library.Domain;
using Library.GameState;
using Microsoft.Xna.Framework;

namespace Library.Assets
{
    public abstract class Character : IBaseDrawableObject, IAnimatedAsset, ICollidable
    {
        public CharacterName? Name { get; set; }
        public Vector SpriteSize { get; set; }
        public CharacterState CharacterState { get; set; }
        public virtual int NumberOfFrames => 3;

        public Character() {
            SpriteSize = new Vector(1, 2);
        }

        public bool Update()
        {
            if (CharacterState is NPCState npcState)
            {
                npcState.Update();
            }

            if (CharacterState.IsMoving)
            {
                CharacterState.IncrementFrame();
                MovementHandler.MoveCharacter(this);
            }

            if (CharacterState.IsJumping || CharacterState.IsFalling)
            {
                CharacterState.IncrementJump();
            }

            return true;
        }

        public IBaseDrawableObject GetShadow()
        {
            return new Shadow { Position = new Vector(CharacterState.Position.X, CharacterState.Position.Y + CharacterState.ZPosition) };
        }

        public void Move(Vector newPosition)
        {
            CharacterState.Position = newPosition;
        }

        public Vector GetPosition() => CharacterState.Position - new Vector(0, 1 * Constants.ScaledTileSize + CharacterState.ZPosition);
        public virtual Rectangle GetSourceRectangle() => new Rectangle(
            location: (new Vector(SpriteSize.X * CharacterState.CurrentFrame, SpriteSize.Y * (int)CharacterState.Direction) * Constants.TileSize).ToPoint(),
            size: (SpriteSize * Constants.TileSize).ToPoint()
        );

        public Rectangle GetCollisionRectangle() => new Rectangle(CharacterState.Position.ToPoint(), (Vector.One * Constants.ScaledTileSize).ToPoint());

        public abstract TextureName GetTextureName();

        private class Shadow : IBaseDrawableObject
        {
            public Vector Position { get; set; }

            public Vector GetPosition() => Position;

            public Rectangle GetSourceRectangle() => new Rectangle(1 * Constants.TileSize, 3 * Constants.TileSize, Constants.TileSize, Constants.TileSize);

            public TextureName GetTextureName() => TextureName.Effects;
        }
    }
}
