using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.Core
{

    /// <summary>
    /// Base class for all object in scene.
    /// </summary>
    public abstract class GameObject
    {

        #region Fields

        protected Transform transform;

        Texture2D sprite;
        Vector2   origin;
        Color     color    = Color.White;

        #endregion

        #region Properties

        public Transform Transform
        {
            get
            {
                return transform;
            }
        }

        public Color Color
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
            }
        }

        public Vector2 Origin
        {
            get
            {
                return origin;
            }
        }

        protected Texture2D Sprite
        {
            get
            {
                return sprite;
            }

            set
            {
                sprite = value;

                if (value != null)
                {
                    origin = new Vector2(value.Width / 2f, value.Height / 2f);
                }
            }
        }

        public virtual bool Visable { get; set; } = true;

        #endregion

        public GameObject(Transform transform, Texture2D sprite)
        {
            this.transform = transform;
            this.transform.SetGameObject(this);

            this.sprite = sprite;

            if (sprite != null)
            {
                origin = new Vector2(sprite.Width / 2f, sprite.Height / 2f);
            }
        }

        public virtual void PositionChanged()
        {
            //Do nothing here
            //We'll leave this method blank (and not modified as abstract).
            //This is because we don't want to override this method in ALL classes further down the line.
            //It looks ugly but it's the best we can do.
        }

        public virtual void SizeChanged()
        {
            //Do nothing here
            //We'll leave this method blank (and not modified as abstract).
            //This is because we don't want to override this method in ALL classes further down the line.
            //It looks ugly but it's the best we can do.
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if (Visable && sprite != null)
            {
                spriteBatch.Draw(sprite, new Rectangle(transform.RawX, transform.RawY, transform.RawWidth, transform.RawHeight), null, color, transform.Rotation, Origin, SpriteEffects.None, 0f);
            }
        }

        public bool Intersect(GameObject other)
        {
            Rectangle thisRect  = new Rectangle((int)transform.X, (int)transform.Y, (int)transform.RawWidth, (int)transform.RawHeight);
            Rectangle otherRect = new Rectangle((int)other.Transform.X, (int)other.Transform.Y, (int)other.Transform.RawWidth, (int)other.Transform.RawHeight);

            return thisRect.Intersects(otherRect);
        }

    }

}
