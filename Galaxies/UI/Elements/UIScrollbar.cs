﻿using Galaxies.Core;
using Galaxies.UI.Interfaces;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Elements
{

    /// <summary>
    /// Vertical scrollbar for an <see cref="IScrollable"/> element.
    /// </summary>
    sealed class UIScrollbar : UIGroup
    {

        #region Variables

        private IScrollable    scrollable;
        private UIGroupElement handle;

        /// <summary>
        /// How much (in %) can the user see of the scrollable at once?
        /// 1 = entire view, 0 = nothing.
        /// This determines the size of the scrollbar handle.
        /// </summary>
        private float deltaViewSize;

        /// <summary>
        /// X = the Y coordinate of the upper bounds of the scrollbar.
        /// Y = the Y coordinate of the lower bounds of the scrollbar.
        /// </summary>
        private Vector2 containingBounds;

        /// <summary>
        /// Same as <see cref="ScrollbarValue"/>.
        /// </summary>
        private float scrollbarValue;

        private EventArg onValueChanged;

        #endregion

        #region Properties

        /// <summary>
        /// How far down is the scrollbar handle?
        /// 0 = top, 0.5 = middle and 1 = bottom.
        /// </summary>
        public float ScrollbarValue
        {
            get
            {
                return scrollbarValue;
            }

            set
            {
                //Check if we can scroll.
                if (deltaViewSize < 1)
                {
                    float lastValue = this.scrollbarValue;

                    this.scrollbarValue = MathHelper.Clamp(value, 0f, 1f);

                    //Just to avoid overloading with updates.
                    if (!lastValue.Equals(value))
                    {
                        CalculateScrollbarHandlePosition();

                        if (onValueChanged != null)
                        {
                            onValueChanged.Invoke();
                        }

                        //scrollable.SetViewMiddleIndex(scrollbarValue);
                        scrollable.ScrollValue = scrollbarValue;
                    }
                }
                else
                {
                    this.scrollbarValue = 0;
                }
            }
        }

        #endregion

        public UIScrollbar(Transform transform, Texture2D backgroundSprite, Texture2D handleSprite, EventArg onValueChanged, IScrollable scrollable, Screen screen) : base(transform, backgroundSprite, screen)
        {
            this.scrollable     = scrollable;
            this.onValueChanged = onValueChanged;

            var handleElement = AddUIElement(new UIScrollbarHandle(
                new Transform(transform.Position, new Vector2(transform.Width)),
                handleSprite,
                null,
                this,
                screen
                ));

            handleElement.SetColor(new Color(66, 134, 244));

            //Get the handle group element:
            handle = GetGroupElement(handleElement);

            CalculateScrollbarHandlePosition();
        }
        
        public void IScrollableChanged()
        {
            CalculateContainingBounds();
            CalculateScrollbarHandlePosition();
        }

        private void CalculateContainingBounds()
        {
            float upperY = -transform.Height / 2f;
            float lowerY = transform.Height / 2f;

            containingBounds = new Vector2(upperY + handle.UIElement.Transform.Height / 2f, lowerY - handle.UIElement.Transform.Height / 2f);

            deltaViewSize = MathHelper.Clamp(scrollable.DeltaViewSize(), 0f, 1f);
        }

        private void CalculateScrollbarHandlePosition()
        {
            float yCoord = (containingBounds.Y - containingBounds.X) * scrollbarValue;
            Vector2 offset = new Vector2(0, containingBounds.X + yCoord);

            //Set the position of the handle:
            handle.GroupPosition = offset;

            //Calculate the new position:
            CalculatePositions();
        }

        public void SetHandlePosition(Vector2 mousePosition)
        {
            float value = (MathHelper.Clamp(mousePosition.Y, transform.Y - transform.Height / 2f, transform.Y + transform.Height / 2f) - (transform.Y - transform.Height / 2f)) / transform.Height;

            ScrollbarValue = value;
        }

    }

}
