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

        Texture2D sprite;
        Vector2   position;
        Vector2   origin;
        float     rotation = 0;
        Color     color    = Color.White;

        /// <summary>
        /// Rectangle draw width and height.
        /// </summary>
        Vector2 drawSize;

        #endregion

        #region Properties

        public Vector2 Position
        {
            get
            {
                return position;
            }

            set
            {
                position = value;

                PositionChanged();
            }
        }

        public float Rotation
        {
            get
            {
                return rotation;
            }

            set
            {
                rotation = value;
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

        public int Width
        {
            get
            {
                return (int)drawSize.X;
            }
        }

        public int Height
        {
            get
            {
                return (int)drawSize.Y;
            }
        }

        public Vector2 Size
        {
            get
            {
                return drawSize;
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

        public GameObject(Texture2D sprite, Vector2 position, float rotation, Color color, Vector2 size)
        {
            this.sprite   = sprite;
            this.position = position;
            this.rotation = rotation;
            this.color    = color;

            this.drawSize = size;

            if (sprite != null)
            {
                origin = new Vector2(sprite.Width / 2f, sprite.Height / 2f);
            }
        }

        public void SetDrawWidth(int width)
        {
            this.drawSize = new Vector2(width, drawSize.Y);

            SizeChanged();
        }

        public void SetDrawHeight(int height)
        {
            this.drawSize = new Vector2(drawSize.X, height);

            SizeChanged();
        }

        public void SetDrawSize(int width, int height)
        {
            this.drawSize = new Vector2(width, height);

            SizeChanged();
        }

        protected virtual void PositionChanged()
        {
            //Do nothing here
            //We'll leave this method blank (and not modified as abstract).
            //This is because we don't want to override this method in ALL classes further down the line.
            //It looks ugly but it's the best we can do.
        }

        protected virtual void SizeChanged()
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
                spriteBatch.Draw(sprite, new Rectangle((int)position.X, (int)position.Y, (int)drawSize.X, (int)drawSize.Y), null, color, rotation, Origin, SpriteEffects.None, 0f);
            }
        }

        public bool Intersect(GameObject other)
        {
            Rectangle thisRect  = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
            Rectangle otherRect = new Rectangle((int)other.Position.X, (int)other.Position.Y, (int)other.Size.X, (int)other.Size.Y);

            return thisRect.Intersects(otherRect);
        }

    }

}
