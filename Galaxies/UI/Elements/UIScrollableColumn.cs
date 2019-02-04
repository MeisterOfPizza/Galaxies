using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Elements
{

    class UIScrollableColumn : UIColumn
    {

        private int scrollYOffset;
        private int maxFitPerScroll;
        private int itemHeight;

        /// <summary>
        /// What index is the one at the top?
        /// </summary>
        private int minIndex;

        /// <summary>
        /// What index is the one at the bottom?
        /// </summary>
        private int maxIndex;

        public UIScrollableColumn(Texture2D sprite, Vector2 position, float rotation, Color color, Screen screen, int spaceY, int borderY, int itemHeight) : base(sprite, position, rotation, color, screen, spaceY, borderY, true)
        {
            this.itemHeight = itemHeight;

            screen.SelectCallbacks += SelectedChanged;
        }

        protected override void CalculatePositions()
        {
            int currentY = BorderY;
            
            for (int i = minIndex; i < maxIndex; i++)
            {
                Container[i].Position = Position + new Vector2((Width - Container[i].Width) / 2f, currentY);

                currentY += itemHeight + SpaceY;
            }
        }

        protected override void CalculateSize()
        {
            CalculateResponsiveHeight();
            CalculateFitPerScroll();
        }

        protected void SelectedChanged(UIElement newSelected)
        {
            int index = Container.IndexOf(newSelected);

            if (index != -1)
            {
                CalculateScrollYOffset(index);

                CalculatePositions();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visable && sprite != null)
            {
                spriteBatch.Draw(sprite, new Rectangle((int)Position.X, (int)Position.Y, Width, Height), null, Color, Rotation, Vector2.Zero, SpriteEffects.None, 0f);
            }

            for (int i = minIndex; i < maxIndex; i++)
            {
                Container[i].Draw(spriteBatch);
            }
        }

        #region Helpers

        private void CalculateResponsiveHeight()
        {
            int currentY = BorderY;

            for (int i = 0; i < Container.Count; i++)
            {
                currentY += Container[i].Height + SpaceY;
            }

            ResponsiveMaxY = currentY;
        }

        private void CalculateFitPerScroll()
        {
            maxFitPerScroll = 0;

            int totHeight = 0;

            while (totHeight <= Height - itemHeight)
            {
                totHeight += itemHeight;
                maxFitPerScroll++;
            }
        }

        private void CalculateScrollYOffset(int selectedIndex)
        {
            if (selectedIndex < minIndex)
            {
                minIndex = selectedIndex;

                maxIndex = MathHelper.Clamp(minIndex + maxFitPerScroll, 0, Container.Count - 1);
            }
            else if (selectedIndex > maxIndex)
            {
                maxIndex = selectedIndex;

                minIndex = MathHelper.Clamp(maxIndex - maxFitPerScroll, 0, Container.Count - 1);
            }

            scrollYOffset = 0;

            for (int i = 0; i < maxIndex; i++)
            {
                scrollYOffset += Container[i].Height;
            }
        }

        #endregion

    }

}
