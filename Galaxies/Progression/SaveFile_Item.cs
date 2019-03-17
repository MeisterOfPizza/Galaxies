using Galaxies.Controllers;
using Galaxies.Datas;
using Galaxies.Datas.Items;
using Galaxies.Items;
using System;

namespace Galaxies.Progression
{

    [Serializable]
    class SaveFile_Item
    {

        public string   ItemId   { get; private set; }
        public ItemType ItemType { get; private set; }

        public SaveFile_Item(Item item)
        {
            this.ItemId   = item.Data.Id;
            this.ItemType = item.Data.ItemType;
        }

        public Item GetItem(Inventory inventory)
        {
            if (ItemType == ItemType.ShipUpgrade)
            {
                return new ShipUpgrade(DataController.LoadData<ShipUpgradeItemData>(ItemId, DataFileType.Items), inventory);
            }
            else if (ItemType == ItemType.StarChart)
            {
                return new StarChart(DataController.LoadData<StarChartItemData>(ItemId, DataFileType.Items), inventory);
            }
            else
            {
                return default(Item);
            }
        }

    }

}
