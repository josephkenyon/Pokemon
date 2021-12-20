using Library.Domain;
using Microsoft.Xna.Framework;

namespace Library.World
{
    public class Item : BaseObject
    {
        public ItemName ItemName { get; set; }
        public int Count { get; set; }

        public Item() { }
        public Item(ItemName itemType, Point position, int count = 1)
        {
            ItemName = itemType;
            Position = new Vector(position);
            TextureName = TextureName.Effects;
            SpritePosition = new Vector(0, 3);
            Count = count;
        }
    }
}