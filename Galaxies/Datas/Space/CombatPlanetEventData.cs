using Galaxies.Space.Events;
using System.Xml.Serialization;

namespace Galaxies.Datas.Space
{

    [XmlRoot(Namespace = "", ElementName = "Combat")]
    class CombatPlanetEventData : PlanetEventData
    {

        [XmlArray("Enemies", IsNullable = false)]
        [XmlArrayItem("Enemy", IsNullable = false)]
        public string[] Enemies { get; set; }

        public override PlanetEvent CreateEvent()
        {
            return new CombatPlanetEvent(this);
        }

    }

}
