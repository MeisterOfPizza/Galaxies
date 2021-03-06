﻿using Galaxies.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

//Disable "[...] is never assigned to [...]" warnings.
#pragma warning disable 0649

namespace Galaxies.Entities
{

    public abstract class ShipEntity : GameObject
    {

        #region Fields

        protected ShipStats baseStats;
        protected ShipStats modifiedStats;

        int  currentHealth;
        int  currentShield;
        int  currentEnergy;
        bool isAlive = true;

        #endregion

        #region Properties

        public int Health
        {
            get
            {
                return currentHealth;
            }

            protected set
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
                return currentShield;
            }

            protected set
            {
                currentShield = value;
            }
        }

        public int MaxShield
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

            protected set
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

        public bool HasShieldUp { get; set; }

        public bool IsAlive
        {
            get
            {
                return isAlive;
            }
        }

        #endregion

        public ShipEntity(Transform transform, Texture2D sprite, ShipStats baseStats) : base(transform, sprite)
        {
            this.baseStats     = baseStats;
            this.modifiedStats = new ShipStats();

            this.currentHealth = MaxHealth;
            this.currentShield = MaxShield;
            this.currentEnergy = MaxEnergy;
        }

        public void RefillStats()
        {
            Health = MaxHealth;
            Shield = MaxShield;
            Energy = MaxEnergy;

            isAlive = true;
        }

        public virtual void Attack(ShipEntity defender)
        {
            defender.Defend(Damage);
        }

        public virtual void Defend(int damage)
        {
            if (HasShieldUp)
            {
                int tempDmg = damage;

                damage = MathHelper.Clamp(damage - currentShield, 0, damage);
                Shield = MathHelper.Clamp(Shield - tempDmg, 0, currentShield);
            }

            Health -= damage;

            if (Health <= 0)
            {
                Die();
            }
        }

        public abstract void TakeEnergy();

        public virtual void RegenEnergy()
        {
            Energy = MathHelper.Clamp(Energy + EnergyRegen, 0, MaxEnergy);
        }

        public virtual void Die()
        {
            Health  = 0;
            isAlive = false;
        }

    }

}
