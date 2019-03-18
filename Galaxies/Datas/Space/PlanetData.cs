using System.Xml.Serialization;

namespace Galaxies.Datas.Space
{

    [XmlRoot(Namespace = "", ElementName = "Planet")]
    public class PlanetData : Data
    {

        [XmlElement("SpriteName", IsNullable = false)]
        public string SpriteName { get; set; }

        [XmlArray("OnVisitEvents", IsNullable = true)]
        [XmlArrayItem("ItemDrop", typeof(ItemDropPlanetEventData), IsNullable = false)]
        [XmlArrayItem("Combat", typeof(CombatPlanetEventData), IsNullable = false)]
        public PlanetEventData[] OnVisitEvents { get; set; }

    }

}
