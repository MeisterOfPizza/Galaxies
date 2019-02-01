using System.Xml.Serialization;

namespace Galaxies.Datas.Space
{

    [XmlRoot(Namespace = "", ElementName = "Planet")]
    class PlanetData : Data
    {

        [XmlElement("SpriteName", IsNullable = false)]
        public string SpriteName { get; set; }

        [XmlArray("OnVisitEvents", IsNullable = true)]
        [XmlArrayItem("Artifact", typeof(ArtifactPlanetEventData), IsNullable = false)]
        [XmlArrayItem("ItemDrop", typeof(ItemDropPlanetEventData), IsNullable = false)]
        [XmlArrayItem("Combat", typeof(CombatPlanetEventData), IsNullable = false)]
        public PlanetEventData[] OnVisitEvents { get; set; }

    }

}
