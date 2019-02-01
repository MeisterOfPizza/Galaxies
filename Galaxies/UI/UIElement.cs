using Galaxies.Core;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI
{

    class UIElement : GameObject
    {

        #region Private Properties

        private OnClickEvent OnClick      { get; set; }
        private Color        DefaultColor { get; set; }

        #endregion

        #region Public Properties

        public bool IsFocused    { get; protected set; }
        public bool CanBeClicked { get; private set; }

        protected Screen Screen { get; set; }

        #endregion

        public delegate void OnClickEvent();

        public UIElement(Texture2D sprite, Vector2 position, float rotation, Color color, OnClickEvent onClick, Screen screen, bool canBeClicked = true) : base(sprite, position, rotation, color)
        {
            this.OnClick      = onClick;
            this.Screen       = screen;
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
