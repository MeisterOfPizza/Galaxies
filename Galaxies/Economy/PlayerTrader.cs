using Galaxies.Controllers;
using Galaxies.Items;
using System.Collections.Generic;

namespace Galaxies.Economy
{

    class PlayerTrader : Trader
    {

        public PlayerTrader(Inventory inventory, Balance balance) : base(inventory, balance)
        {

        }

        public override void InventoryChanged(Item changedItem)
        {
            base.InventoryChanged(changedItem);

            PlayerController.Ship.CalculateStats();
        }

        public override void InventoryChanged(IList<Item> changedItems)
        {
            base.InventoryChanged(changedItems);

            PlayerController.Ship.CalculateStats();
        }

    }

}
