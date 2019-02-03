namespace Galaxies.Items
{

    interface IInventoryOwner
    {

        Inventory Inventory { get; set; }

        void InventoryChanged(Item changedItem);

    }

}
