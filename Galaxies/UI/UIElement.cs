using Galaxies.Core;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI
{

    /// <summary>
    /// Allows this GameObject to be clicked, as well as focused on.
    /// </summary>
    class UIElement : GameObject
    {

        #region Protected Variables

        protected Screen screen;

        #endregion

        #region Private Properties

        private EventArg OnClick      { get; set; }
        private Color    DefaultColor { get; set; }

        #endregion

        #region Public Properties

        public bool IsFocused    { get; protected set; }
        public bool CanBeClicked { get; private set; }

        #endregion

        public override bool Visable
        {
            get
            {
                return visable;
            }

            set
            {
                visable = value;

                if (CanBeClicked)
                {
                    if (value)
                    {
                        screen.AddUIClickable(this);
                    }
                    else
                    {
                        screen.RemoveUIClickable(this);
                    }
                }
            }
        }

        public Screen Screen
        {
            get
            {
                return screen;
            }
        }

        private bool visable = true;

        public UIElement(Texture2D sprite, Vector2 position, float rotation, Color color, Vector2 size, EventArg onClick, Screen screen, bool canBeClicked = true) : base(sprite, position, rotation, color, size)
        {
            this.OnClick      = onClick;
            this.screen       = screen;
            this.CanBeClicked = canBeClicked;

            DefaultColor = Color;
        }

        public void Click()
        {
            if (CanBeClicked && OnClick != null)
            {
                OnClick.Invoke();
            }
        }

        public void SetOnClick(EventArg @event)
        {
            this.OnClick = @event;
        }

        public virtual void Select()
        {
            IsFocused = true;

            Color = Color.Red;
        }

        public virtual void Deselect()
        {
            IsFocused = false;

            Color = DefaultColor;
        }

    }

}
