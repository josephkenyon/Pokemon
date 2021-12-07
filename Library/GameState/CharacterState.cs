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
        public Vector MovementPath { get; set; }
        public Vector TileSetPosition => Position / Constants.ScaledTileSize;
        public List<Badge> Badges { get; set; }
        public override int NumberOfFrames => 3;

        public CharacterState() {
            Position = Vector.Zero;
        }

        public void StartMoving(Direction direction)
        {
            Direction = direction;
            MovementPath = MovementHandler.GetNewPath(direction, Position);
            IsMoving = true;
        }
    }
}
