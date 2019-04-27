using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.UI.Interfaces;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Galaxies.UI.Elements
{

    sealed class UIScrollableGrid : UIContainer, IScrollable
    {

        /// <summary>
        /// By how much should the each "scroll" event change the <see cref="currentScrollValue"/>.
        /// </summary>
        private const float SCROLL_DELTA_VALUE = 0.1f;
        
        /// <summary>
        /// 2D grid where the Y coordinate comes BEFORE the X coordinate.
        /// Therefore, access the grid with [y][x].
        /// </summary>
        private List<List<UIElement>> Grid = new List<List<UIElement>>();

        private float currentScrollValue;

        private UIScrollbar scrollbar;

        public override bool Visable
        {
            get
            {
                return base.Visable;
            }

            set
            {
                base.Visable = value;

                scrollbar.Visable = value;
            }
        }

        #region IScrollable

        public bool IsScrollable { get; set; } = true;

        public float ScrollValue
        {
            get
            {
                return currentScrollValue;
            }

            set
            {
                currentScrollValue = value;

                CalculatePositions();
            }
        }

        #endregion

        public UIScrollableGrid(Transform transform, Texture2D sprite, Screen screen, Vector4 padding, Vector2 spacing)
            : base(transform, sprite, screen, padding, spacing, true)
        {
            Screen.kb_selectCallbacks.AddEvent(new _EventArg1<UIElement>(SelectedChanged));

            scrollbar = screen.AddUIElement(new UIScrollbar(
                new Transform(new Vector2(transform.X + transform.Width / 2f + 7.5f, transform.Y), new Vector2(15, transform.Height)),
                ContentHelper.Box4x4_Sprite,
                ContentHelper.GetSprite("Sprites/UI/handle"),
                null,
                this,
                screen
                ));
        }

        #region Overriden methods

        protected override void CalculatePositions()
        {
            if (Container.Count > 0)
            {
                float scrollOffsetY = (TotalGridItemsHeight() - (RawSize.Y - Spacing.Y * (Grid.Count - 1))) * currentScrollValue;
                scrollOffsetY = MathHelper.Clamp(scrollOffsetY, 0, scrollOffsetY);
                
                float currentY = Grid[0][0].Transform.Height / 2f; //Position of the first element from the top.

                float startX = transform.X - transform.Width / 2f + Padding.W; //Apply left padding.
                float startY = transform.Y - transform.Height / 2f + Padding.X; //Apply top padding.

                for (int y = 0; y < Grid.Count; y++)
                {
                    float tallestItem = float.MaxValue;

                    float currentX = Grid[y][0].Transform.Width / 2f; //Position of the first element from the left.

                    for (int x = 0; x < Grid[y].Count; x++)
                    {
                        Grid[y][x].Transform.Position = new Vector2(startX + currentX, startY + currentY - scrollOffsetY);

                        currentX += Grid[y][x].Transform.Width / 2f + Spacing.X;

                        //Add the width of the previous item:
                        if (x < Grid[y].Count - 1)
                        {
                            currentX += Grid[y][x + 1].Transform.Width / 2f;
                        }

                        //Check if the current item is the tallest item:
                        if (Grid[y][x].Transform.Height < tallestItem)
                        {
                            tallestItem = Grid[y][x].Transform.Height;
                        }
                    }
                    
                    currentY += tallestItem + Spacing.Y;
                }
            }
        }

        protected override void UIElementAdded(UIElement addedElement)
        {
            base.UIElementAdded(addedElement);

            ArrangeItems();

            scrollbar.IScrollableChanged();
        }

        protected override void UIElementRemoved(UIElement removedElement, int removedIndex)
        {
            base.UIElementRemoved(removedElement, removedIndex);

            ArrangeItems();

            scrollbar.IScrollableChanged();
        }

        public override void PositionChanged()
        {
            base.PositionChanged();

            if (scrollbar != null)
            {
                scrollbar.Transform.Position = new Vector2(transform.Position.X + transform.Width / 2f + scrollbar.Transform.Width / 2f, transform.Y);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Visable)
            {
                spriteBatch.End();
                spriteBatch.GraphicsDevice.ScissorRectangle = new Rectangle((transform.Position - transform.Size / 2f).ToPoint(), transform.Size.ToPoint());
                spriteBatch.Begin(samplerState: SamplerState.PointClamp, rasterizerState: new RasterizerState() { ScissorTestEnable = true });

                if (Sprite != null)
                {
                    spriteBatch.Draw(Sprite, new Rectangle(transform.RawX, transform.RawY, transform.RawWidth, transform.RawHeight), null, Color, transform.Rotation, Origin, SpriteEffects.None, 0f);
                }

                if (Container.Count > 0)
                {
                    foreach (var item in Container)
                    {
                        if (item is IMaskable maskable)
                        {
                            maskable.CheckMask(spriteBatch.GraphicsDevice.ScissorRectangle);
                        }

                        item.Draw(spriteBatch);
                    }
                }

                spriteBatch.End();
                spriteBatch.Begin(samplerState: SamplerState.PointClamp, blendState: BlendState.AlphaBlend);
            }
        }

        #endregion

        #region IScrollable

        public void MouseScroll(int value)
        {
            float scrollSpeedMultiplier = transform.Height / TotalGridItemsHeight();
            currentScrollValue = MathHelper.Clamp(currentScrollValue + -value * SCROLL_DELTA_VALUE * scrollSpeedMultiplier, 0f, 1f);

            scrollbar.ScrollbarValue = currentScrollValue;

            CalculatePositions();
        }

        public float DeltaViewSize()
        {
            return RawSize.Y / TotalGridItemsHeight();
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Arranges the items in the grid, limiting the total width of each row to the width of the grid.
        /// </summary>
        private void ArrangeItems()
        {
            Queue<UIElement>      unarrangedItems = new Queue<UIElement>(Container);
            List<List<UIElement>> arrangedItems = new List<List<UIElement>>();

            arrangedItems.Add(new List<UIElement>(3));

            int   currentRow   = 0; //Current row.
            float currentWidth = Padding.X; //Add the left padding.
            float maxWidth     = transform.Width - Padding.Y; //Remove the right padding.

            while (unarrangedItems.Count > 0)
            {
                UIElement item = unarrangedItems.Dequeue();

                if (currentWidth + item.Transform.Width > maxWidth)
                {
                    currentRow++;
                    currentWidth = Padding.X;
                    arrangedItems.Add(new List<UIElement>(3));
                }

                currentWidth += item.Transform.Width + (currentWidth > Padding.X ? Spacing.X : 0); //Spacing from the last item on the same row.

                arrangedItems[currentRow].Add(item);
            }

            Grid = arrangedItems;

            CalculatePositions();
        }

        /// <summary>
        /// Returns the total grid items height, with each row adding the tallest item to the total height.
        /// </summary>
        private float TotalGridItemsHeight()
        {
            float totalHeight = 0;

            for (int y = 0; y < Grid.Count; y++)
            {
                if (Grid[y].Count > 0)
                {
                    //Get the largest height (tallest item).
                    totalHeight += Grid[y].Max(g => g.Transform.Height);
                }
            }

            return totalHeight;
        }

        /// <summary>
        /// Callback method whenever the player selects a new UI Element from the current screen.
        /// </summary>
        /// <param name="newSelected">Newly selected element.</param>
        private void SelectedChanged(UIElement newSelected)
        {
            int index = Container.IndexOf(newSelected);

            if (index != -1)
            {
                //Find the item in the grid:
                for (int y = 0; y < Grid.Count; y++)
                {
                    //Get the index of the index in the current row (y):
                    index = Grid[y].IndexOf(newSelected);

                    if (index != 0)
                    {
                        //Divide the row with the total amount of rows.
                        ScrollValue = y / (float)Grid.Count;

                        CalculatePositions();
                    }
                }
            }
        }

        #endregion

    }

}
