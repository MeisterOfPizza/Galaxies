using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.Core
{

    /// <summary>
    /// Base class for all object in scene.
    /// </summary>
    abstract class GameObject
    {

        #region Fields

        protected Texture2D sprite;
        Vector2 position;
        Vector2 origin;
        float   rotation = 0;
        Color   color    = Color.White;

        /// <summary>
        /// Rectangle draw width.
        /// </summary>
        int drawWidth;

        /// <summary>
        /// Rectangle draw height.
        /// </summary>
        int drawHeight;

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
                return drawWidth;
            }
        }

        public int Height
        {
            get
            {
                return drawHeight;
            }
        }

        public virtual bool Visable { get; set; } = true;

        #endregion

        public GameObject(Texture2D sprite, Vector2 position, float rotation, Color color)
        {
            this.sprite   = sprite;
            this.position = position;
            this.rotation = rotation;
            this.color    = color;

            if (sprite != null)
            {
                this.drawWidth  = sprite.Width;
                this.drawHeight = sprite.Height;
            }

            origin = new Vector2(Width / 2f, Height / 2f);
        }

        public void SetDrawWidth(int width)
        {
            this.drawWidth = width;

            origin = new Vector2(Width / 2f, Height / 2f);
        }

        public void SetDrawHeight(int height)
        {
            this.drawHeight = height;

            origin = new Vector2(Width / 2f, Height / 2f);
        }

        public void SetDrawSize(int width, int height)
        {
            this.drawWidth  = width;
            this.drawHeight = height;

            origin = new Vector2(Width / 2f, Height / 2f);
        }

        protected virtual void PositionChanged()
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
                spriteBatch.Draw(sprite, new Rectangle((int)position.X, (int)position.Y, drawWidth, drawHeight), null, color, rotation, origin, SpriteEffects.None, 0f);
            }
        }

    }

}
