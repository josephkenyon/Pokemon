using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Library.Assets;
using Library.Content;
using Library.Domain;
using Library.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Library.GameState.Base.TransitionState
{
    public class TransitionStateManager
    {
        public static LocationName TransitionLocationName { get; private set; }
        public static Point TransitionLocationPoint { get; private set; }
        public static int Counter { get; set; }

        private static Rectangle DestinationRectangle = new Rectangle(0, 0, Constants.ResolutionWidth, Constants.ResolutionHeight);
        private static Rectangle SourceRectangle = new Rectangle(0, 0, 1, 1);
        private static Texture2D BlankTexture { get; set; }
        

        public static void StartTransition(LocationName locationName, Point locationCoordinates) {
            TransitionLocationName = locationName;
            TransitionLocationPoint = locationCoordinates;
            Counter = 0;

            BaseStateManager.Instance.BaseState = BaseState.Transition;

            if (BlankTexture == null) {
                BlankTexture = CreateTexture(GraphicsManager.GraphicsDevice);
            }
        }

        public static void Update()
        {
            if (Counter == Constants.TransitionTime / 2)
            {
                Player player = GameStateManager.Instance.GetPlayer();
                CharacterState playerState = player.CharacterState;
                BaseStateManager.Instance.LocationStates[playerState.CurrentLocation].Characters.Remove(player);

                playerState.CurrentLocation = TransitionLocationName;
                playerState.Position = new Vector(TransitionLocationPoint) * Constants.ScaledTileSize;

                BaseStateManager.Instance.LocationStates[TransitionLocationName].Characters.Add(player);
            }
            else if (Counter >= Constants.TransitionTime)
            {
                BaseStateManager.Instance.BaseState = BaseState.Base;
            }

            Counter++;
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            float scaler;
            if (Counter < Constants.TransitionTime / 2)
            {
                Debug.WriteLine("1" + 1f / Constants.TransitionTime);
                Debug.WriteLine("2" + (1f / Constants.TransitionTime) * 2);
                Debug.WriteLine("3" + ((1f / Constants.TransitionTime) * 2) * Counter);

                scaler = ((1f / Constants.TransitionTime) / 2) * Counter;
            }
            else if (Counter == Constants.TransitionTime / 2)
            {
                scaler = 1f;
            }
            else {
                scaler = ((1f / Constants.TransitionTime) * 2) * (Constants.TransitionTime - Counter);
            }


            spriteBatch.Begin();
            spriteBatch.Draw(BlankTexture, DestinationRectangle, SourceRectangle, Color.Black * scaler);
            spriteBatch.End();
        }

        private static Texture2D CreateTexture(GraphicsDevice device)
        {
            //initialize a texture
            Texture2D texture = new Texture2D(device, 1, 1);

            //the array holds the color for each pixel in the texture
            Color[] data = new Color[1];
            for (int pixel = 0; pixel < data.Count(); pixel++)
            {
                //the function applies the color according to the specified pixel
                data[pixel] = Color.White;
            }

            //set the color
            texture.SetData(data);

            return texture;
        }
    }
}
