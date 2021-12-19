using Library.Domain;
using Microsoft.Xna.Framework;

namespace Library.World
{
    public class Item : BaseObject
    {
        public ItemName ItemType { get; set; }

        public Item() { }
        public Item(ItemName itemType, Point position)
        {
            ItemType = itemType;
            Position = new Vector(position);
        }
    }
}