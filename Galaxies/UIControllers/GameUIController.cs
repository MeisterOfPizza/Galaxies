using Galaxies.Controllers;
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

        #region Windows helpers

        public static int WindowWidth
        {
            get
            {
                return Window.ClientBounds.Width;
            }
        }

        public static int WindowHeight
        {
            get
            {
                return Window.ClientBounds.Height;
            }
        }

        #endregion

        #region Screens

        public static void CreateMenuScreen()
        {
            GameController.GameState = GameState.MainMenu;

            CurrentScreen = new MenuScreen();
            CurrentScreen.CreateUI(MainGame.Singleton.Content);
        }

        public static void CreateGalaxyScreen()
        {
            GameController.GameState = GameState.Galaxy;

            CurrentScreen = new GalaxyScreen();
            CurrentScreen.CreateUI(MainGame.Singleton.Content);
        }

        public static void CreatePlanetarySystemScreen()
        {
            GameController.GameState = GameState.PlanetarySystem;

            CurrentScreen = new PlanetarySystemScreen();
            CurrentScreen.CreateUI(MainGame.Singleton.Content);
        }

        public static void CreateCombatScreen()
        {
            GameController.GameState = GameState.Combat;

            CurrentScreen = new CombatScreen();
            CurrentScreen.CreateUI(MainGame.Singleton.Content);
        }

        public static void CreateCitadelScreen()
        {
            //Refill the player's stats:
            PlayerController.Ship.RefillStats();

            GameController.GameState = GameState.Citadel;

            CurrentScreen = new CitadelScreen();
            CurrentScreen.CreateUI(MainGame.Singleton.Content);
        }

        #endregion

        #region Position helpers

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
