using System.Xml.Serialization;

namespace Galaxies.Datas.Space
{

    [XmlRoot(Namespace = "", ElementName = "PlanetarySystem")]
    public class PlanetarySystemData : Data
    {

        [XmlElement("SpriteName", IsNullable = false)]
        public string SpriteName { get; set; }

        [XmlElement("MinRandomPlanets", typeof(int), IsNullable = false)]
        public int MinRandomPlanets { get; set; }

        [XmlElement("MaxRandomPlanets", typeof(int), IsNullable = false)]
        public int MaxRandomPlanets { get; set; }

        [XmlArray("PlanetIds", IsNullable = false)]
        [XmlArrayItem("PlanetId", IsNullable = false)]
        public string[] PlanetIds { get; set; }

    }

}
