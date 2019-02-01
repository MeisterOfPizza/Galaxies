using Galaxies.Datas.Space;
using System;

namespace Galaxies.Space.Events
{

    class ArtifactPlanetEvent : PlanetEvent
    {

        private ArtifactPlanetEventData data;

        public override PlanetEventData Data
        {
            get
            {
                return data;
            }
        }

        public ArtifactPlanetEvent(ArtifactPlanetEventData data)
        {
            this.data = data;
        }

        public override void Trigger()
        {
            throw new NotImplementedException();
        }

    }

}
