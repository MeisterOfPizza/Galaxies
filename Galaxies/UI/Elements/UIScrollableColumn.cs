using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.UI.Interfaces;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace Galaxies.UI.Elements
{

    class UIScrollableColumn : UIContainer, IScrollable
    {

        /// <summary>
        /// By how much should the each "scroll" event change the <see cref="currentScrollValue"/>.
        /// </summary>
        private const float SCROLL_DELTA_VALUE = 0.1f;

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
                currentScrollValue = MathHelper.Clamp(value, 0f, 1f);

                CalculatePositions();
            }
        }

        #endregion

        public UIScrollableColumn(Transform transform, Texture2D sprite, Screen screen, Vector4 padding, Vector2 spacing)
            : base(transform, sprite, screen, padding, spacing, true)
        {
            screen.kb_selectCallbacks.AddEvent(new _EventArg1<UIElement>(SelectedChanged));

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
                float scrollOffsetY = (TotalItemsHeight() - (RawSize.Y - Spacing.Y * (Container.Count - 1))) * currentScrollValue;
                scrollOffsetY = MathHelper.Clamp(scrollOffsetY, 0, scrollOffsetY);

                float currentY = Container[0].Transform.Height / 2f;
                float startY   = transform.Y - transform.Height / 2f + Padding.X;

                for (int i = 0; i < Container.Count; i++)
                {
                    Container[i].Transform.Position = new Vector2(transform.X, startY + currentY - scrollOffsetY);

                    currentY += Container[i].Transform.Height / 2f + Spacing.Y;

                    //Add the height of the previous item:
                    if (i < Container.Count - 1)
                    {
                        currentY += Container[i + 1].Transform.Height / 2f;
                    }
                }
            }
        }
        
        protected override void UIElementAdded(UIElement addedElement)
        {
            base.UIElementAdded(addedElement);
            
            CalculatePositions();

            scrollbar.IScrollableChanged();
        }

        protected override void UIElementRemoved(UIElement removedElement, int removedIndex)
        {
            base.UIElementRemoved(removedElement, removedIndex);

            if (Container.Count > 0)
            {
                CalculateSize();
                CalculatePositions();
            }

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
            float scrollSpeedMultiplier = transform.Height / TotalItemsHeight();
            currentScrollValue = MathHelper.Clamp(currentScrollValue + -value * SCROLL_DELTA_VALUE * scrollSpeedMultiplier, 0f, 1f);

            scrollbar.ScrollbarValue = currentScrollValue;

            CalculatePositions();
        }

        public float DeltaViewSize()
        {
            return RawSize.Y / TotalItemsHeight();
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Returns the total height of all items.
        /// </summary>
        private float TotalItemsHeight()
        {
            return Container.Sum(c => c.Transform.Height);
        }

        /// <summary>
        /// Callback method whenever the player selects a new Interactable from the current screen.
        /// </summary>
        /// <param name="newSelected">Newly selected element.</param>
        protected void SelectedChanged(UIElement newSelected)
        {
            int index = Container.IndexOf(newSelected);

            if (index != -1)
            {
                ScrollValue = (index + 1) / (float)Container.Count;
            }
        }

        #endregion

    }

}
