using System;
using System.Collections.Generic;
using System.Linq;
using Library.Content;
using Library.Domain;
using Library.GameState.Base.MessageState;
using Library.GameState.Base.TransitionState;
using Library.World;
using Microsoft.Xna.Framework.Graphics;

namespace Library.GameState.Base
{
    public class BaseStateManager : IStateManager
    {
        public static BaseStateManager Instance { get; private set; }
        public BaseState BaseState { get; set; }
        public Dictionary<LocationName, LocationState> LocationStates { get; private set; }

        public BaseStateManager()
        {
            Instance = this;
            LocationStates = new Dictionary<LocationName, LocationState>();
            BaseState = BaseState.Base;

            foreach (LocationName locationName in Enum.GetValues(typeof(LocationName)))
            {
                LocationStates.Add(locationName, new LocationState
                {
                    Characters = LocationManager.LocationLayouts[locationName].InitialCharacters,
                    Name = locationName
                });
            }
        }

        public bool Update()
        {
            if (BaseState == BaseState.Base)
            {
                BaseGamePadHandler.Update();
            }
            else if (BaseState == BaseState.Message)
            {
                MessageStateGamePadHandler.Update();
            }
            else if (BaseState == BaseState.Transition)
            {
                TransitionStateManager.Update();
            }

            if (BaseState == BaseState.Message)
            {
                MessageStateManager.Update();
            }

            LocationStates[GameStateManager.Instance.GetPlayer().CharacterState.CurrentLocation].Update();

            GameStateManager.Instance.CameraLocation = GameStateManager.Instance.GetPlayer().CharacterState.Position + (new Vector(GameStateManager.Instance.GetPlayer().GetTargetRectangle().Size) / 2);

            return true;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            LocationName currentLocation = GameStateManager.Instance.GetPlayer().CharacterState.CurrentLocation;
            LocationManager.LocationLayouts[currentLocation].DrawBackground(spriteBatch);

            List<LocationStitch> stitches = WorldManager.LocationStitches.Where(stitch => stitch.LocationA == currentLocation || stitch.LocationB == currentLocation).ToList();
            stitches.ForEach(stitch => WorldManager.DrawStitch(spriteBatch, stitch));

            LocationStates[currentLocation].Draw(spriteBatch);

            LocationManager.LocationLayouts[currentLocation].DrawForeground(spriteBatch);

            if (BaseState == BaseState.Message)
            {
                MessageStateDrawingManager.Draw(spriteBatch);
            }
            else if (BaseState == BaseState.Transition)
            {
                TransitionStateManager.Draw(spriteBatch);
            }
        }

        public LocationName GetPlayerLocation() { 
             return LocationStates.Values.Single(state => state.Characters.Exists(character => character.Name == Constants.PlayerName)).Name;
        }
    }
}
