using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.Entities
{

    class PlayerShip : ShipEntity
    {

        public PlayerShip(Texture2D sprite, Vector2 position, float rotation, Color color, Vector2 speed, ShipStats baseStats) : base(sprite, position, rotation, color, speed, baseStats)
        {

        }

        public override void DoDamage(ShipEntity entity)
        {
            base.DoDamage(entity);
        }

        public override void TakeDamage(int dmg)
        {
            base.TakeDamage(dmg);
        }

    }

}
