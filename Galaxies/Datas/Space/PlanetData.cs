using System.Xml.Serialization;

namespace Galaxies.Datas.Space
{

    [XmlRoot(Namespace = "", ElementName = "Planet")]
    public class PlanetData : Data
    {

        [XmlArray("OnVisitEvents", IsNullable = true)]
        [XmlArrayItem("ItemDrop", typeof(ItemDropPlanetEventData), IsNullable = false)]
        [XmlArrayItem("Combat", typeof(CombatPlanetEventData), IsNullable = false)]
        public PlanetEventData[] OnVisitEvents { get; set; }

    }

}
