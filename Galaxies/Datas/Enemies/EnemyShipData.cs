using Galaxies.Entities;
using System.Xml.Serialization;

namespace Galaxies.Datas.Enemies
{

    [XmlRoot(Namespace = "", ElementName = "EnemyShip")]
    class EnemyShipData : Data
    {

        [XmlElement("SpriteName", IsNullable = false)]
        public string SpriteName { get; set; }

        [XmlElement("BaseShipStats", typeof(ShipStats), IsNullable = false)]
        public ShipStats BaseShipStats { get; set; }

        [XmlArray("ShipUpgradeIds", IsNullable = true)]
        [XmlArrayItem("ShipUpgradeId", IsNullable = true)]
        public string[] ShipUpgradeIds { get; set; }

    }

}
