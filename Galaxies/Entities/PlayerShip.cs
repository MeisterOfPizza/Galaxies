using Galaxies.Controllers;
using Galaxies.Core;
using Galaxies.Datas.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace Galaxies.Entities
{

    class PlayerShip : ShipEntity
    {

        #region Constants

        public const int FIRE_ENERGY_COST = 25;

        #endregion

        #region Properties

        public ShipStats BaseStats
        {
            get
            {
                return baseStats;
            }
        }

        public ShipStats ModifiedStats
        {
            get
            {
                return modifiedStats;
            }
        }

        #endregion

        public PlayerShip(Transform transform, Texture2D sprite, ShipStats baseStats) : base(transform, sprite, baseStats)
        {

        }

        public override void TakeEnergy()
        {
            Energy -= FIRE_ENERGY_COST;
        }

        public void CalculateStats()
        {
            var shipUpgradeShipStats = PlayerController.Player.Inventory.Items
                .Where(i => i.Data.ItemType == ItemType.ShipUpgrade)
                .Select(i => ((ShipUpgradeItemData)i.Data).ShipStats);

            int newHealth      = 0;
            int newShield      = 0;
            int newDamage      = 0;
            int newEnergy      = 0;
            int newEnergyRegen = 0;

            foreach (ShipStats stats in shipUpgradeShipStats)
            {
                newHealth      += stats.Health;
                newShield      += stats.Shield;
                newDamage      += stats.Damage;
                newEnergy      += stats.Energy;
                newEnergyRegen += stats.EnergyRegen;
            }

            modifiedStats = new ShipStats(newHealth, newShield, newDamage, newEnergy, newEnergyRegen);

            ClampStats();
        }

        private void ClampStats()
        {
            if (Health > MaxHealth) Health = MaxHealth;
            if (Shield > MaxShield) Shield = MaxShield;
            if (Energy > MaxEnergy) Energy = MaxEnergy;
        }

    }

}
