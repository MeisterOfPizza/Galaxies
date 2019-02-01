using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

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

        #region Screens

        public static void CreateGalaxyScreen(ContentManager content)
        {
            CurrentScreen = new GalaxyScreen();
            CurrentScreen.CreateUI(content);
        }

        #endregion

        #region Position helpers

        public static Vector2 TopLeftCorner()
        {
            return Vector2.Zero;
        }

        public static Vector2 TopRightCorner(int width, int height)
        {
            return new Vector2(Window.ClientBounds.Width - width, 0);
        }

        public static Vector2 BottomLeftCorner(int width, int height)
        {
            return new Vector2(0, Window.ClientBounds.Height - height);
        }

        public static Vector2 BottomRightCorner(int width, int height)
        {
            return new Vector2(Window.ClientBounds.Width - width, Window.ClientBounds.Height - height);
        }

        public static Vector2 Center(int width, int height)
        {
            return new Vector2(Window.ClientBounds.Width / 2f - width / 2f, Window.ClientBounds.Height / 2f - height / 2f);
        }

        public static int WidthPercent(float percent)
        {
            return (int)Math.Round(Window.ClientBounds.Width * percent, 0);
        }

        public static int HeightPercent(float percent)
        {
            return (int)Math.Round(Window.ClientBounds.Height * percent, 0);
        }

        #endregion

    }

}
