using System.Collections.Generic;

namespace Galaxies.Items
{

    class Inventory
    {

        public IInventoryOwner Owner { get; set; }

        public List<Item> Items { get; private set; } = new List<Item>();

        public Inventory(IInventoryOwner owner)
        {
            this.Owner = owner;
        }

        public void AddItem(Item item)
        {
            Items.Add(item);

            Owner.InventoryChanged(item);
        }

        public void RemoveItem(Item item)
        {
            Items.Remove(item);

            Owner.InventoryChanged(item);
        }

    }

}
