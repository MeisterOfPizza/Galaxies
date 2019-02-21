using Galaxies.Datas.Helpers;
using System.Xml.Serialization;
using Galaxies.Items;

namespace Galaxies.Datas.Items
{

    public enum ItemType
    {
        ShipUpgrade,
        Artifact, //TODO: Remove obsolete
        StarChart
    }

    [XmlRoot(Namespace = "", ElementName = "Item")]
    public abstract class ItemData : Data
    {

        [XmlElement("SpriteName", IsNullable = false)]
        public string SpriteName { get; set; }

        [XmlElement("Color", typeof(ColorData), IsNullable = true)]
        public ColorData Color { get; set; } = new ColorData();

        public abstract ItemType ItemType { get; }

        public abstract Item CreateItem(Inventory inventory);

    }

}
