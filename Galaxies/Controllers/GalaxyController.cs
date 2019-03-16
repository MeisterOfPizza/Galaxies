using Galaxies.Datas.Space;
using Galaxies.Space;
using System.Collections.Generic;

namespace Galaxies.Controllers
{

    static class GalaxyController
    {

        public static List<IVisitable> Visitables { get; private set; } = new List<IVisitable>();
        
        private static VisitablesData VisitablesData { get; set; }

        public static void Initialize()
        {
            VisitablesData = DataController.LoadData<VisitablesData>("default", DataFileType.Visitables);
        }

        public static void CreateNewGalaxy()
        {
            ResetGalaxy();

            Visitables.Add(VisitablesData.GetRandom());
        }

        public static void ResetGalaxy()
        {
            Visitables.Clear();
            Visitables.Add(new Citadel());
        }

        public static void AddVisitables(IList<IVisitable> visitables)
        {
            Visitables.AddRange(visitables);
        }

        public static void RemoveVisistable(IVisitable visitable)
        {
            Visitables.Remove(visitable);

            //Always allow the player to visit at least one planet for "free".
            if (Visitables.Count <= 1)
            {
                Visitables.Add(VisitablesData.GetRandom());
            }
        }

    }

}
