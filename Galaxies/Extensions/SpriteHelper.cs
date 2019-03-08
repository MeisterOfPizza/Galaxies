using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.IO;
using System.Windows.Forms;

namespace Galaxies.Extensions
{

    /// <summary>
    /// Contains preloaded sprites.
    /// </summary>
    static class SpriteHelper
    {

        #region Helpers

        private static string ContentDirectoryPath = Path.GetDirectoryName(Application.ExecutablePath) + "/Content";

        #endregion

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

        #region Animations

        #region Backgrounds

        public static GIF Citadel_Background_Animation { get; private set; }

        #endregion

        #endregion

        public static void Initialize(ContentManager content)
        {
            Arial_Font = content.Load<SpriteFont>("Fonts/Arial");

            Box4x4_Sprite = content.Load<Texture2D>("Sprites/Box 4x4");

            Bullet_Sprite = content.Load<Texture2D>("Sprites/Effects/Bullet");
            Shield_Sprite = content.Load<Texture2D>("Sprites/Effects/Shield");

            Citadel_Background_Animation = new GIF("Sprites/Backgrounds/Animated/space-background-1", 0.08);
        }

        public static Texture2D GetSprite(string spriteName)
        {
            //TODO: Implement try-catch?
            return MainGame.Singleton.Content.Load<Texture2D>(spriteName);
        }

        public static Texture2D[] GetSprites(string path)
        {
            ContentManager content = MainGame.Singleton.Content; //Create a new reference for quicker access.

            DirectoryInfo dir = new DirectoryInfo(ContentDirectoryPath + "/" + path);

            if (!dir.Exists) //The directory did not exist, return an empty array.
            {
                return new Texture2D[0];
            }

            FileInfo[] files = dir.GetFiles("*.xnb"); //Search for all files with the file extension .xnb (monogame files).

            Texture2D[] result = new Texture2D[files.Length];

            for (int i = 0; i < files.Length; i++)
            {
                string name = Path.GetFileNameWithoutExtension(files[i].Name); //Get the file name alone.

                result[i] = content.Load<Texture2D>(path + "/" + name);
            }

            return result;
        }

    }

}
