using Galaxies.Space;
using System.Collections.Generic;

namespace Galaxies.Controllers
{

    static class GalaxyController
    {

        public static List<IVisitable> Visitables { get; private set; } = new List<IVisitable>();

        public static void Recreate()
        {
            Visitables.Clear();
            Visitables.Add(new Citadel());
        }

        public static void AddVisitables(IList<IVisitable> visitables)
        {
            Visitables.AddRange(visitables);
        }

    }

}
