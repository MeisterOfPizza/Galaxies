using System.Xml.Serialization;

namespace Galaxies.Datas.Items
{

    [XmlRoot(Namespace = "", ElementName = "StarChart")]
    class StarChartItemData : ItemData
    {

        [XmlElement("Planetary SystemId", IsNullable = false)]
        public string PlanetarySystemId { get; set; }

        public override ItemType ItemType
        {
            get
            {
                return ItemType.StarChart;
            }
        }

    }

}
