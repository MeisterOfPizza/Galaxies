using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Galaxies.Extensions
{

    class GIF
    {

        /// <summary>
        /// General bounds of the GIF.
        /// </summary>
        public Vector2 Bounds { get; private set; }

        public double GIFSpeed { get; set; }

        public Texture2D[] Images { get; private set; }

        public Texture2D Current { get; private set; }

        private int currentIndex;

        /// <summary>
        /// Creates a new GIF.
        /// </summary>
        /// <param name="path">Path to the directory containing all the <see cref="Texture2D"/>s.</param>
        /// <param name="gifSpeed">How fast the gif should play. The interval between each image update.</param>
        public GIF(string path, double gifSpeed)
        {
            this.Images   = ContentHelper.GetSprites(path);
            this.GIFSpeed = gifSpeed;

            if (Images.Length > 0)
            {
                Bounds = Images[0].Bounds.Size.ToVector2();

                Current = Images[0];
            }
        }

        public void Next()
        {
            currentIndex++;

            if (currentIndex >= Images.Length)
            {
                currentIndex = 0;
            }

            Current = Images[currentIndex];
        }

        public void Reset()
        {
            if (Images.Length > 0) Current = Images[0];
            currentIndex = 0;
        }

    }

}
