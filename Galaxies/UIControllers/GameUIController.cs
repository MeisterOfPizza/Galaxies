using Galaxies.Controllers;
using Galaxies.Core;
using Galaxies.UI.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Threading.Tasks;

namespace Galaxies.UIControllers
{

    static class GameUIController
    {

        public static Screen CurrentScreen { get; private set; }

        #region Window helpers

        static Vector2 windowSize;

        public static Vector2 WindowSize
        {
            get
            {
                return windowSize;
            }
        }

        public static int WindowWidth
        {
            get
            {
                return (int)windowSize.X;
            }
        }

        public static int WindowHeight
        {
            get
            {
                return (int)windowSize.Y;
            }
        }

        #endregion

        public static void Initialize()
        {
            windowSize = MainGame.Singleton.Window.ClientBounds.Size.ToVector2();
        }

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

        public static void ChangeResolution(int width, int height)
        {
            windowSize = new Vector2(width, height);

            MainGame.Singleton.Graphics.PreferredBackBufferWidth  = width;
            MainGame.Singleton.Graphics.PreferredBackBufferHeight = height;

            MainGame.Singleton.Graphics.ApplyChanges();
        }

        #region Screens

        private static void CreateScreen(Screen newScreen, EventArg onSwitchScreen)
        {
            //Create new screen asynchronously:
            Task.Run(() => CreateScreenAsync(newScreen, onSwitchScreen));
        }

        private async static void CreateScreenAsync(Screen newScreen, EventArg onSwitchScreen)
        {
            await Task.Run(() => newScreen.CreateUI()); //Await the creation of the UI to finish.

            if (CurrentScreen != null)
            {
                //Properly destroy the previous screen.
                CurrentScreen.DestroyScreen();
            }

            CurrentScreen = newScreen;

            if (onSwitchScreen != null)
            {
                onSwitchScreen.Invoke();
            }
        }

        public static void CreateLoadingScreen()
        {
            LoadingScreen loadingScreen = new LoadingScreen();
            loadingScreen.CreateUI();

            CurrentScreen = loadingScreen;
        }

        public static void CreateMenuScreen(EventArg onSwitchScreen = null)
        {
            GameController.GameState = GameState.MainMenu;

            CreateScreen(new MenuScreen(), onSwitchScreen);
        }

        public static void CreateSettingsScreen(EventArg onSwitchScreen = null)
        {
            GameController.GameState = GameState.MainMenu;

            CreateScreen(new SettingsScreen(), onSwitchScreen);
        }

        public static void CreateGalaxyScreen(EventArg onSwitchScreen = null)
        {
            GameController.GameState = GameState.Galaxy;

            CreateScreen(new GalaxyScreen(), onSwitchScreen);
        }

        public static void CreatePlanetarySystemScreen(EventArg onSwitchScreen = null)
        {
            GameController.GameState = GameState.PlanetarySystem;

            CreateScreen(new PlanetarySystemScreen(), onSwitchScreen);
        }

        public static void CreateCombatScreen(EventArg onSwitchScreen = null)
        {
            GameController.GameState = GameState.Combat;

            CreateScreen(new CombatScreen(), onSwitchScreen);
        }

        public static void CreateCitadelScreen(EventArg onSwitchScreen = null)
        {
            //Refill all the player ship templates' stats:
            ShipyardController.RefillShipStats();

            GameController.GameState = GameState.Citadel;

            CreateScreen(new CitadelScreen(), onSwitchScreen);
        }

        public static void CreateGameOverScreen(EventArg onSwitchScreen = null)
        {
            GameController.GameState = GameState.GameOver;

            CreateScreen(new GameOverScreen(), onSwitchScreen);
        }

        public static void CreateVictoryScreen(EventArg onSwitchScreen = null)
        {
            GameController.GameState = GameState.Victory;

            CreateScreen(new VictoryScreen(), onSwitchScreen);
        }

        #endregion

        #region Position helpers

        public static int WidthPercent(float percent)
        {
            return (int)Math.Round(windowSize.X * percent, 0);
        }

        public static int HeightPercent(float percent)
        {
            return (int)Math.Round(windowSize.Y * percent, 0);
        }

        #endregion

    }

}
