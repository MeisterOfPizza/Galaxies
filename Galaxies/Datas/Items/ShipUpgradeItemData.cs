using Galaxies.Entities;
using System.Xml.Serialization;

namespace Galaxies.Datas.Items
{

    [XmlRoot(Namespace = "", ElementName = "ShipUpgrade")]
    public class ShipUpgradeItemData : ItemData
    {

        [XmlElement("PurchaseGG", typeof(int), IsNullable = false)]
        public int PurchaseGG { get; set; }

        [XmlElement("SellGG", typeof(int), IsNullable = false)]
        public int SellGG { get; set; }

        [XmlElement("ShipStats", typeof(ShipStats), IsNullable = false)]
        public ShipStats ShipStats { get; set; }

        public override ItemType ItemType
        {
            get
            {
                return ItemType.ShipUpgrade;
            }
        }

    }

}
