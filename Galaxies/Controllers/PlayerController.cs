using Galaxies.Core;
using Galaxies.Economy;
using Galaxies.Entities;
using Galaxies.Extensions;
using Galaxies.Items;
using Microsoft.Xna.Framework;

namespace Galaxies.Controllers
{

    static class PlayerController
    {

        public static PlayerShip Ship   { get; private set; }
        public static Trader     Player { get; private set; }

        /// <summary>
        /// Creates an entirely new player.
        /// </summary>
        public static void CreateNewPlayer()
        {
            Ship = new PlayerShip(
                new Transform(new Vector2(100)),
                SpriteHelper.GetSprite("Sprites/Player Ships/Player Ship 1"),
                Vector2.Zero,
                new ShipStats(100, 100, 10, 1000, 50)
                );

            Player = new Trader(Ship.Inventory, new Balance());
        }

        public static void AssignNewShip(PlayerShip newShip)
        {
            Ship = newShip;

            //Swap the two inventories.
            foreach (Item item in Player.Inventory.Items)
            {
                item.ChangeInventory(newShip.Inventory);
            }

            Player.Inventory = Ship.Inventory;
        }

    }

}
