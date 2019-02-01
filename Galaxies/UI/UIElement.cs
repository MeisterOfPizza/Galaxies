using Galaxies.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI
{

    class UIElement : GameObject
    {

        #region Private Properties

        private OnClickEvent OnClick { get; set; }

        #endregion

        #region Public Properties

        public bool IsFocused    { get; private set; }
        public bool CanBeClicked { get; private set; }

        #endregion

        public delegate void OnClickEvent();

        public UIElement(Texture2D sprite, Vector2 position, float rotation, Color color, OnClickEvent onClick, bool canBeClicked = true) : base(sprite, position, rotation, color)
        {
            this.OnClick      = onClick;
            this.CanBeClicked = canBeClicked;
        }

        public void Click()
        {
            if (CanBeClicked)
            {
                OnClick.Invoke();
            }
        }

        public void Select()
        {
            IsFocused = !IsFocused;
        }

    }

}
