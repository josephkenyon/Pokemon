using Library.Domain;
using Microsoft.Xna.Framework;

namespace Library.World
{
    public class Item : BaseObject
    {
        public ItemType ItemType { get; set; }

        public Item() { }
        public Item(ItemType itemType, Point position)
        {
            ItemType = itemType;
            Position = new Vector(position);
        }
    }
}