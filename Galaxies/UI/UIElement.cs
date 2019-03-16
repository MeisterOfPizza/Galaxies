using Galaxies.Core;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UI
{

    /// <summary>
    /// Base class for all UI Elements.
    /// </summary>
    class UIElement : GameObject
    {

        #region Protected fields

        protected Screen screen;

        #endregion

        #region Public properties

        public Screen Screen
        {
            get
            {
                return screen;
            }
        }

        #endregion

        public UIElement(Transform transform, Texture2D sprite, Screen screen) : base(transform, sprite)
        {
            this.screen = screen;
        }

        public virtual void OnDestroy()
        {
            //Do nothing here.
            //We'll leave this method blank (and not modified as abstract).
            //This is because we don't want to override this method in ALL classes further down the line.
            //It looks ugly but it's the best we can do.
        }

    }

}
