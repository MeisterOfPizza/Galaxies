using Galaxies.Space.Events;
using System.Xml.Serialization;

namespace Galaxies.Datas.Space
{

    [XmlRoot(Namespace = "", ElementName = "ItemDrop")]
    class ItemDropPlanetEventData : PlanetEventData
    {

        [XmlArray("Items", IsNullable = false)]
        [XmlArrayItem("Item", IsNullable = false)]
        public string[] Items { get; set; }

        public override PlanetEvent CreateEvent()
        {
            return new ItemDropPlanetEvent(this);
        }

    }

}
