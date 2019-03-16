using Galaxies.Progression;
using Galaxies.UIControllers;
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

        public static void NewGame()
        {
            MerchantController.CreateNewMerchant();
            PlayerController.CreateNewPlayer();

            //Assign the player the first ship template:
            ShipyardController.AssignPlayerShip(ShipyardController.PlayerShipTemplates[0]);

            GalaxyController.CreateNewGalaxy();

            GameUIController.CreateGalaxyScreen();
        }
        
        public static void LoadGame(SaveFile saveFile)
        {
            if (saveFile != null)
            {
                GalaxyController.ResetGalaxy();
                MerchantController.CreateNewMerchant();

                //Default (these should always load):
                saveFile.Load_PlanetarySystems();
                saveFile.Load_Player();

                switch (saveFile.GameState)
                {
                    default:
                    case SaveFile_GameState.Galaxy:
                        GameUIController.CreateGalaxyScreen();
                        break;
                    case SaveFile_GameState.PlanetarySystem:
                        GameUIController.CreatePlanetarySystemScreen();
                        saveFile.Load_CurrentPlanetarySystem();
                        break;
                    case SaveFile_GameState.Citadel:
                        GameUIController.CreateCitadelScreen();
                        break;
                }
            }
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            //TODO: Clean up
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
            //TODO: Clean up
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
