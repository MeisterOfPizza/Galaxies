using Galaxies.Datas.Items;

namespace Galaxies.Items
{

    public abstract class Item
    {

        public ItemData  Data      { get; protected set; }
        public Inventory Inventory { get; private set; }

        #region Helpers

        /// <summary>
        /// Can this item be sold?
        /// </summary>
        public abstract bool CanSell { get; }

        /// <summary>
        /// How much does purchasing this item cost the player?
        /// </summary>
        public virtual int PurchaseGG { get; }

        /// <summary>
        /// How much does selling this item yield the player?
        /// </summary>
        public virtual int SellGG { get; }

        #endregion

        public Item(ItemData data, Inventory inventory)
        {
            this.Data      = data;
            this.Inventory = inventory;
        }

        public virtual void ChangeInventory(Inventory newInventory)
        {
            this.Inventory.RemoveItem(this); //Remove item from old inventory
            this.Inventory = newInventory; //Switch pointer
            this.Inventory.AddItem(this); //Add item to new inventory
        }

    }

}
