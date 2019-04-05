using Galaxies.Core;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Galaxies.UI.Elements
{

    sealed class UIScrollbarHandle : UIHandle
    {

        UIScrollbar scrollbar;

        public UIScrollbarHandle(Transform transform, Texture2D sprite, EventArg onClick, UIScrollbar scrollbar, Screen screen) : base(transform, sprite, onClick, screen)
        {
            this.scrollbar = scrollbar;
        }

        #region Checking for input

        protected override void CheckForKeyboard()
        {
            if (screen.IsSelected_ByKeyboard(this))
            {
                KeyboardState keyboardState = Keyboard.GetState();

                if (keyboardState.IsKeyDown(Keys.Left))
                {
                    //Decrease value:
                    scrollbar.ScrollbarValue -= 0.025f;
                }
                else if (keyboardState.IsKeyDown(Keys.Right))
                {
                    //Increase value:
                    scrollbar.ScrollbarValue += 0.025f;
                }
            }
        }

        protected override void CheckForMouse()
        {
            //throw new NotImplementedException();
        }

        #endregion

    }

}
