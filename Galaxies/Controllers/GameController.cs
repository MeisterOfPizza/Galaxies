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

        /*
        public static void LoadGame()
        {
            if (!SaveFileController.CurrentSaveFile.IsNewGame) //TODO: Check if a save file exists, if so: check if it's valid.
            {
                LoadSaveGame(SaveFileController.CurrentSaveFile);
            }
            else
            {
                LoadNewGame();
            }
        }
        */

        public static void NewGame()
        {
            SaveFileController.CurrentSaveFile.IsNewGame = false;

            MerchantController.CreateMerchant();
            PlayerController.CreateNewPlayer();
            ShipyardController.CreateShipyard();
        }
        
        public static void LoadGame(SaveFile saveFile)
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
