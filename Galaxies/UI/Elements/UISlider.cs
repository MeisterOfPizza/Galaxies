using Galaxies.Core;
using Galaxies.UI.Interfaces;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxies.UI.Elements
{

    class UISlider : UIGroup, IInteractable
    {

        #region Variables

        UIGroupElement handle;

        /// <summary>
        /// Total width of the slider.
        /// </summary>
        float totalWidth;

        float value;

        #endregion

        #region Properties

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
                    OnClick.Invoke();
                }
            }
        }

        #region IInteractable

        public bool  IsInteractable { get; set; } = true;
        public bool  IsSelected     { get; set; }
        public Color DefaultColor   { get; set; }

        /// <summary>
        /// This is actually not called on click, but instead whenever the handle is moving.
        /// </summary>
        public EventArg OnClick { get; set; }

        #endregion

        #endregion

        public UISlider(Transform transform, Texture2D barSprite, Texture2D handleSprite, EventArg onValueChanged, float value, Screen screen) : base(transform, null, screen)
        {
            this.value        = MathHelper.Clamp(value, 0f, 1f);
            this.totalWidth   = transform.Width;
            this.DefaultColor = color;
            this.OnClick      = onValueChanged;

            //The actual bar:
            AddUIElement(new UIElement(
                new Transform(transform.Size),
                barSprite,
                screen
                )).SetColor(new Color(48, 48, 48));

            //The handle:
            var handleElement = AddUIElement(new UIElement(
                new Transform(new Vector2(50, 50)),
                handleSprite,
                screen
                ));

            handle = GetGroupElement(handleElement);

            UpdateHandle();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (IsSelected)
            {
                CheckForKeyboard();
                CheckForMouse();
            }
        }

        public override void SizeChanged()
        {
            base.SizeChanged();

            totalWidth = transform.Width;

            UpdateHandle();
        }

        public override void SetColor(Color color)
        {
            base.SetColor(color);

            DefaultColor = color;
        }

        #region Helpers

        private void UpdateHandle()
        {
            float valueOffset = totalWidth * value;
            Vector2 offset = new Vector2(-transform.Width / 2f + valueOffset, 0);
            
            //Update the offset:
            handle.GroupPosition = offset;

            //Position the handle:
            CalculatePositions();
        }

        /// <summary>
        /// Move the slider with the left and right arrows.
        /// </summary>
        private void CheckForKeyboard()
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Left))
            {
                //Decrease value:
                Value -= 0.025f;
            }
            else if (keyboardState.IsKeyDown(Keys.Right))
            {
                //Increase value:
                Value += 0.025f;
            }
        }

        /// <summary>
        /// Move the slider with the mouse.
        /// </summary>
        private void CheckForMouse()
        {
            MouseState mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed)
            {
                float mouseX = mouseState.Position.X; //Where is the mouse?
                float halfWidth = transform.Width / 2f; //for faster calculations.

                //Clamp it so we know where the mouse is (X) relative to the handle.
                mouseX = MathHelper.Clamp(transform.X + halfWidth - mouseX, 0, totalWidth);

                //Apply the mouse movement.
                Value = 1f - mouseX / totalWidth;
            }
        }

        #endregion

        #region IInteractable

        public void Click()
        {
            //Do nothing
        }

        public void SetOnClick(EventArg @event)
        {
            OnClick = @event;
        }

        public void Select()
        {
            handle.UIElement.SetColor(new Color(DefaultColor * 0.9f, 1f));

            IsSelected = true;
        }

        public void Deselect()
        {
            handle.UIElement.SetColor(DefaultColor);

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
