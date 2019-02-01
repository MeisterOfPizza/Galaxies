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

        Texture2D sprite;
        Vector2   position;
        float     rotation = 0;
        Color     color    = Color.White;

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

        #endregion

        public GameObject(Texture2D sprite, Vector2 position, float rotation, Color color)
        {
            this.sprite   = sprite;
            this.position = position;
            this.rotation = rotation;
            this.color    = color;

            this.drawWidth  = sprite.Width;
            this.drawHeight = sprite.Height;
        }

        public void SetDrawWidth(int width)
        {
            this.drawWidth = width;
        }

        public void SetDrawHeight(int height)
        {
            this.drawHeight = height;
        }

        public void SetDrawSize(int width, int height)
        {
            this.drawWidth  = width;
            this.drawHeight = height;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, new Rectangle((int)position.X, (int)position.Y, drawWidth, drawHeight), null, color, rotation, Vector2.Zero, SpriteEffects.None, 0f);
        }

    }

}
