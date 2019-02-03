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
        bool      isAlive = true;

        #endregion

        #region Properties

        public int Health
        {
            get
            {
                return currentHealth;
            }

            set
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

        public int Armor
        {
            get
            {
                return baseStats.Armor + modifiedStats.Armor;
            }
        }

        public int Damage
        {
            get
            {
                return baseStats.Damage + modifiedStats.Damage;
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

        public ShipEntity(Texture2D sprite, Vector2 position, float rotation, Color color, Vector2 speed, ShipStats baseStats) : base(sprite, position, rotation, color, speed)
        {
            this.baseStats     = baseStats;
            this.modifiedStats = new ShipStats();
        }

        protected void CalculateStats()
        {
            var shipUpgradeShipStats = Inventory.Items
                .Where(i => i.Data.ItemType == ItemType.ShipUpgrade)
                .Select(i => ((ShipUpgradeItemData)i.Data).ShipStats);

            int newHealth = 0;
            int newArmor  = 0;
            int newDamage = 0;

            foreach (ShipStats stats in shipUpgradeShipStats)
            {
                newHealth += stats.Health;
                newArmor  += stats.Armor;
                newDamage += stats.Damage;
            }

            modifiedStats = new ShipStats(newHealth, newArmor, newDamage);
        }

        public virtual void TakeDamage(int dmg)
        {
            Health -= dmg;

            if (Health <= 0)
            {
                Die();
            }
        }

        public virtual void DoDamage(ShipEntity entity)
        {
            entity.TakeDamage(Damage);
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
