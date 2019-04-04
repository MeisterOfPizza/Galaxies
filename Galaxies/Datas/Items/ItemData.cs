using Galaxies.Datas.Helpers;
using System.Xml.Serialization;
using Galaxies.Items;

namespace Galaxies.Datas.Items
{

    public enum ItemType
    {
        ShipUpgrade,
        StarChart
    }

    [XmlRoot(Namespace = "", ElementName = "Item")]
    public abstract class ItemData : Data
    {

        [XmlElement("Color", typeof(ColorData), IsNullable = true)]
        public ColorData Color { get; set; } = new ColorData();

        [XmlElement("GalacticGoldValue", typeof(int), IsNullable = false)]
        public int GalacticGoldValue { get; set; }

        public abstract ItemType ItemType { get; }

        public abstract Item CreateItem(Inventory inventory);

    }

}
