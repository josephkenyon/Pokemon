using Library.Assets;
using Library.Domain;
using System.Collections.Generic;

namespace Library.GameState
{
    public class CharacterState : FrameState
    {
        public Bag Bag { get; set; }
        public List<Pokemon> Pokemon { get; set; }
        public LocationName CurrentLocation { get; set; }
        public Vector Position { get; set; }
        public Direction Direction { get; set; }
        public bool IsMoving { get; set; }
        public bool IsJumping { get; set; }
        public bool IsFalling { get; set; }
        public int JumpCounter { get; set; }
        public float ZPosition { get; set; }
        public Vector MovementPath { get; set; }
        public Vector TileSetPosition => Position / Constants.ScaledTileSize;
        public List<Badge> Badges { get; set; }

        public CharacterState(IAnimatedAsset parentAsset) : base(parentAsset)
        {
            Position = Vector.Zero;
            MovementPath = Vector.Zero;
            Pokemon = new List<Pokemon>();
            Bag = new Bag();

            Direction = Direction.Down;
        }

        public void StartMoving(Direction direction)
        {
            Direction = direction;
            MovementPath = MovementHandler.GetNewPath(direction, Position);
            IsMoving = true;
        }

        public void StartJumping(Direction direction)
        {
            Direction = direction;
            MovementPath = MovementHandler.GetNewJumpPath(direction, Position);
            IsMoving = true;
            IsJumping = true;
        }

        public void IncrementJump()
        {
            if (JumpCounter == 16)
            {
                IsJumping = false;
                IsFalling = true;
            }
            else if (IsFalling && ZPosition <= 0)
            {
                ZPosition = 0;
                JumpCounter = 0;
                IsFalling = false;
            }

            if (IsJumping)
            {
                ZPosition += Constants.Scaler / 2f;
                JumpCounter++;
            }
            else if (IsFalling)
            {
                ZPosition -= Constants.Scaler / 1.5f;
                if (ZPosition < 0)
                {
                    ZPosition = 0;
                }

                JumpCounter--;
            }

        }
    }
}
