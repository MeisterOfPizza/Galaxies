using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.Extensions
{

    /// <summary>
    /// Contains preloaded sprites.
    /// </summary>
    static class SpriteHelper
    {

        #region Fonts

        public static SpriteFont Arial_Font { get; private set; }

        #endregion

        #region Sprites

        #region General

        public static Texture2D Box4x4_Sprite { get; private set; }

        #endregion

        #region Combat

        public static Texture2D Bullet_Sprite { get; private set; }
        public static Texture2D Shield_Sprite { get; private set; }

        #endregion

        #endregion

        public static void Initialize(ContentManager content)
        {
            Arial_Font = content.Load<SpriteFont>("Fonts/Arial");

            Box4x4_Sprite = content.Load<Texture2D>("Sprites/Box 4x4");

            Bullet_Sprite = content.Load<Texture2D>("Sprites/Effects/Bullet");
            Shield_Sprite = content.Load<Texture2D>("Sprites/Effects/Shield");
        }

        public static Texture2D GetSprite(string spriteName)
        {
            //TODO: Implement try-catch?
            return MainGame.Singleton.Content.Load<Texture2D>(spriteName);
        }

    }

}
