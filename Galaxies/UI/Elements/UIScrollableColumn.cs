using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Elements
{

    class UIScrollableColumn : UIColumn
    {
        
        /// <summary>
        /// How many items can fit per view?
        /// </summary>
        private int maxFitPerView;

        /// <summary>
        /// How high is each item?
        /// </summary>
        private int itemHeight;

        /// <summary>
        /// What index is the one at the top?
        /// </summary>
        private int minIndex;

        /// <summary>
        /// What index is the one at the bottom?
        /// </summary>
        private int maxIndex;

        public UIScrollableColumn(Texture2D sprite, Vector2 position, float rotation, Color color, Vector2 size, Screen screen, int spaceY, int borderY, int itemHeight) : base(sprite, position, rotation, color, size, screen, spaceY, borderY, true)
        {
            this.itemHeight = itemHeight;

            screen.SelectCallbacks += SelectedChanged;
        }

        protected override void CalculatePositions()
        {
            int currentY = BorderY - Height / 2;
            
            for (int i = minIndex; i <= maxIndex; i++)
            {
                Container[i].Position = Position + new Vector2(0, 0);

                currentY += itemHeight + SpaceY;
            }

            ResponsiveMaxY = currentY + BorderY; //Not used by this class, but may be used later down the road?
        }

        protected override void CalculateSize()
        {
            CalculateFitPerView();
        }

        /// <summary>
        /// Callback method whenever the player selects a new UI Element from the current screen.
        /// </summary>
        /// <param name="newSelected">Newly selected element.</param>
        protected void SelectedChanged(UIElement newSelected)
        {
            int index = Container.IndexOf(newSelected);

            if (index != -1)
            {
                CalculateIndexRange(index);

                CalculatePositions();
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visable && sprite != null)
            {
                spriteBatch.Draw(sprite, new Rectangle((int)Position.X, (int)Position.Y, Width, Height), null, Color, Rotation, Vector2.Zero, SpriteEffects.None, 0f);
            }
            
            if (Container.Count > 0)
            {
                for (int i = minIndex; i <= maxIndex; i++)
                {
                    Container[i].Draw(spriteBatch);
                }
            }
        }

        #region Helpers

        /// <summary>
        /// How many elements can fit per view?
        /// </summary>
        private void CalculateFitPerView()
        {
            maxFitPerView = 0;

            int totHeight = 0;

            while (totHeight <= Height - itemHeight)
            {
                totHeight += itemHeight;
                maxFitPerView++;
            }

            maxFitPerView--; //Account for index value.
        }

        /// <summary>
        /// Calculates the min and max index allowed.
        /// </summary>
        private void CalculateIndexRange(int selectedIndex)
        {
            if (selectedIndex <= minIndex)
            {
                minIndex = MathHelper.Clamp(selectedIndex, 0, Container.Count - 1);

                maxIndex = MathHelper.Clamp(minIndex + maxFitPerView, 0, Container.Count - 1);
            }
            else if (selectedIndex >= maxIndex)
            {
                maxIndex = MathHelper.Clamp(selectedIndex, 0, Container.Count - 1);

                minIndex = MathHelper.Clamp(maxIndex - maxFitPerView, 0, Container.Count - 1);
            }
        }

        #endregion

    }

}
