using Galaxies.Datas.Helpers;
using Galaxies.Entities;
using Microsoft.Xna.Framework;
using System.Xml.Serialization;

namespace Galaxies.Datas.Enemies
{

    [XmlRoot(Namespace = "", ElementName = "EnemyShip")]
    public class EnemyShipData : Data
    {

        [XmlElement("SpriteName", IsNullable = true)]
        public string SpriteName { get; set; }

        [XmlElement("BaseShipStats", typeof(ShipStats), IsNullable = false)]
        public ShipStats BaseShipStats { get; set; }

        [XmlArray("ShipUpgradeIds", IsNullable = true)]
        [XmlArrayItem("ShipUpgradeId", IsNullable = true)]
        public string[] ShipUpgradeIds { get; set; }

        [XmlElement("Color", typeof(ColorData), IsNullable = true)]
        public ColorData Color { get; set; } = new ColorData();

        [XmlElement("IsFinalBoss", typeof(bool), IsNullable = false)]
        public bool IsFinalBoss { get; set; }

        public EnemyShip GetEnemyShip(Vector2 size)
        {
            if (IsFinalBoss)
            {
                return new FinalBossShip(this, size);
            }
            else
            {
                return new EnemyShip(this, size);
            }
        }

    }

}
