using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.UI.Interfaces;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Elements
{

    class UIInputField : UIGroup, IInteractable
    {

        /// <summary>
        /// The text that the user has entered.
        /// </summary>
        public string Text { get; private set; } = "";

        UIText inputFieldText;
        string placeholderText;
        int    maxLength;

        #region IInteractable

        public bool     IsInteractable { get; set; } = true;
        public bool     IsSelected     { get; set; }
        public Color    DefaultColor   { get; set; }

        /// <summary>
        /// This is repurposed to be used as an event caller for whenever a new text input event is called.
        /// </summary>
        public EventArg OnClick { get; set; }

        #endregion

        public UIInputField(Transform transform, SpriteFont spriteFont, string placeholderText, int maxLength, TextAlign textAlign, int textPadding, Texture2D backgroundSprite, Screen screen) : base(transform, backgroundSprite, screen)
        {
            this.placeholderText = placeholderText;
            this.maxLength       = maxLength;
            this.DefaultColor    = Color;

            this.OnClick = new EventArg1<TextInputEventArgs>(InputChanged, null); //Setup for input changes.

            inputFieldText = AddUIElement(new UIText(
                new Transform(transform.Size),
                spriteFont,
                placeholderText,
                textAlign,
                textPadding,
                screen
                ));
        }

        public override void OnDestroy()
        {
            base.OnDestroy();

            if (IsSelected)
            {
                TextInputController.RemoveTextInputListener();
            }
        }

        public override void SetColor(Color color)
        {
            base.SetColor(color);

            DefaultColor = color;
        }

        #region Input Field

        private void InputChanged(TextInputEventArgs args)
        {
            Text = TextInputController.NewInput(args, Text);

            if (Text.Length > maxLength)
            {
                //Limit the length of the input text:
                Text = Text.Substring(0, maxLength);
            }

            inputFieldText.Text = string.IsNullOrWhiteSpace(Text) ? placeholderText : Text;
        }

        public void SetText(string text)
        {
            this.Text = text;

            //If the given text is empty (or null), then set the new text as the placeholder text.
            if (string.IsNullOrEmpty(text))
            {
                inputFieldText.Text = placeholderText;
            }
            else
            {
                inputFieldText.Text = text;
            }
        }

        #endregion

        #region IInteractable

        public void SetOnClick(EventArg @event)
        {
            //Do nothing here
        }

        public void Click()
        {
            TextInputController.SetTextInputListener((EventArg1<TextInputEventArgs>)OnClick);
        }

        public void Select()
        {
            color = new Color(DefaultColor * 0.9f, 1f);

            IsSelected = true;
        }

        public void Deselect()
        {
            color = DefaultColor;

            if (IsSelected)
            {
                TextInputController.RemoveTextInputListener();
            }

            IsSelected = false;
        }

        public void MouseEnter()
        {
            Select();
        }

        public void MouseExit()
        {
            Deselect();
        }

        #endregion

    }

}
