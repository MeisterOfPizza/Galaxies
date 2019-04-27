using Galaxies.Core;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxies.UI.Elements
{

    /// <summary>
    /// Used with the <see cref="UISlider"/> class.
    /// </summary>
    sealed class UISliderHandle : UIHandle
    {

        private UISlider slider;

        public UISliderHandle(Transform transform, Texture2D sprite, EventArg onClick, UISlider slider, Screen screen) : base(transform, sprite, onClick, screen)
        {
            this.slider = slider;
        }

        #region Checking for input

        /// <summary>
        /// Move the slider with the left and right arrows.
        /// </summary>
        protected override void CheckForKeyboard()
        {
            if (screen.IsSelected_ByKeyboard(this))
            {
                KeyboardState keyboardState = Keyboard.GetState();

                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    //Decrease value:
                    slider.Value -= 0.025f;
                }
                else if (keyboardState.IsKeyDown(Keys.Right))
                {
                    //Increase value:
                    slider.Value += 0.025f;
                }
            }
        }

        /// <summary>
        /// Move the slider with the mouse.
        /// </summary>
        protected override void CheckForMouse()
        {
            MouseState mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                float mouseX    = mouseState.Position.X; //Where is the mouse?
                float halfWidth = slider.Transform.Width / 2f; //For faster calculations.

                //Clamp it so we know where the mouse (X coordinate) is relative to the handle.
                mouseX = MathHelper.Clamp(slider.Transform.X + halfWidth - mouseX, 0, slider.TotalWidth);

                //Apply the mouse movement.
                slider.Value = 1f - mouseX / slider.TotalWidth;
            }
        }

        #endregion

    }

}
