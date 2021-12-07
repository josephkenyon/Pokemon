using Library.Domain;

namespace Library.World
{
    public class LocationStitch
    {
        public LocationName LocationA { get; set; }
        public LocationName LocationB { get; set; }

        public Orientation Orientation { get; set; }
        public int Offset { get; set; }
        public int LocationADistance { get; set; }
        public int LocationBDistance { get; set; }
    }
}
