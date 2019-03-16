using Galaxies.Entities;
using System.Xml.Serialization;

namespace Galaxies.Datas.Player
{

    [XmlRoot(Namespace = "", ElementName = "PlayerShipTemplate")]
    public class PlayerShipTemplateData : Data
    {

        [XmlElement("SpriteName", IsNullable = false)]
        public string SpriteName { get; set; }

        [XmlElement("BaseShipStats", typeof(ShipStats), IsNullable = false)]
        public ShipStats BaseShipStats { get; set; }

        [XmlElement("Price", typeof(int), IsNullable = false)]
        public int Price { get; set; }

    }

}
