using Galaxies.Datas.Space;
using Galaxies.Progression;
using Galaxies.Space;
using System.Collections.Generic;

namespace Galaxies.Controllers
{

    static class GalaxyController
    {

        public static List<IVisitable> Visitables { get; private set; } = new List<IVisitable>();

        static GalaxyController()
        {
            Visitables.Add(new Citadel());
        }

        public static void LoadSaveFile(SaveFile saveFile)
        {
            foreach (string planetarySystemId in saveFile.PlanetarySystemIds)
            {
                Visitables.Add(new PlanetarySystem(DataController.LoadData<PlanetarySystemData>(planetarySystemId, DataFileType.PlanetarySystems)));
            }
        }

    }

}
