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
            Player = new Trader(null, new Balance());
        }

        public static void AssignNewTrader(Trader trader)
        {
            Player = trader;
            Ship.Inventory = trader.Inventory;
        }

        public static void AssignNewShip(PlayerShip newShip)
        {
            Ship = newShip;

            if (Player != null)
            {
                if (Player.Inventory != null)
                {
                    //Swap the two inventories.
                    foreach (Item item in Player.Inventory.Items.ToArray())
                    {
                        item.ChangeInventory(newShip.Inventory);
                    }
                }

                Player.Inventory = Ship.Inventory;
            }

            Ship.RefillStats();
        }

    }

}
