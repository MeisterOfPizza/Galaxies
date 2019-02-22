using Galaxies.Controllers;
using Galaxies.Datas.Helpers;
using Galaxies.Datas.Items;
using Galaxies.Space.Events;
using System.Xml.Serialization;

namespace Galaxies.Datas.Space
{

    [XmlRoot(Namespace = "", ElementName = "ItemDrop")]
    public class ItemDropPlanetEventData : PlanetEventData
    {

        [XmlArray("ItemPointers", IsNullable = false)]
        [XmlArrayItem("ItemPointer", typeof(DataPointer), IsNullable = false)]
        public DataPointer[] ItemPointers { get; set; }

        public override PlanetEvent CreateEvent()
        {
            return new ItemDropPlanetEvent(this);
        }

        public override Data[] GetDataFromIds()
        {
            Data[] dataObjs = new Data[ItemPointers.Length]; //We're assuming that every pointer does point to an existing data object.

            for (int i = 0; i < ItemPointers.Length; i++)
            {
                if (ItemPointers[i].Type == DataPointer.TYPE_ITEM_SHIPUPGRADE)
                {
                    dataObjs[i] = DataController.LoadData<ShipUpgradeItemData>(ItemPointers[i].Id, DataFileType.Items);
                }
                else if (ItemPointers[i].Type == DataPointer.TYPE_ITEM_STARCHART)
                {
                    dataObjs[i] = DataController.LoadData<StarChartItemData>(ItemPointers[i].Id, DataFileType.Items);
                }
            }

            return dataObjs;
        }

    }

}
