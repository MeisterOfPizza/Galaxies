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

        public MovingObject(Transform transform, Texture2D sprite, Vector2 speed) : base(transform, sprite)
        {
            this.speed = speed;
        }

        public virtual void Move()
        {
            transform.Position += speed;
        }

    }

}
