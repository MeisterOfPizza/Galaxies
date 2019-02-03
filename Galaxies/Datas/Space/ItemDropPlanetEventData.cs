using Galaxies.Space.Events;
using System.Xml.Serialization;

namespace Galaxies.Datas.Space
{

    [XmlRoot(Namespace = "", ElementName = "ItemDrop")]
    class ItemDropPlanetEventData : PlanetEventData
    {

        [XmlArray("ItemIds", IsNullable = false)]
        [XmlArrayItem("ItemId", IsNullable = false)]
        public string[] ItemIds { get; set; }

        public override PlanetEvent CreateEvent()
        {
            return new ItemDropPlanetEvent(this);
        }

    }

}
