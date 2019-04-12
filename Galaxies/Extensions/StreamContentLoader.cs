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

        /*
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
            /*
            return result as Texture2D;
        }
        */

        public static Texture2D LoadTextureStream(string filePath)
        {
            GraphicsDevice GraphicsDevice = MainGame.Singleton.GraphicsDevice;

            Texture2D file = null;
            Texture2D resultTexture;
            RenderTarget2D result = null;

            try
            {
                using (System.IO.Stream titleStream = TitleContainer.OpenStream(filePath))
                {
                    file = Texture2D.FromStream(GraphicsDevice, titleStream);
                }
            }
            catch
            {
                throw new System.IO.FileLoadException("Cannot load '" + filePath + "' file!");
            }

            PresentationParameters pp = GraphicsDevice.PresentationParameters;
            //Setup a render target to hold our final texture which will have premulitplied alpha values
            result = new RenderTarget2D(GraphicsDevice, file.Width, file.Height, true, pp.BackBufferFormat, pp.DepthStencilFormat);

            GraphicsDevice.SetRenderTarget(result);
            GraphicsDevice.Clear(Color.Black);

            SpriteBatch spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteBatch.Begin(SpriteSortMode.Immediate, blendColor);
            spriteBatch.Draw(file, file.Bounds, Color.White);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Immediate, blendAlpha);
            spriteBatch.Draw(file, file.Bounds, Color.White);
            spriteBatch.End();

            //Release the GPU back to drawing to the screen
            GraphicsDevice.SetRenderTarget(null);

            resultTexture = new Texture2D(GraphicsDevice, result.Width, result.Height);
            Color[] data = new Color[result.Height * result.Width];
            Color[] textureColor = new Color[result.Height * result.Width];

            result.GetData<Color>(textureColor);

            for (int i = 0; i < result.Height; i++)
            {
                for (int j = 0; j < result.Width; j++)
                {
                    data[j + i * result.Width] = textureColor[j + i * result.Width];
                }
            }

            resultTexture.SetData(data);

            return resultTexture;

        }

    }

}
