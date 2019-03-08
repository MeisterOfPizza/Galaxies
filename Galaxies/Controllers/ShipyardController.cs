using Galaxies.Core;
using Galaxies.Entities;
using Galaxies.Extensions;
using Microsoft.Xna.Framework;

namespace Galaxies.Controllers
{

    static class ShipyardController
    {
        
        public static PlayerShip PlayerShip_Template_Fighter   { get; private set; }
        public static PlayerShip PlayerShip_Template_Bomber    { get; private set; }
        public static PlayerShip PlayerShip_Template_Destroyer { get; private set; }

        public static void CreateShipyard()
        {
            //Create the player ship templates:

            PlayerShip_Template_Fighter = new PlayerShip(
                new Transform(new Vector2(100)),
                SpriteHelper.GetSprite("Sprites/Player Ships/Player Ship 1"),
                Vector2.Zero,
                new ShipStats(100, 250, 15, 500, 75),
                0
                );

            PlayerShip_Template_Fighter.Unlocked = true; //The default PlayerShip should ALWAYS be unlocked.

            PlayerShip_Template_Bomber = new PlayerShip(
                new Transform(new Vector2(100)),
                SpriteHelper.GetSprite("Sprites/Player Ships/Player Ship 2"),
                Vector2.Zero,
                new ShipStats(500, 500, 10, 1250, 35),
                1000
                );

            PlayerShip_Template_Destroyer = new PlayerShip(
                new Transform(new Vector2(100)),
                SpriteHelper.GetSprite("Sprites/Player Ships/Player Ship 3"),
                Vector2.Zero,
                new ShipStats(250, 1000, 50, 2000, 25),
                2500
                );
        }

        public static void RefillShipStats()
        {
            PlayerShip_Template_Fighter.RefillStats();
            PlayerShip_Template_Bomber.RefillStats();
            PlayerShip_Template_Destroyer.RefillStats();
        }

        public static void AssignPlayerShip(PlayerShip playerShip)
        {
            playerShip.Unlocked = true;

            PlayerController.AssignNewShip(playerShip);
        }

    }

}
