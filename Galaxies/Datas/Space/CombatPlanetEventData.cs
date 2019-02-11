using Galaxies.Space.Events;
using System.Xml.Serialization;

namespace Galaxies.Datas.Space
{

    [XmlRoot(Namespace = "", ElementName = "Combat")]
    public class CombatPlanetEventData : PlanetEventData
    {

        [XmlArray("EnemyIds", IsNullable = false)]
        [XmlArrayItem("EnemyId", IsNullable = false)]
        public string[] EnemyIds { get; set; }

        public override PlanetEvent CreateEvent()
        {
            return new CombatPlanetEvent(this);
        }

    }

}
