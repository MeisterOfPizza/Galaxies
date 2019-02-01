using Galaxies.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Galaxies.Controllers
{

    static class GameController
    {

        public static PlayerShip Player { get; set; }

        public static void StartNewGame()
        {

        }

        public static void LoadGame(ContentManager content)
        {
            if (false) //Check if a save file exists, if so: check if it's valid.
            {
                LoadSaveGame(content);
            }
            else
            {
                LoadNewGame(content);
            }
        }

        private static void LoadNewGame(ContentManager content)
        {
            
        }
        
        private static void LoadSaveGame(ContentManager content)
        {

        }

        public static void UpdateGame(GameTime gameTime)
        {

        }

    }

}
