﻿using Galaxies.Core;
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

        public UIScrollableColumn(Texture2D sprite, Vector2 position, float rotation, Color color, Vector2 size, Screen screen, Vector4 padding, Vector2 spacing, int itemHeight)
            : base(sprite, position, rotation, color, size, screen, padding, spacing, true)
        {
            this.itemHeight = itemHeight;

            screen.SelectCallbacks.AddEvent(new _EventArg1<UIElement>(SelectedChanged));
        }

        protected override void CalculatePositions()
        {
            int currentY = (int)Padding.X;
            float startY = Height / 2f - itemHeight / 2f;

            for (int i = minIndex; i <= maxIndex; i++)
            {
                if (i < Container.Count)
                {
                    Container[i].Position = Position - new Vector2(0, startY - currentY);

                    currentY += itemHeight + (int)Spacing.Y;
                }
                else
                {
                    break;
                }
            }

            ResponsiveMaxY = currentY + (int)Padding.Z; //Not used by this class, but may be used later down the road?
        }

        protected override void CalculateSize()
        {
            CalculateFitPerView();

            //Set size with the addition of padding and spacing.
            SetDrawSize(
                (int)(RawSize.X + Padding.W + Padding.Y),
                (int)(RawSize.Y + Padding.X + Padding.Z + maxFitPerView * Spacing.Y)
                );
        }

        protected override void UIElementRemoved(UIElement removedElement, int removedIndex)
        {
            base.UIElementRemoved(removedElement, removedIndex);

            if (Container.Count > 0)
            {
                CalculateIndexRange(MathHelper.Clamp(removedIndex, 0, Container.Count - 1));

                CalculateSize();
                CalculatePositions();
            }
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
            if (Visable)
            {
                if (Sprite != null)
                {
                    spriteBatch.Draw(Sprite, new Rectangle((int)Position.X, (int)Position.Y, Width, Height), null, Color, Rotation, Origin, SpriteEffects.None, 0f);
                }

                if (Container.Count > 0)
                {
                    for (int i = minIndex; i <= maxIndex; i++)
                    {
                        if (i < Container.Count)
                        {
                            Container[i].Draw(spriteBatch);
                        }
                    }
                }
            }
        }

        #region Helpers

        /// <summary>
        /// How many elements can fit per view?
        /// </summary>
        private void CalculateFitPerView()
        {
            maxFitPerView = (int)(RawSize.Y / itemHeight);
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
