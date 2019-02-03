using Galaxies.UI.Interfaces;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Elements
{

    class UIText : UIElement, ITextElement<UIText>
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

        public string FormattedText { get; set; }
        public int    TextPadding   { get; set; }

        #endregion

        public UIText(SpriteFont spriteFont, string text, TextAlign textAlign, int textPadding, Vector2 position, float rotation, Color color, Screen screen) : base(null, position, rotation, color, null, screen, false)
        {
            this.spriteFont  = spriteFont;
            this.textAlign   = textAlign;
            this.Text        = text;
            this.TextPadding = textPadding;
        }

        private void CalculateTextPosition()
        {
            textPosition = Position + TextHelper.Align(this);
            
            FormattedText = TextHelper.WrapText(this, Width);

            SetDrawHeight((int)spriteFont.MeasureString(FormattedText).Y);
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
                if (spriteFont != null)
                {
                    spriteBatch.DrawString(spriteFont, FormattedText, textPosition, Color);
                }
            }
        }

    }

}
