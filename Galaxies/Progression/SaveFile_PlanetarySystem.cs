using Galaxies.Controllers;
using Galaxies.Datas;
using Galaxies.Datas.Space;
using Galaxies.Space;
using System;

namespace Galaxies.Progression
{

    [Serializable]
    class SaveFile_PlanetarySystem
    {

        public string            Id      { get; private set; }
        public SaveFile_Planet[] Planets { get; private set; }

        public SaveFile_PlanetarySystem(PlanetarySystem planetarySystem)
        {
            this.Id = planetarySystem.Data.Id;

            this.Planets = new SaveFile_Planet[planetarySystem.Planets.Length];

            for (int i = 0; i < Planets.Length; i++)
            {
                Planets[i] = new SaveFile_Planet(planetarySystem.Planets[i].Data.Id, planetarySystem.Planets[i].Visited);
            }
        }

        public PlanetarySystemData GetPlanetarySystemData()
        {
            return DataController.LoadData<PlanetarySystemData>(Id, DataFileType.PlanetarySystems);
        }

    }

}
