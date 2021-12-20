using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Library.Controls
{
    public class ControlBinding
    {
        public Keys Key { get; set; }
        public Func<ButtonState> GetButtonState { get; set; }
    }
}
