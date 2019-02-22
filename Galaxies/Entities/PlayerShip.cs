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

        public static PlayerShip Singleton { get; private set; }

        public PlayerShip(Texture2D sprite, Vector2 position, float rotation, Color color, Vector2 size, Vector2 speed, ShipStats baseStats) : base(sprite, position, rotation, color, size, speed, baseStats)
        {
            this.Inventory = new Inventory(this);

            Singleton = this;
        }

        public override void TakeEnergy()
        {
            Energy -= FIRE_ENERGY_COST;
        }

        public static void CreateNewPlayer()
        {
            var content = MainGame.Singleton.Content;

            Singleton = new PlayerShip(
                content.Load<Texture2D>("Sprites/Player Ships/Player Ship 1"), 
                Vector2.Zero, 
                0, 
                Color.White, 
                new Vector2(100), 
                Vector2.Zero, 
                new ShipStats(100, 100, 10, 1000, 50));
        }

    }

}
