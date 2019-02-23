using Galaxies.Core;
using Galaxies.UI.Interfaces;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Elements
{

    class UIButton : UIElement, ITextElement<UIButton>
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

        public UIButton(Transform transform, SpriteFont spriteFont, string text, TextAlign textAlign, int textPadding, Texture2D backgroundSprite, EventArg onClick, Screen screen, bool canBeClicked = true)
            : base(transform, backgroundSprite, onClick, screen, canBeClicked)
        {
            this.spriteFont  = spriteFont;
            this.textAlign   = textAlign;
            this.Text        = text;
            this.TextPadding = textPadding;
        }

        private void CalculateTextPosition()
        {
            FormattedText = TextHelper.WrapText(this, transform.Width);

            textPosition = TextHelper.Align(this);
        }

        public override void PositionChanged()
        {
            base.PositionChanged();

            CalculateTextPosition();
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
                    spriteBatch.DrawString(spriteFont, FormattedText, transform.Position + textPosition, Color, 0, FormattedTextOrigin, 1, SpriteEffects.None, 0);
                }
            }
        }

    }

}
