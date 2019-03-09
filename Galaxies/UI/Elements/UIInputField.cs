using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        /// Cooldown between keystrokes.
        /// </summary>
        private const double INPUT_COOLDOWN = 0.25;

        double timeSinceLastInput;

        UIText inputFieldText;
        string placeholderText;

        #region IInteractable

        public bool     IsInteractable { get; set; } = true;
        public bool     IsSelected     { get; set; }
        public EventArg OnClick        { get; set; }
        public Color    DefaultColor   { get; set; }

        #endregion

        public UIInputField(Transform transform, SpriteFont spriteFont, string placeholderText, TextAlign textAlign, int textPadding, Texture2D backgroundSprite, Screen screen) : base(transform, backgroundSprite, screen)
        {
            this.placeholderText = placeholderText;
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

        #region Input Field

        private void InputChanged(TextInputEventArgs args)
        {
            inputFieldText.Text = TextInputController.NewInput(args, inputFieldText.Text);

            if (string.IsNullOrWhiteSpace(inputFieldText.Text))
            {
                inputFieldText.Text = placeholderText;
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
            //Do nothing here
        }

        public void Select()
        {
            SetColor(Color.LightGray);

            IsSelected = true;

            TextInputController.SetTextInputListener((EventArg1<TextInputEventArgs>)OnClick);
        }

        public void Deselect()
        {
            SetColor(DefaultColor);

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
