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

        #endregion

        #region Properties

        public Texture2D Sprite
        {
            get
            {
                return sprite;
            }
        }

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

        #endregion

        public GameObject(Texture2D sprite, Vector2 position, float rotation, Color color)
        {
            this.sprite   = sprite;
            this.position = position;
            this.rotation = rotation;
            this.color    = color;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, new Rectangle((int)position.X, (int)position.Y, sprite.Width, sprite.Height), null, color, rotation, position, SpriteEffects.None, 0f);
        }

    }

}
