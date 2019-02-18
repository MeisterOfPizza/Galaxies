using Galaxies.Space;

namespace Galaxies.Controllers
{

    static class PlanetarySystemController
    {

        public static PlanetarySystem CurrentPlanetarySystem { get; private set; }

        public static void SetPlanetarySystem(PlanetarySystem planetarySystem)
        {
            CurrentPlanetarySystem = planetarySystem;
        }

    }

}
