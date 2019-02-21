using System.Xml.Serialization;
using Galaxies.Items;

namespace Galaxies.Datas.Items
{

    [XmlRoot(Namespace = "", ElementName = "StarChart")]
    public class StarChartItemData : ItemData
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

        public override Item CreateItem(Inventory inventory)
        {
            return new StarChart(this, inventory);
        }

    }

}
