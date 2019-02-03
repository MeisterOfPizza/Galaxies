using System.Xml.Serialization;

namespace Galaxies.Entities
{

    [XmlRoot(Namespace = "", ElementName = "ShipStats")]
    struct ShipStats
    {

        [XmlElement("Health", typeof(int), IsNullable = false)]
        public int Health { get; set; }

        [XmlElement("Armor", typeof(int), IsNullable = false)]
        public int Armor { get; set; }

        [XmlElement("Damage", typeof(int), IsNullable = false)]
        public int Damage { get; set; }

        public ShipStats(int health, int armor, int damage)
        {
            this.Health = health;
            this.Armor  = armor;
            this.Damage = damage;
        }

    }

}
