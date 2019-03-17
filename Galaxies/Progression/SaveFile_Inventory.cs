using Galaxies.Items;
using System;

namespace Galaxies.Progression
{

    [Serializable]
    class SaveFile_Inventory
    {

        public SaveFile_Item[] Items { get; private set; }

        public SaveFile_Inventory(Inventory inventory)
        {
            Items = new SaveFile_Item[inventory.Items.Count];

            for (int i = 0; i < Items.Length; i++)
            {
                Items[i] = new SaveFile_Item(inventory.Items[i]);
            }
        }

    }

}
