using Galaxies.Entities;
using Galaxies.Progression;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Galaxies.Controllers
{

    static class GameController
    {

        public static PlayerShip Player { get; private set; }

        public static SaveFile CurrentSaveFile { get; private set; }

        public static void StartNewGame()
        {
            
        }

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
        }
        
        private static void LoadSaveGame(SaveFile saveFile)
        {

        }

        public static void UpdateGame(GameTime gameTime)
        {

        }

    }

}
