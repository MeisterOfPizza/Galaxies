using Galaxies.Debug;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System.IO;
using System.Windows.Forms;

namespace Galaxies.Extensions
{

    /// <summary>
    /// Contains preloaded fonts, sprites, songs and sound effects.
    /// </summary>
    static class ContentHelper
    {

        #region Helpers

        private static string ContentDirectoryPath = Path.GetDirectoryName(Application.ExecutablePath) + "/Content";

        #endregion

        #region Fonts

        public static SpriteFont Arial_Font  { get; private set; }

        #endregion

        #region Sprites

        #region General

        public static Texture2D Missing_Sprite { get; private set; }
        public static Texture2D Box4x4_Sprite  { get; private set; }

        #endregion

        #region Combat

        public static Texture2D Bullet_Sprite { get; private set; }
        public static Texture2D Shield_Sprite { get; private set; }

        #endregion

        #endregion

        #region Animations

        #region Backgrounds

        public static GIF Space_Background_Animation_1 { get { return new GIF("Sprites/Backgrounds/Animated/space-background-1", 0.08); } }
        public static GIF Space_Background_Animation_2 { get { return new GIF("Sprites/Backgrounds/Animated/space-background-2", 0.03); } }

        public static GIF Citadel_Background_Animation_1 { get { return new GIF("Sprites/Backgrounds/Animated/citadel-background-1", 0.05); } }
        public static GIF Citadel_Background_Animation_2 { get { return new GIF("Sprites/Backgrounds/Animated/citadel-background-2", 0.10); } }

        #endregion

        #endregion

        public static void Initialize()
        {
            Arial_Font = GetFont("Fonts/arial");

            Missing_Sprite = GetSprite("Sprites/missing");
            Box4x4_Sprite  = GetSprite("Sprites/box-4x4");

            Bullet_Sprite = GetSprite("Sprites/Effects/bullet");
            Shield_Sprite = GetSprite("Sprites/Effects/shield");

            //Preload GIFs:
            GetSprites("Sprites/Backgrounds/Animated/space-background-1");
            GetSprites("Sprites/Backgrounds/Animated/space-background-2");
            GetSprites("Sprites/Backgrounds/Animated/citadel-background-1");
            GetSprites("Sprites/Backgrounds/Animated/citadel-background-2");
        }

        public static Texture2D GetSprite(string path)
        {
            Texture2D loaded = Missing_Sprite;

            try
            {
                loaded = MainGame.Singleton.Content.Load<Texture2D>(path);
            }
            catch (ContentLoadException e)
            {
                CrashHandler.ShowException(e);
            }

            return loaded;
        }

        public static SpriteFont GetFont(string path)
        {
            SpriteFont loaded = null;

            try
            {
                loaded = MainGame.Singleton.Content.Load<SpriteFont>(path);
            }
            catch (ContentLoadException e)
            {
                CrashHandler.ShowException(e);
            }

            return loaded;
        }

        public static Song GetSong(string path)
        {
            Song loaded = null;

            try
            {
                loaded = MainGame.Singleton.Content.Load<Song>(path);
            }
            catch (ContentLoadException e)
            {
                CrashHandler.ShowException(e);
            }

            return loaded;
        }

        public static SoundEffect GetSoundEffect(string path)
        {
            SoundEffect loaded = null;

            try
            {
                loaded = MainGame.Singleton.Content.Load<SoundEffect>(path);
            }
            catch (ContentLoadException e)
            {
                CrashHandler.ShowException(e);
            }

            return loaded;
        }

        /*
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
        */

        public static Texture2D[] GetSprites(string path)
        {
            ContentManager content = MainGame.Singleton.Content; //Create a new reference for quicker access.

            DirectoryInfo dir = new DirectoryInfo(ContentDirectoryPath + "/" + path);

            if (!dir.Exists) //The directory did not exist, return an empty array.
            {
                return new Texture2D[0];
            }

            FileInfo[] files = dir.GetFiles("*.png"); //Search for all files with the file extension .xnb (monogame files).

            Texture2D[] result = new Texture2D[files.Length];

            for (int i = 0; i < files.Length; i++)
            {
                string name = Path.GetFileNameWithoutExtension(files[i].Name); //Get the file name alone.

                result[i] = StreamContentLoader.LoadTextureStream(path + "/" + name);
            }

            return result;
        }

    }

}
