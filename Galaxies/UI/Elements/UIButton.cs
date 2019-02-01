using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Elements
{

    class UIButton : UIElement
    {

        #region Fields

        SpriteFont spriteFont;
        string     text;
        Vector2    center;

        #endregion

        #region Properties

        public string Text
        {
            set
            {
                text = value;

                CalculateCenter();
            }
        }

        #endregion

        public UIButton(SpriteFont spriteFont, string text, Texture2D backgroundSprite, Vector2 position, float rotation, Color color, OnClickEvent onClick, Screen screen, bool canBeClicked = true) : base(backgroundSprite, position, rotation, color, onClick, screen, canBeClicked)
        {
            this.spriteFont  = spriteFont;
            this.text        = text;

            CalculateCenter();
        }

        private void CalculateCenter()
        {
            var size = spriteFont.MeasureString(text);

            center = Position + new Vector2(Width / 2f - size.X / 2f, Height / 2f - size.Y / 2f);
        }

        protected override void PositionChanged()
        {
            base.PositionChanged();

            CalculateCenter();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (sprite != null)
            {
                base.Draw(spriteBatch);
            }

            spriteBatch.DrawString(spriteFont, text, center, Color);
        }

    }

}
