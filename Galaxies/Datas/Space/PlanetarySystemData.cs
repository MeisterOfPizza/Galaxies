using System.Xml.Serialization;

namespace Galaxies.Datas.Space
{

    [XmlRoot(Namespace = "", ElementName = "PlanetarySystem")]
    class PlanetarySystemData : Data
    {

        [XmlElement("SpriteName", IsNullable = false)]
        public string SpriteName { get; set; }

        [XmlElement("MinRandomPlanets", typeof(int), IsNullable = true)]
        public int MinRandomPlanets { get; set; }

        [XmlElement("MaxRandomPlanets", typeof(int), IsNullable = true)]
        public int MaxRandomPlanets { get; set; }

        [XmlArray("PlanetIds", IsNullable = true)]
        [XmlArrayItem("PlanetId", IsNullable = false)]
        public string[] PlanetIds { get; set; }

    }

}
