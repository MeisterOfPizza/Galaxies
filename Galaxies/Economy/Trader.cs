using Galaxies.Items;
using System.Collections.Generic;

namespace Galaxies.Economy
{

    /// <summary>
    /// Container class.
    /// Contains a reference to an inventory and a balance.
    /// </summary>
    class Trader : IInventoryOwner
    {

        public Inventory Inventory { get; set; }
        public Balance   Balance   { get; set; }

        public Trader(Inventory inventory, Balance balance)
        {
            this.Inventory = inventory;
            this.Balance   = balance;
        }

        public void InventoryChanged(Item changedItem)
        {
            //Do nothing here.
        }

        public void InventoryChanged(IList<Item> changedItems)
        {
            //Do nothing here.
        }
    }

}
