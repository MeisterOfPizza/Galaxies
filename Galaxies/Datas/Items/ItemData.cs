using System.Xml.Serialization;

namespace Galaxies.Datas.Items
{

    public enum ItemType
    {
        ShipUpgrade,
        Artifact,
        StarChart
    }

    [XmlRoot(Namespace = "", ElementName = "Item")]
    public abstract class ItemData : Data
    {

        [XmlElement("SpriteName", IsNullable = false)]
        public string SpriteName { get; set; }

        [XmlElement("Color", typeof(ColorData), IsNullable = true)]
        public ColorData Color { get; set; }

        public abstract ItemType ItemType { get; }

    }

}
