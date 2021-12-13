using Library.Domain;
using Microsoft.Xna.Framework;

namespace Library.GameState.Base.TransitionState
{
    public class TeleportTransaction
    {
        public CharacterName CharacterName { get; set; }
        public LocationName? ToLocationName { get; set; }
        public Point ToLocationPoint { get; set; }
        public Direction? FinalDirection { get; set; }
        public bool Instant { get; set; }
    }
}