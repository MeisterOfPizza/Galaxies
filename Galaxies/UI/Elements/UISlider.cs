using Galaxies.Core;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI.Elements
{

    class UISlider : UIGroup
    {

        #region Variables

        UIGroupElement handle;

        /// <summary>
        /// Total width of the slider.
        /// </summary>
        float totalWidth;

        float value;

        EventArg onValueChanged;

        #endregion

        #region Properties

        public float TotalWidth
        {
            get
            {
                return totalWidth;
            }
        }

        public float Value
        {
            get
            {
                return value;
            }

            set
            {
                float lastValue = this.value;

                this.value = MathHelper.Clamp(value, 0f, 1f);

                //Just to avoid overloading with updates.
                if (!lastValue.Equals(value))
                {
                    UpdateHandle();
                    onValueChanged.Invoke();
                }
            }
        }

        #endregion

        public UISlider(Transform transform, Texture2D barSprite, Texture2D handleSprite, EventArg onValueChanged, float value, Screen screen) : base(transform, null, screen)
        {
            this.value          = MathHelper.Clamp(value, 0f, 1f);
            this.totalWidth     = transform.Width;
            this.onValueChanged = onValueChanged;

            //The actual bar:
            AddUIElement(new UIElement(
                new Transform(transform.Size),
                barSprite,
                screen
                )).SetColor(new Color(48, 48, 48));

            //The handle:
            var handleElement = AddUIElement(new UISliderHandle(
                new Transform(new Vector2(50)),
                handleSprite,
                new EventArg0(UpdateHandle),
                this,
                screen
                ));

            handle = GetGroupElement(handleElement);

            UpdateHandle();
        }

        public override void SizeChanged()
        {
            base.SizeChanged();

            totalWidth = transform.Width;

            UpdateHandle();
        }

        #region Helpers

        public void SetOnValueChanged(EventArg @event)
        {
            onValueChanged = @event;
        }

        private void UpdateHandle()
        {
            float valueOffset = totalWidth * value;
            Vector2 offset = new Vector2(-transform.Width / 2f + valueOffset, 0);
            
            //Update the offset:
            handle.GroupPosition = offset;

            //Position the handle:
            CalculatePositions();
        }

        #endregion

    }

}
