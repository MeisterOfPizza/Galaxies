using Galaxies.Core;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Elements
{

    /// <summary>
    /// Progress bar from left to right.
    /// </summary>
    class UIProgressBar : UIElement
    {

        #region Fields

        protected Texture2D bar;

        Vector2 barPosition;
        Vector2 barPadding;
        int     barWidth;
        float   progress;

        #endregion

        #region Properties

        public float Progress
        {
            get
            {
                return progress;
            }

            set
            {
                progress = MathHelper.Clamp(value, 0f, 1f);

                CalculateBarWidth();
            }
        }

        public Color BarColor { get; set; }

        #endregion

        public UIProgressBar(Transform transform, Texture2D background, Color backgroundColor, Texture2D bar, Color barColor, Vector2 barPadding, float progress, Screen screen) : base(transform, background, screen)
        {
            this.SetColor(backgroundColor);

            this.bar        = bar;
            this.barPadding = barPadding * 2; //To account for both sides (top <=> bottom and left <=> right).
            this.BarColor   = barColor;

            this.Progress = progress;
        }

        /// <summary>
        /// Calculate the bar's (progress bar) width based on <see cref="Progress"/>.
        /// </summary>
        private void CalculateBarWidth()
        {
            barWidth = (int)((transform.Width - barPadding.X) * progress); //Apply padding on both sides, then multiply by progress.

            barPosition = transform.Position;
            barPosition.X -= transform.Width / 2 - barPadding.X / 2f; //Go to left side, but move right with a value of barPadding.X / 2 (because it is already accounted for both sides from before).
            barPosition.X += barWidth / 2f; //Go right half of the bar width.
        }

        public override void SizeChanged()
        {
            base.SizeChanged();

            CalculateBarWidth();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visable)
            {
                if (Sprite != null)
                {
                    spriteBatch.Draw(Sprite, new Rectangle(transform.RawX, transform.RawY, transform.RawWidth, transform.RawHeight), null, Color, transform.Rotation, Origin, SpriteEffects.None, 0f);
                }

                if (bar != null)
                {
                    spriteBatch.Draw(bar, new Rectangle((int)barPosition.X, (int)barPosition.Y, barWidth, (int)(transform.RawHeight - barPadding.Y)), null, BarColor, transform.Rotation, Origin, SpriteEffects.None, 0f);
                }
            }
        }

    }

}
