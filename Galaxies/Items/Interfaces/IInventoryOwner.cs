using System.Collections.Generic;

namespace Galaxies.Items
{

    public interface IInventoryOwner
    {

        Inventory Inventory { get; set; }

        void InventoryChanged(Item changedItem);
        void InventoryChanged(IList<Item> changedItems);

    }

}
