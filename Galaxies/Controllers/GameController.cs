using Galaxies.Entities;
using Galaxies.Progression;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.Controllers
{

    enum GameState
    {
        MainMenu,
        Galaxy,
        PlanetarySystem,
        Combat,
        Citadel
    }

    static class GameController
    {

        public static GameState GameState { get; set; }

        public static SaveFile CurrentSaveFile { get; private set; }

        public static void LoadGame(SaveFile saveFile)
        {
            CurrentSaveFile = saveFile;

            if (!CurrentSaveFile.IsNewGame) //TODO: Check if a save file exists, if so: check if it's valid.
            {
                LoadSaveGame(saveFile);
            }
            else
            {
                LoadNewGame();
            }
        }

        private static void LoadNewGame()
        {
            CurrentSaveFile.IsNewGame = false;

            MerchantController.CreateMerchant();
            ShipyardController.CreateShipyard();
            PlayerController.CreateNewPlayer();
        }
        
        private static void LoadSaveGame(SaveFile saveFile)
        {

        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            switch (GameState)
            {
                case GameState.MainMenu:
                    break;
                case GameState.Galaxy:
                    break;
                case GameState.PlanetarySystem:
                    break;
                case GameState.Combat:
                    CombatController.Draw(spriteBatch);
                    break;
                case GameState.Citadel:
                    break;
            }
        }

        public static void Update(GameTime gameTime)
        {
            switch (GameState)
            {
                case GameState.MainMenu:
                    break;
                case GameState.Galaxy:
                    break;
                case GameState.PlanetarySystem:
                    break;
                case GameState.Combat:
                    CombatController.Update();
                    break;
                case GameState.Citadel:
                    break;
            }
        }

    }

}
