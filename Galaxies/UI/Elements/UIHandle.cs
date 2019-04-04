using Galaxies.Core;
using Galaxies.UI.Interfaces;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxies.UI.Elements
{
    /// <summary>
    /// Used with the <see cref="UISlider"/> class.
    /// </summary>
    sealed class UIHandle : UIElement, IInteractable
    {

        UISlider slider;

        #region IInteractable

        public bool  IsInteractable { get; set; } = true;
        public bool  IsSelected     { get; set; }
        public Color DefaultColor   { get; set; }

        /// <summary>
        /// NOTE: This is NOT called whenever the value of the slider that the handle is attached to is changed.
        /// </summary>
        public EventArg OnClick { get; set; }

        #endregion

        public UIHandle(Transform transform, Texture2D sprite, EventArg onClick, UISlider slider, Screen screen) : base(transform, sprite, screen)
        {
            this.DefaultColor = color;
            this.OnClick      = onClick;
            this.slider       = slider;
        }

        #region Overriden methods

        public override void SetColor(Color color)
        {
            base.SetColor(color);

            DefaultColor = color;
        }

        public override void PositionChanged()
        {
            base.PositionChanged();

            IsSelected = false;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (IsSelected)
            {
                CheckForKeyboard();
                CheckForMouse();
            }
            //Check if the mouse is still over this element:
            else if (screen.IsSelected_ByMouse(this) || screen.IsSelected_ByKeyboard(this))
            {
                Select();
            }
            else
            {
                Deselect();
            }
        }

        #endregion

        #region Checking for input

        /// <summary>
        /// Move the slider with the left and right arrows.
        /// </summary>
        private void CheckForKeyboard()
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
        private void CheckForMouse()
        {
            if (screen.IsSelected_ByMouse(this))
            {
                MouseState mouseState = Mouse.GetState();

                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    float mouseX = mouseState.Position.X; //Where is the mouse?
                    float halfWidth = slider.Transform.Width / 2f; //For faster calculations.

                    //Clamp it so we know where the mouse (X coordinate) is relative to the handle.
                    mouseX = MathHelper.Clamp(slider.Transform.X + halfWidth - mouseX, 0, slider.TotalWidth);

                    //Apply the mouse movement.
                    slider.Value = 1f - mouseX / slider.TotalWidth;
                }
            }
        }

        #endregion

        #region IInteractable

        public void Click()
        {
            if (OnClick != null)
            {
                OnClick.Invoke();
            }
        }

        public void SetOnClick(EventArg @event)
        {
            OnClick = @event;
        }

        public void Select()
        {
            color = new Color(DefaultColor * 0.9f, 1f);

            IsSelected = true;
        }

        public void Deselect()
        {
            color = DefaultColor;

            IsSelected = false;
        }

        public void MouseEnter()
        {
            Select();
        }

        public void MouseExit()
        {
            Deselect();
        }

        #endregion

    }

}
