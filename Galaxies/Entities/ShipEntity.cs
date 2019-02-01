using Galaxies.Core;
using Galaxies.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

//Disable "[...] is never assigned to [...]" warnings.
#pragma warning disable 0649

namespace Galaxies.Entities
{

    abstract class ShipEntity : MovingObject
    {

        #region Fields

        int  baseHealth;
        int  modifiedHealth;
        int  currentHealth;
        int  baseArmor;
        int  modifiedArmor;
        int  baseDamage;
        int  modifiedDamage;
        bool isAlive = true;
        List<ShipUpgrade> upgrades;

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
                return baseHealth + modifiedHealth;
            }
        }

        public int Armor
        {
            get
            {
                return baseArmor + modifiedArmor;
            }
        }

        public int Damage
        {
            get
            {
                return baseDamage + modifiedDamage;
            }
        }

        public bool IsAlive
        {
            get
            {
                return isAlive;
            }
        }

        public List<ShipUpgrade> Upgrades
        {
            get
            {
                return upgrades;
            }
        }

        #endregion

        public ShipEntity(Texture2D sprite, Vector2 position, float rotation, Color color, Vector2 speed, int baseHealth, int baseArmor, int baseDamage) : base(sprite, position, rotation, color, speed)
        {
            this.baseHealth = baseHealth;
            this.baseArmor  = baseArmor;
            this.baseDamage = baseDamage;
        }

        public void Equip(ShipUpgrade shipUpgrade)
        {
            upgrades.Add(shipUpgrade);

            CalculateStats();
        }

        private void CalculateStats()
        {

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

    }

}
