using System;
using System.IO;

namespace Galaxies.Controllers
{

    static class GameTipsController
    {

        private static string[] gameTips;

        public static void Initialize()
        {
            FileStream file = File.Open("Data\\GameTips.txt", FileMode.Open);

            using (StreamReader reader = new StreamReader(file))
            {
                string text = reader.ReadToEnd();

                gameTips = text.Replace("\n", "").Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            }

            file.Dispose();
        }

        public static string RandomTip()
        {
            return gameTips[Extensions.Random.Next(gameTips.Length)];
        }

    }

}
