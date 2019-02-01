using Galaxies.Datas.Space;

namespace Galaxies.Space
{

    class PlanetarySystem
    {

        public PlanetarySystemData Data { get; private set; }

        public Planet[] Planets { get; private set; }

        public PlanetarySystem(PlanetarySystemData data)
        {
            this.Data = data;
        }

    }

}
