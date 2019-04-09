using Galaxies.Core;
using Galaxies.Extensions;
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

        IScrollable    scrollable;
        UIGroupElement handle;

        /// <summary>
        /// How much (in %) can the user see of the scrollable at once?
        /// 1 = entire view, 0 = nothing.
        /// This determines the size of the scrollbar handle.
        /// </summary>
        float deltaViewSize;

        /// <summary>
        /// X = the Y coordinate of the upper bounds of the scrollbar.
        /// Y = the Y coordinate of the lower bounds of the scrollbar.
        /// </summary>
        Vector2 containingBounds;

        /// <summary>
        /// Same as <see cref="ScrollbarValue"/>.
        /// </summary>
        float scrollbarValue;

        EventArg onValueChanged;

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
                }
            }
        }

        #endregion

        public UIScrollbar(Transform transform, Texture2D backgroundSprite, Texture2D handleSprite, EventArg onValueChanged, IScrollable scrollable, Screen screen) : base(transform, backgroundSprite, screen)
        {
            this.scrollable     = scrollable;
            this.onValueChanged = onValueChanged;

            var handleElement = AddUIElement(new UIScrollbarHandle(
                new Transform(new Vector2(transform.Width, 0)),
                handleSprite,
                null,
                this,
                screen
                ));

            //Get the handle group element:
            handle = GetGroupElement(handleElement);

            CalculateScrollbarHandleSize();
        }

        public void IScrollableChanged()
        {
            CalculateContainingBounds();
            CalculateScrollbarHandleSize();
        }

        private void CalculateContainingBounds()
        {
            float upperY = transform.Y - transform.Height / 2f;
            float lowerY = transform.Y + transform.Height / 2f;

            containingBounds = new Vector2(upperY, lowerY);
        }

        /// <summary>
        /// Calculates the size of the scrollbar handle size using <see cref="deltaViewSize"/> and <see cref="IScrollable.DeltaViewSize"/>.
        /// Also calls <see cref="CalculateScrollbarHandlePosition"/>.
        /// </summary>
        private void CalculateScrollbarHandleSize()
        {
            deltaViewSize = MathHelper.Clamp(scrollable.DeltaViewSize(), 0f, 1f);

            handle.UIElement.Transform.SetSizeY(deltaViewSize * transform.Height);

            CalculateScrollbarHandlePosition();
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

    }

}
