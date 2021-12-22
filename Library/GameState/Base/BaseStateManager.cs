using System;
using System.Collections.Generic;
using System.Linq;
using Library.Base;
using Library.Content;
using Library.Domain;
using Library.GameState.Base.CutsceneState;
using Library.GameState.Base.GamePadHandling;
using Library.GameState.Base.MessageState;
using Library.GameState.Base.TransitionState;
using Library.World;
using Microsoft.Xna.Framework.Graphics;

namespace Library.GameState.Base
{
    public class BaseStateManager : IStateManager
    {
        public static BaseStateManager Instance { get; private set; }
        public Stack<BaseState> StateStack { get; set; }
        public Dictionary<LocationName, LocationState> LocationStates { get; private set; }
        public FlagManager FlagManager { get; set; }


        public BaseStateManager()
        {
            Instance = this;
            LocationStates = new Dictionary<LocationName, LocationState>();
            StateStack = new Stack<BaseState>();
            StateStack.Push(BaseState.Base);

            foreach (LocationName locationName in Enum.GetValues(typeof(LocationName)))
            {
                LocationStates.Add(locationName, new LocationState
                {
                    CapturedPokemon = LocationManager.LocationLayouts[locationName].InitialCapturedPokemon,
                    Items = LocationManager.LocationLayouts[locationName].InitialItems,
                    Characters = LocationManager.LocationLayouts[locationName].InitialCharacters,
                    Name = locationName
                });
            }

            FlagManager = new FlagManager();
        }

        public bool Update()
        {
            if (StateStack.Peek() == BaseState.Base)
            {
                BaseGamePadHandler.Update();
            }
            else if (StateStack.Peek() == BaseState.Message)
            {
                MessageStateGamePadHandler.Update();
            }
            else if (StateStack.Peek() == BaseState.Transition)
            {
                TransitionStateManager.Update();
            }
            else if (StateStack.Peek() == BaseState.Cutscene)
            {
                CutsceneStateManager.Update();
            }

            if (StateStack.Peek() == BaseState.Message)
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

            StitchHelperObject stitchHelperObject = WorldManager.GetClosestStitchHelperObject();
            if (stitchHelperObject != null)
            {
                WorldManager.DrawStitch(spriteBatch, stitchHelperObject.LocationStitch);
            }

            LocationStates[currentLocation].Draw(spriteBatch);

            LocationManager.LocationLayouts[currentLocation].DrawForeground(spriteBatch);

            if (StateStack.Peek() == BaseState.Message)
            {
                MessageStateDrawingManager.Draw(spriteBatch);
            }
            else if (StateStack.Peek() == BaseState.Transition)
            {
                TransitionStateManager.Draw(spriteBatch);
            }
        }

        public LocationName GetPlayerLocation()
        {
            return LocationStates.Values.Single(state => state.Characters.Exists(character => character.Name == CharacterName.Ash)).Name;
        }
    }
}
