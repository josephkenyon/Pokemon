using Library.Assets;
using Library.Domain;
using System.Collections.Generic;

namespace Library.GameState
{
    public class CharacterState
    {
        public Bag Bag { get; set; }
        public List<Pokemon> Pokemon { get; set; }
        public LocationName CurrentLocation { get; set; }
        public Vector Position { get; set; }
        public Direction Direction { get; set; }
        public bool IsMoving { get; set; }
        public Vector MovementPath { get; set; }
        public Vector TileSetPosition => Position / Constants.ScaledTileSize;
        public int CurrentFrame { get; set; }
        public int FrameSkip { get; set; }
        public List<Badge> Badges { get; set; }

        public void IncrementFrame()
        {
            if (FrameSkip == 5)
            {
                CurrentFrame = CurrentFrame == 2 ? 0 : CurrentFrame + 1;
                FrameSkip = 0;
            }
            else
            {
                FrameSkip += 1;
            }
        }

        public void StartMoving(Direction direction)
        {
            Direction = direction;
            MovementPath = MovementHandler.GetNewPath(direction, Position);
            IsMoving = true;
        }
    }
}
