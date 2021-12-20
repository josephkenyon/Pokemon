using Library.Domain;

namespace Library.Assets.Json
{
    public class ItemsJson
    {
        public ItemName ItemName { get; set; }
        public ItemType ItemType { get; set; }
        public int? Heal { get; set; }
        public int Level { get; set; }
    }
}
