using Galaxies.Space;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
