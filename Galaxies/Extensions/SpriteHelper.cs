using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.Extensions
{

    /// <summary>
    /// Contains preloaded sprites.
    /// </summary>
    static class SpriteHelper
    {

        public static Texture2D BulletSprite;

        public static void Initialize(ContentManager content)
        {
            BulletSprite = content.Load<Texture2D>("Sprites/Effects/Bullet");
        }

        public static Texture2D GetSprite(string spriteName)
        {
            //TODO: Implement try-catch?
            return MainGame.Singleton.Content.Load<Texture2D>(spriteName);
        }

    }

}
