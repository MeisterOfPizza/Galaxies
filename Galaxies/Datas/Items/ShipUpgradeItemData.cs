using Galaxies.Entities;
using Galaxies.Items;
using System.Xml.Serialization;

namespace Galaxies.Datas.Items
{

    [XmlRoot(Namespace = "", ElementName = "ShipUpgrade")]
    public class ShipUpgradeItemData : ItemData
    {

        [XmlElement("ShipStats", typeof(ShipStats), IsNullable = false)]
        public ShipStats ShipStats { get; set; }

        public override ItemType ItemType
        {
            get
            {
                return ItemType.ShipUpgrade;
            }
        }

        public override Item CreateItem(Inventory inventory)
        {
            return new ShipUpgrade(this, inventory);
        }

    }

}
