using System.Xml.Serialization;

namespace Galaxies.Entities
{

    [XmlRoot(Namespace = "", ElementName = "ShipStats")]
    public struct ShipStats
    {

        [XmlElement("Health", typeof(int), IsNullable = false)]
        public int Health { get; set; }

        [XmlElement("Shield", typeof(int), IsNullable = false)]
        public int Shield { get; set; }

        [XmlElement("Damage", typeof(int), IsNullable = false)]
        public int Damage { get; set; }

        [XmlElement("Energy", typeof(int), IsNullable = false)]
        public int Energy { get; set; }

        [XmlElement("EnergyRegen", typeof(int), IsNullable = false)]
        public int EnergyRegen { get; set; }

        public ShipStats(int health, int shield, int damage, int energy, int energyRegen)
        {
            this.Health      = health;
            this.Shield      = shield;
            this.Damage      = damage;
            this.Energy      = energy;
            this.EnergyRegen = energyRegen;
        }

    }

}
