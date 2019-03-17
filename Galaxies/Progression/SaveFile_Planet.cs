using System;

namespace Galaxies.Progression
{

    [Serializable]
    class SaveFile_Planet
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
