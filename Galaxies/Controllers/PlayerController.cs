using Galaxies.Economy;
using Galaxies.Entities;
using Galaxies.Items;

namespace Galaxies.Controllers
{

    static class PlayerController
    {

        public static PlayerShip   Ship   { get; private set; }
        public static PlayerTrader Player { get; private set; }

        /// <summary>
        /// Creates an entirely new player.
        /// </summary>
        public static void CreateNewPlayer()
        {
            Player = new PlayerTrader(null, new Balance());
            Inventory inventory = new Inventory(Player);
            Player.Inventory = inventory;

            //Give the player the default starting money:
            Player.Balance.Deposit(250);
        }

        public static void AssignNewTrader(PlayerTrader trader)
        {
            Player = trader;
        }

        public static void AssignNewShip(PlayerShip newShip)
        {
            Ship = newShip;

            Ship.RefillStats();
        }

    }

}
