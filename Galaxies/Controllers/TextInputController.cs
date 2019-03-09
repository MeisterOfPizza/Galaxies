using Galaxies.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Galaxies.Extensions
{

    static class TextInputController
    {

        public static EventHandler<TextInputEventArgs> TextInputListener { get; private set; }
        public static EventArg1<TextInputEventArgs>    OnInputEvent      { get; private set; }

        public static string NewInput(TextInputEventArgs textInput, string text)
        {
            KeyboardState kbState = Keyboard.GetState();

            if (kbState.IsKeyDown(Keys.Back))
            {
                text = text.Remove(text.Length - 1);
            }
            else
            {
                text += textInput.Character;
            }

            return text;
        }

        public static void SetTextInputListener(EventArg1<TextInputEventArgs> onInputEvent)
        {
            OnInputEvent = onInputEvent;

            MainGame.Singleton.Window.TextInput -= TextInputListener; //Remove old listnener
            TextInputListener = new EventHandler<TextInputEventArgs>(InputEvent);
            MainGame.Singleton.Window.TextInput += TextInputListener; //Add new listnener
        }

        public static void RemoveTextInputListener()
        {
            OnInputEvent = null;

            MainGame.Singleton.Window.TextInput -= TextInputListener; //Remove listnener
        }

        private static void InputEvent(object obj, TextInputEventArgs args)
        {
            if (OnInputEvent != null)
            {
                OnInputEvent.SetArguments(args);

                OnInputEvent.Invoke();
            }
        }

    }

}
