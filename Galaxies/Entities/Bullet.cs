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

        public Bullet(Texture2D sprite, Vector2 position, float rotation, Color color, Vector2 size, Vector2 speed, GameObject target, EventArg onHit) : base(sprite, position, rotation, color, size, speed)
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
            else if (Position.X < 0 || Position.X > GameUIController.WindowWidth)
            {
                Destroyed = true;
                onHit.Invoke();
            }
        }

    }

}
