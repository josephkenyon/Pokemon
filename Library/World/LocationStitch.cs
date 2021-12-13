using Library.Domain;

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
    }
}
