using Galaxies.Controllers;
using Galaxies.Datas.Space;

namespace Galaxies.Space.Events
{

    class CombatPlanetEvent : PlanetEvent
    {

        public CombatPlanetEventData data;

        public override PlanetEventData Data
        {
            get
            {
                return data;
            }
        }

        public CombatPlanetEvent(CombatPlanetEventData data)
        {
            this.data = data;
        }

        public override void Trigger()
        {
            CombatController.StartBattle(this);
        }

    }

}
