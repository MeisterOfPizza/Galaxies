using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.UIControllers
{

    static class GameUIController
    {

        public static Screen CurrentScreen { get; private set; }

        public static GameWindow Window { get; set; }

        public static void Update()
        {
            if (CurrentScreen != null)
            {
                CurrentScreen.Update();
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            if (CurrentScreen != null)
            {
                CurrentScreen.Draw(spriteBatch);
            }
        }

        #region Helpers

        public static Vector2 TopLeftCorner(int width, int height)
        {
            return new Vector2(width / 2f, height / 2f);
        }

        public static Vector2 TopRightCorner(int width, int height)
        {
            return new Vector2(Window.ClientBounds.Width - width / 2f, height / 2f);
        }

        public static Vector2 BottomLeftCorner(int width, int height)
        {
            return new Vector2(width / 2f, Window.ClientBounds.Height - height / 2f);
        }

        public static Vector2 BottomRightCorner(int width, int height)
        {
            return new Vector2(Window.ClientBounds.Width - width / 2f, Window.ClientBounds.Height - height / 2f);
        }

        #endregion

    }

}
