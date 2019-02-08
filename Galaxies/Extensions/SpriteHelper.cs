using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.Extensions
{

    static class SpriteHelper
    {

        public static Texture2D GetSprite(string spriteName)
        {
            //TODO: Implement try-catch?
            return MainGame.Singleton.Content.Load<Texture2D>(spriteName);
        }

    }

}
