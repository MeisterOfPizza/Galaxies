using Galaxies.Datas.Items;
using Galaxies.Extensions;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.Items
{

    class ShipUpgrade : Item
    {

        private ShipUpgradeItemData data;

        public override bool CanSell { get { return true; } }

        public override bool CanUse { get { return false; } }

        public override Texture2D ItemIcon { get { return ContentHelper.GetSprite("Sprites/UI/ship-upgrade-icon"); } }

        public ShipUpgrade(ShipUpgradeItemData data, Inventory inventory) : base(data, inventory)
        {
            this.data = data;
        }

    }

}
