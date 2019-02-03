using Galaxies.Datas.Space;
using System;

namespace Galaxies.Space.Events
{

    class CombatPlanetEvent : PlanetEvent
    {

        private CombatPlanetEventData data;

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
            throw new NotImplementedException();
        }

    }

}
