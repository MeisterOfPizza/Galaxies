using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.Core
{

    /// <summary>
    /// Movable object.
    /// </summary>
    abstract class MovingObject : GameObject
    {

        #region Fields

        Vector2 speed;

        #endregion

        #region Properties

        public Vector2 Speed
        {
            get
            {
                return speed;
            }

            set
            {
                speed = value;
            }
        }

        #endregion

        public MovingObject(Texture2D sprite, Vector2 position, float rotation, Color color, Vector2 speed) : base(sprite, position, rotation, color)
        {
            this.speed = speed;
        }

        public virtual void Move()
        {
            Position += speed;
        }

    }

}
