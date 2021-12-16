using Library.Assets;
using Library.Domain;
using Library.Graphics;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Library.GameState.BagState
{
    public static class BagStateDrawingManager
    {
        public static void Draw(SpriteBatch spriteBatch)
        {
            GraphicsHelper.DrawWallpaper(spriteBatch, TextureName.BagWallpaper);
            DrawingManager.DrawSingleString(spriteBatch, BagStateGraphicsHelper.GetTitleDrawingObject());

            int index = 0;
            List<IDrawingString> drawingStrings = new List<IDrawingString>();
            Bag bag = GameStateManager.Instance.GetPlayer().CharacterState.Bag;
            if (BagStateManager.BagState == BagStateState.Items)
            {
                foreach (ItemType item in bag.ItemsDictionary.Keys)
                {
                    if (bag.ItemsDictionary[item] > 0)
                    {
                        drawingStrings.Add(new GraphicsHelper.StringObject {
                            String = GraphicsHelper.GetFormattedString(item.ToString()),
                            Position = BagStateGraphicsHelper.GetItemTitlePosition(index)
                        });

                        drawingStrings.Add(new GraphicsHelper.CenteredStringObject
                        {
                            String = "x" + bag.ItemsDictionary[item].ToString(),
                            Position = BagStateGraphicsHelper.GetItemCountPosition(index)
                        });
                    }

                    index++;
                }
            }

            DrawingManager.DrawStringBatch(spriteBatch, drawingStrings);
            DrawingManager.DrawSingle(spriteBatch, new GraphicsHelper.Cursor { Position = BagStateGraphicsHelper.GetCursorPosition(BagStateManager.ItemIndex) });
        }

    }
}