using System.Xml.Serialization;

namespace Galaxies.Datas.Items
{

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

    }

}
