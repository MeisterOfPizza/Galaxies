using Galaxies.Datas.Items;
using Microsoft.Xna.Framework.Graphics;

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
        /// Can this item be used?
        /// </summary>
        public abstract bool CanUse { get; }

        public abstract Texture2D ItemIcon { get; }

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
