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

    }

}
