using Galaxies.Economy;
using Galaxies.Entities;
using Galaxies.Items;

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
            Ship = ShipyardController.PlayerShip_Template_Fighter;

            Player = new Trader(Ship.Inventory, new Balance());
        }

        public static void AssignNewShip(PlayerShip newShip)
        {
            Ship = newShip;

            //Swap the two inventories.
            foreach (Item item in Player.Inventory.Items.ToArray())
            {
                item.ChangeInventory(newShip.Inventory);
            }

            Player.Inventory = Ship.Inventory;

            Ship.RefillStats();
        }

    }

}
