using Galaxies.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.Entities
{

    class Entity : MovingObject
    {

        #region Fields

        int  health;
        int  armor;
        bool isAlive = true;

        #endregion

        #region Properties

        public int Health
        {
            get
            {
                return health;
            }

            set
            {
                health = value;
            }
        }

        public int Armor
        {
            get
            {
                return armor;
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

        public Entity(Texture2D sprite, Vector2 position, float rotation, Color color, Vector2 speed, int health, int armor) : base(sprite, position, rotation, color, speed)
        {
            this.health = health;
            this.armor  = armor;
        }

        public void TakeDamage(int dmg)
        {

        }

        public void DoDamage(Entity entity)
        {

        }

        public void Die()
        {
            health = 0;
            isAlive = false;
        }

    }

}
