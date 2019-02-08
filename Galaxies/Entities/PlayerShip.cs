using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.Entities
{

    class PlayerShip : ShipEntity
    {

        public static PlayerShip Singleton { get; private set; }

        public PlayerShip(Texture2D sprite, Vector2 position, float rotation, Color color, Vector2 speed, ShipStats baseStats) : base(sprite, position, rotation, color, speed, baseStats)
        {
            Singleton = this;
        }

    }

}
