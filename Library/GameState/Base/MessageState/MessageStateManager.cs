using System;
using System.Collections.Generic;
using System.Linq;
using Library.Content;
using Library.Domain;
using Microsoft.Xna.Framework.Graphics;

namespace Library.GameState.Base.MessageState
{
    public class MessageStateManager
    {
        public static List<string> Messages { get; set; }
        private static int RevealedLetters { get; set; }

        public static void Update()
        {
            if (Messages.Count > 0 && RevealedLetters < Messages.First().Length) {
                RevealedLetters++;
            }
        }

        public static void CompleteMessage() {
            if (Messages.Count > 0)
            {
                Messages.RemoveAt(0);
                RevealedLetters = 0;
            }

            if (Messages.Count == 0) {
                RevealedLetters = 0;
                BaseStateManager.Instance.BaseState = BaseState.Base;
            }
        }

        public static string GetMessage() {
            if (Messages.Count > 0) {
                return Messages.First().Substring(0, RevealedLetters);
            }

            return "";
        }
    }
}
