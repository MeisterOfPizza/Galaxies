using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.Entities
{

    class EnemyShip : ShipEntity
    {

        public EnemyShip(Texture2D sprite, Vector2 position, float rotation, Color color, Vector2 speed, ShipStats baseStats) : base(sprite, position, rotation, color, speed, baseStats)
        {

        }

    }

}
