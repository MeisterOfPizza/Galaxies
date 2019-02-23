using Galaxies.Core;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Galaxies.UI.Elements
{

    class UIScrollableGrid : UIContainer
    {

        /// <summary>
        /// UIElement = Grid item
        /// |
        /// int = Y index of grid item
        /// </summary>
        private Dictionary<UIElement, int> Grid = new Dictionary<UIElement, int>();
        
        private Vector2 itemSize;
        private int     maxFitPerViewX;
        private int     maxFitPerViewY;

        private int minRowIndex;
        private int maxRowIndex;

        private int maxRows;

        public UIScrollableGrid(Texture2D sprite, Vector2 position, float rotation, Color color, Vector2 size, Screen screen, Vector4 padding, Vector2 spacing, Vector2 itemSize)
            : base(sprite, position, rotation, color, size, screen, padding, spacing, true)
        {
            this.itemSize = itemSize;

            Screen.SelectCallbacks.AddEvent(new _EventArg1<UIElement>(SelectedChanged));
        }

        protected override void CalculatePositions()
        {
            int goFrom = MathHelper.Clamp(minRowIndex * maxFitPerViewX, 0, Container.Count);
            int goTo   = MathHelper.Clamp(maxRowIndex * maxFitPerViewX + maxFitPerViewX, 0, Container.Count);

            //int maxIndex = Container.Count - 1;
            int currentX = (int)Padding.W; //Apply left padding.
            int currentY = (int)Padding.X; //Apply top padding.

            float startX = Width / 2f - itemSize.X / 2f; //Position of the first element from the left.
            float startY = Height / 2f - itemSize.Y / 2f; //Position of the first element from the top.

            for (int x = 0; x < maxFitPerViewX; x++)
            {
                for (int y = 0; y < maxFitPerViewY; y++)
                {
                    if (goFrom < goTo)
                    {
                        Container[goFrom++].Position = Position - new Vector2(startX - currentX, startY - currentY);

                        currentX += (int)(itemSize.X + Spacing.X);
                    }
                    else
                    {
                        return;
                    }
                }

                currentX = (int)Padding.W; //Reset X.
                currentY += (int)(itemSize.Y + Spacing.Y);
            }
        }

        protected override void CalculateSize()
        {
            maxFitPerViewX = MathHelper.Clamp(Width / (int)itemSize.X, 1, int.MaxValue);
            maxFitPerViewY = MathHelper.Clamp(Height / (int)itemSize.Y, 1, int.MaxValue);
            
            //Set size with the addition of padding and spacing.
            SetDrawSize(
                (int)(RawSize.X + Padding.W + Padding.Y + (maxFitPerViewX - 1) * Spacing.X), 
                (int)(RawSize.Y + Padding.X + Padding.Z + (maxFitPerViewY - 1) * Spacing.Y)
                );
        }

        protected override void UIElementAdded(UIElement addedElement)
        {
            base.UIElementAdded(addedElement);

            //We need to coordinates of all grid items, and to get that we need to do some quick maths:

            int itemIndex = Container.IndexOf(addedElement) + 1; //The "index" of the newly added item.
            int rowIndex = RowIndexOfItem(itemIndex); //Get the row index by calling the helper method.

            //Now we can add in the UI Element and the y coordinate of the grid item:
            Grid.Add(addedElement, rowIndex);

            maxRows = rowIndex;
        }

        protected override void UIElementRemoved(UIElement removedElement, int removedIndex)
        {
            base.UIElementRemoved(removedElement, removedIndex);

            //Get all elements AFTER the removed element:
            Grid.Remove(removedElement);

            for (int i = removedIndex; i < Container.Count; i++)
            {
                UIElement key = Grid.ElementAt(i).Key;

                //Recalculate the row index of each grid item AFTER the removed element:
                Grid[key] = RowIndexOfItem(i + 1);
            }

            if (Container.Count > 0)
            {
                CalculateIndexRange(Container[MathHelper.Clamp(removedIndex, 0, Container.Count - 1)]);

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
                CalculateIndexRange(newSelected);

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
                    int drawFrom = MathHelper.Clamp(minRowIndex * maxFitPerViewX, 0, Container.Count);
                    int drawTo   = MathHelper.Clamp(maxRowIndex * maxFitPerViewX + maxFitPerViewX, 0, Container.Count);

                    for (int i = drawFrom; i < drawTo; i++)
                    {
                        Container[i].Draw(spriteBatch);
                    }
                }
            }
        }

        #region Helpers

        private void CalculateIndexRange(UIElement element)
        {
            int rowIndex = Grid[element];

            //Account for the fact that we already need one reserved row for the selected element:
            int fitY = maxFitPerViewY - 1;

            //Trying to scroll down
            if (rowIndex <= minRowIndex)
            {
                minRowIndex = MathHelper.Clamp(rowIndex, 0, maxRows);

                maxRowIndex = MathHelper.Clamp(minRowIndex + fitY, 0, maxRows);
            }
            //Trying to scroll up
            else if (rowIndex >= maxRowIndex)
            {
                maxRowIndex = MathHelper.Clamp(rowIndex, 0, maxRows);

                minRowIndex = MathHelper.Clamp(maxRowIndex - fitY, 0, maxRows);
            }
        }

        /// <summary>
        /// Returns the row index of the item (with itemIndex as index).
        /// </summary>
        private int RowIndexOfItem(int itemIndex)
        {
            //Find the x and y grid coordinates:
            int itemX = itemIndex % maxFitPerViewX; //This is not the "real" x-index value (it starts counting from 1).
            if (itemX == 0) itemX = maxFitPerViewX; //It is the last item in the row.
            int itemY = (itemIndex - itemX) / maxFitPerViewX; //This is the "real" y-index value (it starts counting from 0).

            return itemY; //The row index.
        }

        #endregion

    }

}
