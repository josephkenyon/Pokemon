using Library.Domain;
using Library.GameState;
using Microsoft.Xna.Framework;
using System;

namespace Library.World
{
    public class LocationStitch
    {
        public LocationName LocationA { get; set; }
        public LocationName LocationB { get; set; }

        public Orientation Orientation { get; set; }
        public int Offset { get; set; }
        public int LocationAGreatest { get; set; }
        public int LocationALeast { get; set; }
        public int LocationBGreatest { get; set; }
        public int LocationBLeast { get; set; }

        public int GetEdge(LocationName fromLocation)
        {
            if (LocationA == fromLocation)
            {
                return LocationAGreatest;
            }
            else
            {
                return LocationBLeast;
            }
        }

        public int GetDistanceFromPlayer()
        {
            CharacterState characterState = GameStateManager.Instance.GetPlayer().CharacterState;
            Point locationPoint = characterState.TileSetPosition.ToPoint();
            LocationName locationName = characterState.CurrentLocation;

            return Math.Abs(Orientation == Orientation.Vertical
                        ? GetEdge(locationName) - locationPoint.Y
                        : GetEdge(locationName) - locationPoint.X);
        }
    }
}