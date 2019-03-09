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

        public PlayerShip(Transform transform, Texture2D sprite, Vector2 speed, ShipStats baseStats) : base(transform, sprite, speed, baseStats)
        {
            this.Inventory = new Inventory(this);
        }

        public override void TakeEnergy()
        {
            Energy -= FIRE_ENERGY_COST;
        }

    }

}
