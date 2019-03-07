using Galaxies.Core;
using Galaxies.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.Entities
{

    class PlayerShip : ShipEntity
    {

        #region Constants

        public const int FIRE_ENERGY_COST = 25;

        #endregion

        #region Properties

        /// <summary>
        /// Is this PlayerShip unlocked yet?
        /// </summary>
        public bool Unlocked { get; set; }

        /// <summary>
        /// What does this PlayerShip cost (GG)?
        /// </summary>
        public int Price { get; private set; }

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

        public PlayerShip(Transform transform, Texture2D sprite, Vector2 speed, ShipStats baseStats, int price) : base(transform, sprite, speed, baseStats)
        {
            this.Inventory = new Inventory(this);

            this.Price = price;
        }

        public override void TakeEnergy()
        {
            Energy -= FIRE_ENERGY_COST;
        }

    }

}
