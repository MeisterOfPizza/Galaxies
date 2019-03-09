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

            GalaxyController.Recreate();

            GameUIController.CreateGalaxyScreen();

            Foo();
        }

        static void Foo()
        {
            //TEST: Adding a planetary system
            GalaxyController.Visitables.Add(new Space.PlanetarySystem(DataController.LoadData<Datas.Space.PlanetarySystemData>("test", DataFileType.PlanetarySystems)));

            //TEST: Adding items to player's inventory
            PlayerController.Player.Balance.Deposit(10000);
            for (int i = 0; i < 9; i++)
            {
                PlayerController.Player.Inventory.AddItem(DataController.LoadData<Datas.Items.ShipUpgradeItemData>("0", DataFileType.Items).CreateItem(PlayerController.Player.Inventory));
            }

            //TEST: Adding items to merchant's inventory
            for (int i = 0; i < 9; i++)
            {
                MerchantController.Merchant.Inventory.AddItem(DataController.LoadData<Datas.Items.ShipUpgradeItemData>("0", DataFileType.Items).CreateItem(MerchantController.Merchant.Inventory));
            }
        }
        
        public static void LoadGame(SaveFile saveFile)
        {
            GalaxyController.Recreate();
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
