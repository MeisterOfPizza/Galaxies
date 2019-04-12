using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace Galaxies.Extensions
{

    //TODO: Remove if not used:
    static class StreamContentLoader
    {

        private static BlendState blendColor;
        private static BlendState blendAlpha;

        static StreamContentLoader()
        {
            //Multiply each color by the source alpha, and write in just the color values into the final texture
            blendColor = new BlendState();
            blendColor.ColorWriteChannels = ColorWriteChannels.Red | ColorWriteChannels.Green | ColorWriteChannels.Blue;

            blendColor.AlphaDestinationBlend = Blend.Zero;
            blendColor.ColorDestinationBlend = Blend.Zero;

            blendColor.AlphaSourceBlend = Blend.SourceAlpha;
            blendColor.ColorSourceBlend = Blend.SourceAlpha;

            //Now copy over the alpha values from the PNG source texture to the final one, without multiplying them
            blendAlpha = new BlendState();
            blendAlpha.ColorWriteChannels = ColorWriteChannels.Alpha;

            blendAlpha.AlphaDestinationBlend = Blend.Zero;
            blendAlpha.ColorDestinationBlend = Blend.Zero;

            blendAlpha.AlphaSourceBlend = Blend.One;
            blendAlpha.ColorSourceBlend = Blend.One;
        }

        public static Texture2D LoadTextureStream(string path)
        {
            Texture2D file = null;
            RenderTarget2D result = null;

            using (Stream titleStream = TitleContainer.OpenStream("Content\\" + path + ".png"))
            {
                file = Texture2D.FromStream(MainGame.Singleton.GraphicsDevice, titleStream);
            }

            //Setup a render target to hold our final texture which will have premulitplied alpha values
            result = new RenderTarget2D(MainGame.Singleton.GraphicsDevice, file.Width, file.Height);

            /*
            MainGame.Singleton.GraphicsDevice.SetRenderTarget(result);
            MainGame.Singleton.GraphicsDevice.Clear(Color.Black);
            */

            /*
            SpriteBatch spriteBatch = new SpriteBatch(MainGame.Singleton.GraphicsDevice);
            spriteBatch.Begin(SpriteSortMode.Immediate, blendColor);
            spriteBatch.Draw(file, file.Bounds, Color.White);
            spriteBatch.End();
            */

            /*
            spriteBatch.Begin(SpriteSortMode.Immediate, blendAlpha);
            spriteBatch.Draw(file, file.Bounds, Color.White);
            spriteBatch.End();
            */

            //Release the GPU back to drawing to the screen
            //MainGame.Singleton.GraphicsDevice.SetRenderTarget(null);

            return result as Texture2D;
        }

    }

}
