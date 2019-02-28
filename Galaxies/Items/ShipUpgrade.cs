using Galaxies.Datas.Items;

namespace Galaxies.Items
{

    class ShipUpgrade : Item
    {

        private ShipUpgradeItemData data;

        public override bool CanSell { get { return true; } }

        public override int PurchaseGG { get { return data.PurchaseGG; } }

        public override int SellGG { get { return data.SellGG; } }

        public ShipUpgrade(ShipUpgradeItemData data, Inventory inventory) : base(data, inventory)
        {
            this.data = data;
        }

    }

}
