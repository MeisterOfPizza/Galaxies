using Galaxies.Core;
using Galaxies.UI.Extensions;
using Galaxies.UI.Interfaces;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Elements
{

    class UIButton : UIElement, ITextElement<UIButton>, IInteractable
    {

        #region Fields

        SpriteFont spriteFont;
        TextAlign  textAlign;
        string     text;
        Vector2    textPosition;

        #endregion

        #region Properties

        public SpriteFont SpriteFont
        {
            get
            {
                return spriteFont;
            }
        }

        public TextAlign TextAlign
        {
            get
            {
                return textAlign;
            }
        }

        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;

                CalculateTextPosition();
            }
        }

        public string  FormattedText       { get; set; }
        public int     TextPadding         { get; set; }
        public Vector2 FormattedTextOrigin { get; set; }

        #endregion

        #region IInteractable

        public bool IsInteractable { get; set; } = true;
        public bool IsSelected     { get; set; }

        public EventArg OnClick { get; set; }

        public Color DefaultColor { get; set; }

        #endregion

        public UIButton(Transform transform, SpriteFont spriteFont, string text, TextAlign textAlign, int textPadding, Texture2D backgroundSprite, EventArg onClick, Screen screen)
            : base(transform, backgroundSprite, screen)
        {
            this.spriteFont  = spriteFont;
            this.textAlign   = textAlign;
            this.Text        = text;
            this.TextPadding = textPadding;

            this.OnClick      = onClick;
            this.DefaultColor = Color;
        }

        private void CalculateTextPosition()
        {
            FormattedText = TextHelper.WrapText(this, transform.Width);

            textPosition = TextHelper.Align(this);
        }

        #region Overriden methods

        public override void PositionChanged()
        {
            base.PositionChanged();

            CalculateTextPosition();
        }

        public override void SetColor(Color color)
        {
            base.SetColor(color);

            DefaultColor = color;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visable)
            {
                if (Sprite != null)
                {
                    base.Draw(spriteBatch);
                }

                if (spriteFont != null)
                {
                    spriteBatch.DrawString(spriteFont, FormattedText, transform.Position + textPosition, Color.White, 0, FormattedTextOrigin, 1, SpriteEffects.None, 0);
                }
            }
        }

        #endregion

        public void SetOnClick(EventArg @event)
        {
            this.OnClick = @event;
        }

        public void Click()
        {
            if (OnClick != null)
            {
                OnClick.Invoke();
            }
        }

        public void Select()
        {
            color = new Color(DefaultColor * 0.9f, 1f);

            IsSelected = true;
        }

        public void Deselect()
        {
            color = DefaultColor;

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
    }

}
