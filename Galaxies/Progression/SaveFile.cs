using Galaxies.Controllers;
using Galaxies.Datas.Space;
using System;

namespace Galaxies.Progression
{

    [Serializable]
    class SaveFile
    {

        /// <summary>
        /// Check if this save file was just created.
        /// </summary>
        public bool IsNewGame { get; set; } = true;

        public string   PlayerName       { get; set; }
        public string   SaveFileName     { get; set; }
        public DateTime SaveFileDateTime { get; set; }

        /// <summary>
        /// Planetary Systems not visited yet.
        /// </summary>
        public string[] PlanetarySystemIds { get; set; } = new string[0];

        /// <summary>
        /// The Planetary System currently being visited.
        /// </summary>
        public SaveFile_PlanetarySystem CurrentPlanetarySystem { get; set; }

    }

    [Serializable]
    class SaveFile_PlanetarySystem
    {

        public string            Id      { get; private set; }
        public SaveFile_Planet[] Planets { get; private set; }

        public SaveFile_PlanetarySystem()
        {

        }

        public PlanetarySystemData GetPlanetarySystemData()
        {
            return DataController.LoadData<PlanetarySystemData>(Id, DataFileType.PlanetarySystems);
        }

    }

    [Serializable]
    struct SaveFile_Planet
    {

        public string Id      { get; private set; }
        public bool   Visited { get; private set; }

        public SaveFile_Planet(string id, bool visited)
        {
            this.Id      = id;
            this.Visited = visited;
        }

    }

}
