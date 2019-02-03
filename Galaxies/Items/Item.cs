using Galaxies.Core;
using Galaxies.Datas.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.Items
{

    abstract class Item : GameObject
    {

        public ItemData  Data      { get; protected set; }
        public Inventory Inventory { get; private set; }

        public Item(ItemData data, Inventory inventory) : base(MainGame.Singleton.Content.Load<Texture2D>(data.SpriteName), Vector2.Zero, 0, data.Color.GetColor())
        {
            this.Data      = data;
            this.Inventory = inventory;

            Visable = false;
        }

        public virtual void ChangeInventory(Inventory newInventory)
        {
            this.Inventory.RemoveItem(this); //Remove item from old inventory
            this.Inventory = newInventory; //Switch pointer
            this.Inventory.AddItem(this); //Add item to new inventory
        }

    }

}
