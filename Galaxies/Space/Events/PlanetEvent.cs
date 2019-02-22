using Galaxies.Datas.Space;

namespace Galaxies.Space.Events
{

    public abstract class PlanetEvent
    {

        public abstract PlanetEventData Data { get; }

        public abstract void Trigger();

    }

}
