using Galaxies.Core;
using Galaxies.Datas.Items;
using Galaxies.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

//Disable "[...] is never assigned to [...]" warnings.
#pragma warning disable 0649

namespace Galaxies.Entities
{

    abstract class ShipEntity : MovingObject, IInventoryOwner
    {

        #region Fields

        ShipStats baseStats;
        ShipStats modifiedStats;
        int       currentHealth;
        int       currentEnergy;
        bool      isAlive = true;

        #endregion

        #region Properties

        public int Health
        {
            get
            {
                return currentHealth;
            }

            private set
            {
                currentHealth = value;
            }
        }

        public int MaxHealth
        {
            get
            {
                return baseStats.Health + modifiedStats.Health;
            }
        }

        public int Shield
        {
            get
            {
                return baseStats.Shield + modifiedStats.Shield;
            }
        }

        public int Damage
        {
            get
            {
                return baseStats.Damage + modifiedStats.Damage;
            }
        }

        public int Energy
        {
            get
            {
                return currentEnergy;
            }

            private set
            {
                currentEnergy = value;
            }
        }

        public int MaxEnergy
        {
            get
            {
                return baseStats.Energy + modifiedStats.Energy;
            }
        }

        public int EnergyRegen
        {
            get
            {
                return baseStats.EnergyRegen + modifiedStats.EnergyRegen;
            }
        }

        public bool IsAlive
        {
            get
            {
                return isAlive;
            }
        }

        #endregion

        #region IInventoryOwner

        public Inventory Inventory { get; set; }

        #endregion

        public ShipEntity(Texture2D sprite, Vector2 position, float rotation, Color color, Vector2 size, Vector2 speed, ShipStats baseStats) : base(sprite, position, rotation, color, size, speed)
        {
            this.baseStats     = baseStats;
            this.modifiedStats = new ShipStats();
        }

        protected void CalculateStats()
        {
            var shipUpgradeShipStats = Inventory.Items
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
        }

        public virtual void Attack(ShipEntity defender)
        {
            defender.Defend(Damage);
        }

        public virtual void Defend(int damage)
        {
            Health -= damage;

            if (Health <= 0)
            {
                Die();
            }
        }

        public virtual void RegenEnergy()
        {
            Energy = MathHelper.Clamp(Energy + EnergyRegen, 0, MaxEnergy);
        }

        public void Die()
        {
            Health  = 0;
            isAlive = false;
        }

        #region IInventory

        public void InventoryChanged(Item changedItem)
        {
            //We only care to update stats if the changed item was of type ShipUpgrade.
            if (changedItem.Data.ItemType == ItemType.ShipUpgrade)
            {
                CalculateStats();
            }
        }

        #endregion

    }

}
