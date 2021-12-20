using Library.Domain;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Library.Controls
{
    public static class ControlsManager
    {
        private static Dictionary<Control, ControlBinding> Controls { get; set; }
        private static Dictionary<Control, bool> ControlDebounced { get; set; }

        private static int HoldDebounceTimer { get; set; }

        private static GamePadState GamePadState => GamePad.GetState(PlayerIndex.One);

        public static bool ControlPressed(Control control)
        {
            if (ControlDebounced[control])
            {
                return false;
            }

            ControlBinding binding = Controls[control];

            bool controlPressed = Keyboard.GetState().IsKeyDown(binding.Key) || binding.GetButtonState() == ButtonState.Pressed;

            if (controlPressed)
            {
                ControlDebounced[control] = true;
            }

            return controlPressed;
        }

        public static bool ControlHeld(Control control)
        {
            if (HoldDebounceTimer != 0)
            {
                return false;
            }

            ControlBinding binding = Controls[control];

            bool controlPressed = Keyboard.GetState().IsKeyDown(binding.Key) || binding.GetButtonState() == ButtonState.Pressed;

            if (controlPressed)
            {
                HoldDebounceTimer = Constants.StandardDebounce;
            }

            return controlPressed;
        }

        public static void Update()
        {
            if (HoldDebounceTimer != 0)
            {
                HoldDebounceTimer--;
            }

            foreach (Control control in Controls.Keys)
            {
                if (ControlDebounced[control])
                {
                    ControlBinding binding = Controls[control];
                    if (!Keyboard.GetState().IsKeyDown(binding.Key) && binding.GetButtonState() != ButtonState.Pressed)
                    {
                        ControlDebounced[control] = false;
                    }
                }
            }
        }

        public static void Initialize()
        {
            Controls = new Dictionary<Control, ControlBinding>();
            ControlDebounced = new Dictionary<Control, bool>();

            Controls.Add(Control.A, new ControlBinding
            {
                Key = Keys.Space,
                GetButtonState = () => GamePadState.Buttons.A
            });

            Controls.Add(Control.B, new ControlBinding
            {
                Key = Keys.RightShift,
                GetButtonState = () => GamePadState.Buttons.B
            });


            Controls.Add(Control.Select, new ControlBinding
            {
                Key = Keys.LeftShift,
                GetButtonState = () => GamePadState.Buttons.Back
            });

            Controls.Add(Control.Start, new ControlBinding
            {
                Key = Keys.Escape,
                GetButtonState = () => GamePadState.Buttons.Start
            });

            Controls.Add(Control.Left, new ControlBinding
            {
                Key = Keys.Left,
                GetButtonState = () => GamePadState.DPad.Left
            });

            Controls.Add(Control.Right, new ControlBinding
            {
                Key = Keys.Right,
                GetButtonState = () => GamePadState.DPad.Right
            });

            Controls.Add(Control.Up, new ControlBinding
            {
                Key = Keys.Up,
                GetButtonState = () => GamePadState.DPad.Up
            });

            Controls.Add(Control.Down, new ControlBinding
            {
                Key = Keys.Down,
                GetButtonState = () => GamePadState.DPad.Down
            });

            foreach (Control control in Controls.Keys)
            {
                ControlDebounced.Add(control, false);   
            }
        }
    }
}
