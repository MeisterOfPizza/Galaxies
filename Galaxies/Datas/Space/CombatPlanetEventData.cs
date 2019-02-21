using Galaxies.Datas.Helpers;
using Galaxies.Space.Events;
using System.Xml.Serialization;

namespace Galaxies.Datas.Space
{

    [XmlRoot(Namespace = "", ElementName = "Combat")]
    public class CombatPlanetEventData : PlanetEventData
    {

        [XmlArray("EnemyPointers", IsNullable = false)]
        [XmlArrayItem("EnemyPointer", typeof(DataPointer), IsNullable = false)]
        public DataPointer[] EnemyPointers { get; set; }

        public override PlanetEvent CreateEvent()
        {
            return new CombatPlanetEvent(this);
        }

    }

}
