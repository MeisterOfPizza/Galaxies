using Galaxies.Core;
using Galaxies.UIControllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.Entities
{

    class Bullet : MovingObject
    {

        private GameObject target;
        private EventArg   onHit;

        public bool Destroyed { get; private set; }

        public Bullet(Transform transform, Texture2D sprite, Vector2 speed, GameObject target, EventArg onHit) : base(transform, sprite, speed)
        {
            this.target = target;
            this.onHit  = onHit;
        }

        public override void Move()
        {
            if (Visable)
            {
                base.Move();
            }

            if (Intersect(target))
            {
                Destroyed = true;
                onHit.Invoke();
            }
            else if (transform.Position.X < 0 || transform.Position.X > GameUIController.WindowWidth)
            {
                Destroyed = true;
                onHit.Invoke();
            }
        }

    }

}
