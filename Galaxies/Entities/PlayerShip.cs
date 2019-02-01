using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.Entities
{

    class PlayerShip : ShipEntity
    {

        public PlayerShip(Texture2D sprite, Vector2 position, float rotation, Color color, Vector2 speed, int baseHealth, int baseArmor, int baseDamage) : base(sprite, position, rotation, color, speed, baseHealth, baseArmor, baseDamage)
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
