using System;
using System.Xml.Serialization;
using Galaxies.Items;

namespace Galaxies.Datas.Items
{

    [Obsolete("Use ItemDropPlanetEventData instead.", true)] //TODO: Remove obsolete
    [XmlRoot(Namespace = "", ElementName = "Artifact")]
    class ArtifactItemData : ItemData
    {

        [XmlElement("MinSellGG", typeof(int), IsNullable = false)]
        public int MinSellGG { get; set; }

        [XmlElement("MaxSellGG", typeof(int), IsNullable = false)]
        public int MaxSellGG { get; set; }

        public override ItemType ItemType
        {
            get
            {
                return ItemType.Artifact;
            }
        }

        public override Item CreateItem(Inventory inventory)
        {
            throw new NotImplementedException();
        }

    }

}
