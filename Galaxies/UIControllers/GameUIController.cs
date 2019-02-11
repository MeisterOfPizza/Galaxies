using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Galaxies.UIControllers
{

    static class GameUIController
    {

        public static Screen CurrentScreen { get; private set; }

        public static GameWindow Window { get; set; }

        public static void Update(GameTime gameTime)
        {
            if (CurrentScreen != null)
            {
                CurrentScreen.Update(gameTime);
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

        public static void CreateMenuScreen()
        {
            CurrentScreen = new MenuScreen();
            CurrentScreen.CreateUI(MainGame.Singleton.Content);
        }

        public static void CreateGalaxyScreen()
        {
            CurrentScreen = new GalaxyScreen();
            CurrentScreen.CreateUI(MainGame.Singleton.Content);
        }

        public static void CreatePlanetarySystemScreen()
        {
            CurrentScreen = new PlanetarySystemScreen();
            CurrentScreen.CreateUI(MainGame.Singleton.Content);
        }

        public static void CreateCombatScreen()
        {
            CurrentScreen = new CombatScreen();
            CurrentScreen.CreateUI(MainGame.Singleton.Content);
        }

        #endregion

        #region Position helpers

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
