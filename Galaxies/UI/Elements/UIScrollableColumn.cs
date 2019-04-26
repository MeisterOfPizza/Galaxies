using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.UI.Interfaces;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Elements
{

    class UIScrollableColumn : UIColumn, IScrollable
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

        private UIScrollbar scrollbar;

        #region IScrollable

        public bool IsScrollable { get; set; } = true;

        #endregion

        public UIScrollableColumn(Transform transform, Texture2D sprite, Screen screen, Vector4 padding, Vector2 spacing, int itemHeight)
            : base(transform, sprite, screen, padding, spacing, true)
        {
            this.itemHeight = itemHeight;

            screen.kb_selectCallbacks.AddEvent(new _EventArg1<UIElement>(SelectedChanged));

            scrollbar = screen.AddUIElement(new UIScrollbar(
                new Transform(new Vector2(transform.X + transform.Width / 2f, transform.Y), new Vector2(25, transform.Height)),
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
            HideAll();

            int currentY = (int)Padding.X;
            float startY = transform.Height / 2f - itemHeight / 2f;

            /*
            for (int i = minIndex; i <= maxIndex; i++)
            {
                if (i < Container.Count)
                {
                    Container[i].Visable = true;
                    Container[i].Transform.Position = transform.Position - new Vector2(0, startY - currentY);

                    currentY += itemHeight + (int)Spacing.Y;
                }
                else
                {
                    break;
                }
            }
            */
            for (int i = 0; i < Container.Count; i++)
            {
                Container[i].Visable = true;
                Container[i].Transform.Position = transform.Position - new Vector2(0, startY - currentY);

                currentY += itemHeight + (int)Spacing.Y;
            }

            responsiveMaxY = currentY + (int)Padding.Z; //Not used by this class, but may be used later down the road?
        }

        protected override void CalculateSize()
        {
            CalculateFitPerView();

            //Set size with the addition of padding and spacing.
            transform.Size = new Vector2(
                RawSize.X + Padding.W + Padding.Y,
                RawSize.Y + Padding.X + Padding.Z + maxFitPerView * Spacing.Y
                );

            scrollbar.IScrollableChanged();
        }
        
        protected override void UIElementAdded(UIElement addedElement)
        {
            base.UIElementAdded(addedElement);

            FixIndexRange();
            CalculatePositions();

            scrollbar.IScrollableChanged();
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

            scrollbar.IScrollableChanged();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //Effect maskEffect = ContentHelper.AlphaMask_Shader;
            //
            //maskEffect.Parameters["MaskLocation"].SetValue(transform.Position);
            //maskEffect.Parameters["MaskSize"].SetValue(transform.Size);
            //maskEffect.Parameters["MaskTexture"].SetValue(base.Sprite);

            //Viewport defaultViewport = spriteBatch.GraphicsDevice.Viewport;
            spriteBatch.End();

            //RasterizerState rs = new RasterizerState() { ScissorTestEnable = true };

            //spriteBatch.GraphicsDevice.Viewport = new Viewport(new Rectangle(transform.Position.ToPoint(), transform.Size.ToPoint()));
            //spriteBatch.GraphicsDevice.RasterizerState = rs;
            spriteBatch.GraphicsDevice.ScissorRectangle = new Rectangle((transform.Position - transform.Size / 2f).ToPoint(), transform.Size.ToPoint());
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, rasterizerState: new RasterizerState() { ScissorTestEnable = true });
            //spriteBatch.Begin(samplerState: SamplerState.PointClamp);

            if (Visable)
            {
                if (Sprite != null)
                {
                    spriteBatch.Draw(Sprite, new Rectangle(transform.RawX, transform.RawY, transform.RawWidth, transform.RawHeight), null, Color, transform.Rotation, Origin, SpriteEffects.None, 0f);
                }

                if (Container.Count > 0)
                {
                    /*
                    for (int i = minIndex; i <= maxIndex; i++)
                    {
                        if (i < Container.Count)
                        {
                            Container[i].Draw(spriteBatch);
                        }
                    }
                    */
                    foreach (var item in Container)
                    {
                        if (item is IMaskable maskable)
                        {
                            maskable.CheckMask(spriteBatch.GraphicsDevice.ScissorRectangle);
                        }

                        //maskEffect.Parameters["BaseTextureLocation"].SetValue(item.Transform.Position);
                        //maskEffect.Parameters["BaseTextureSize"].SetValue(item.Transform.Size);
                        //maskEffect.Parameters["BaseTexture"].SetValue(item.Sprite);
                        //
                        //ContentHelper.AlphaMask_Shader.CurrentTechnique.Passes[0].Apply();

                        item.Draw(spriteBatch);
                    }
                }
            }

            spriteBatch.End();
            //spriteBatch.GraphicsDevice.Viewport = defaultViewport;
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, blendState: BlendState.AlphaBlend);
        }

        #endregion

        #region IScrollable

        public void MouseScroll(int value)
        {
            if (value < 0) //Scroll down
            {
                minIndex = MathHelper.Clamp(minIndex + 1, 0, Container.Count - 1 - maxFitPerView);
                maxIndex = MathHelper.Clamp(maxIndex + 1, maxFitPerView, Container.Count - 1);

                CalculatePositions();
            }
            else if (value > 0) //Scroll up
            {
                minIndex = MathHelper.Clamp(minIndex - 1, 0, Container.Count - 1 - maxFitPerView);
                maxIndex = MathHelper.Clamp(maxIndex - 1, maxFitPerView, Container.Count - 1);

                CalculatePositions();
            }
        }

        public float DeltaViewSize()
        {
            return maxFitPerView / (float)Container.Count;
        }

        public void SetViewMiddleIndex(float scrollValue)
        {
            int index = (int)(Container.Count * scrollValue);
            int halfMaxFitPerView = maxFitPerView / 2;

            minIndex = MathHelper.Clamp(index - halfMaxFitPerView, 0, Container.Count - 1);

            int extraIndex = index - halfMaxFitPerView < 0 ? maxFitPerView : 0;

            maxIndex = MathHelper.Clamp(index + halfMaxFitPerView + extraIndex, 0, Container.Count - 1);

            CalculatePositions();
        }

        #endregion

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
            //Scroll down
            if (selectedIndex <= minIndex)
            {
                minIndex = MathHelper.Clamp(selectedIndex, 0, Container.Count - 1);

                maxIndex = MathHelper.Clamp(minIndex + maxFitPerView, 0, Container.Count - 1);
            }
            //Scroll up
            else if (selectedIndex >= maxIndex)
            {
                maxIndex = MathHelper.Clamp(selectedIndex, 0, Container.Count - 1);

                minIndex = MathHelper.Clamp(maxIndex - maxFitPerView, 0, Container.Count - 1);
            }
        }

        /// <summary>
        /// Fixes the row index range (<see cref="minIndex"/> and <see cref="maxIndex"/>) if they're acting weird.
        /// </summary>
        private void FixIndexRange()
        {
            if ((maxIndex - maxIndex) * maxFitPerView <= maxIndex * maxFitPerView)
                //Are the amounts of displayed items between minIndex and maxIndex
                //lower or equal to the maximum amount of items that should be displayed?
            {
                minIndex = MathHelper.Clamp(minIndex, 0, Container.Count - 1);

                maxIndex = MathHelper.Clamp(minIndex + maxFitPerView, 0, Container.Count - 1);
            }
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
                CalculateIndexRange(index);

                CalculatePositions();
            }
        }

        #endregion

    }

}
