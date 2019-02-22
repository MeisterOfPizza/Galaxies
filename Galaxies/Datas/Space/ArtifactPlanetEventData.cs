using System;
using System.Xml.Serialization;
using Galaxies.Space.Events;

namespace Galaxies.Datas.Space
{

    [Obsolete("Use ItemDropPlanetEventData instead.", true)] //TODO: Remove obsolete
    [XmlRoot(Namespace = "", ElementName = "Artifact")]
    public class ArtifactPlanetEventData : PlanetEventData
    {

        [XmlElement("MinGalacticGold", typeof(int), IsNullable = false)]
        public int MinGalacticGold { get; set; }

        [XmlElement("MaxGalacticGold", typeof(int), IsNullable = false)]
        public int MaxGalacticGold { get; set; }

        public override PlanetEvent CreateEvent()
        {
            return new ArtifactPlanetEvent(this);
        }

    }

}
