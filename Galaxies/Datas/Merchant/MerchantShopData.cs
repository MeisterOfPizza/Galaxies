using Galaxies.Controllers;
using Galaxies.Datas.Helpers;
using Galaxies.Datas.Items;
using Galaxies.Items;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Galaxies.Datas.Merchant
{

    [XmlRoot(Namespace = "", ElementName = "MerchantShop")]
    public class MerchantShopData : Data
    {

        [XmlArray("Pointers", IsNullable = true)]
        [XmlArrayItem("Pointer", typeof(DataPointer), IsNullable = true)]
        public DataPointer[] ItemPointers { get; set; }

        public Item[] GetItems(Inventory inventory)
        {
            //Create a list, as we don't know how many items exist
            List<Item> items = new List<Item>();

            foreach (DataPointer pointer in ItemPointers)
            {
                if (pointer.Type == DataPointer.ITEM_TYPE_STARCHART)
                {
                    items.Add(new StarChart(DataController.LoadData<StarChartItemData>(pointer.Id, DataFileType.Items), inventory));
                }
                else if (pointer.Type == DataPointer.ITEM_TYPE_SHIPUPGRADE)
                {
                    items.Add(new ShipUpgrade(DataController.LoadData<ShipUpgradeItemData>(pointer.Id, DataFileType.Items), inventory));
                }
            }

            return items.ToArray();
        }

    }

}
