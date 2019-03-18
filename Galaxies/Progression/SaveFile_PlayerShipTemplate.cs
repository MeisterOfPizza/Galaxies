using System;

namespace Galaxies.Progression
{

    [Serializable]
    class SaveFile_PlayerShipTemplate
    {

        public string Id       { get; private set; }
        public bool   Unlocked { get; private set; }

        public SaveFile_PlayerShipTemplate(string id, bool unlocked)
        {
            this.Id       = id;
            this.Unlocked = unlocked;
        }

    }

}
