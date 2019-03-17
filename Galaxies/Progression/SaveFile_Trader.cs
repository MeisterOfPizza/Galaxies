using Galaxies.Controllers;
using System;

namespace Galaxies.Progression
{

    [Serializable]
    class SaveFile_Trader
    {

        public int                PlayerBalance   { get; private set; }
        public SaveFile_Inventory PlayerInventory { get; private set; }

        public SaveFile_Trader()
        {
            PlayerBalance   = PlayerController.Player.Balance.GalacticGold;
            PlayerInventory = new SaveFile_Inventory(PlayerController.Player.Inventory);
        }

    }

}
