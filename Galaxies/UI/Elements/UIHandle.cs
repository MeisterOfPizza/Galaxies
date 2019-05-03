using Galaxies.Core;
using Galaxies.UI.Interfaces;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxies.UI.Elements
{

    abstract class UIHandle : UIElement, IInteractable
    {

        /// <summary>
        /// Has the player let go of the handle with the left mouse button yet?
        /// </summary>
        private bool mouseHasLetGo = true;

        #region IInteractable

        public bool  IsInteractable { get; set; } = true;
        public bool  IsSelected     { get; set; }
        public Color DefaultColor   { get; set; }

        /// <summary>
        /// NOTE: This should ONLY be called whenever the handle is grabbed, NOT whenever its value is changed.
        /// That will be handled differently for each inheritance of <see cref="UIHandle"/>.
        /// </summary>
        public EventArg OnClick { get; set; }

        #endregion

        public override bool Visable
        {
            get
            {
                return base.Visable;
            }

            set
            {
                base.Visable = value;

                IsInteractable = value;
            }
        }

        public UIHandle(Transform transform, Texture2D sprite, EventArg onClick, Screen screen) : base(transform, sprite, screen)
        {
            this.DefaultColor = color;
            this.OnClick      = onClick;
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
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (IsSelected)
            {
                CheckForKeyboard();
            }

            if (!mouseHasLetGo)
            {
                MouseState mouseState = Mouse.GetState();

                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    CheckForMouse();
                }
                else
                {
                    mouseHasLetGo = true;
                }
            }
        }

        #endregion

        #region Checking for input

        /// <summary>
        /// Move the slider with the left and right arrows.
        /// </summary>
        protected abstract void CheckForKeyboard();

        /// <summary>
        /// Move the slider with the mouse.
        /// </summary>
        protected abstract void CheckForMouse();

        #endregion

        #region IInteractable

        public void Click()
        {
            if (OnClick != null)
            {
                OnClick.Invoke();
            }

            mouseHasLetGo = false;
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
