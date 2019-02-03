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
        int        textPadding;

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

        public string FormattedText { get; set; }
        public int    TextPadding   { get; set; }

        #endregion

        public UIButton(SpriteFont spriteFont, string text, TextAlign textAlign, int textPadding, Texture2D backgroundSprite, Vector2 position, float rotation, Color color, OnClickEvent onClick, Screen screen, bool canBeClicked = true) : base(backgroundSprite, position, rotation, color, onClick, screen, canBeClicked)
        {
            this.spriteFont  = spriteFont;
            this.textAlign   = textAlign;
            this.Text        = text;
            this.textPadding = textPadding;
        }

        private void CalculateTextPosition()
        {
            textPosition = Position + TextHelper.Align(this);

            FormattedText = TextHelper.WrapText(this, Width);
        }

        protected override void PositionChanged()
        {
            base.PositionChanged();

            CalculateTextPosition();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visable)
            {
                if (sprite != null)
                {
                    base.Draw(spriteBatch);
                }

                spriteBatch.DrawString(spriteFont, FormattedText, textPosition, Color);
            }
        }

    }

}
