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
                new ShipStats(100, 100, 10, 1000, 50)
                );

            PlayerShip_Template_Bomber = new PlayerShip(
                new Transform(new Vector2(100)),
                SpriteHelper.GetSprite("Sprites/Player Ships/Player Ship 2"),
                Vector2.Zero,
                new ShipStats(100, 100, 10, 1000, 50)
                );

            PlayerShip_Template_Destroyer = new PlayerShip(
                new Transform(new Vector2(100)),
                SpriteHelper.GetSprite("Sprites/Player Ships/Player Ship 3"),
                Vector2.Zero,
                new ShipStats(100, 100, 10, 1000, 50)
                );
        }

        public static void RefillShipStats()
        {
            PlayerShip_Template_Fighter.RefillStats();
            PlayerShip_Template_Bomber.RefillStats();
            PlayerShip_Template_Destroyer.RefillStats();
        }

    }

}
