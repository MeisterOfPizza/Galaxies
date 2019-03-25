using Galaxies.Controllers;
using Galaxies.Core;
using Galaxies.Extensions;
using Galaxies.UIControllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading.Tasks;

namespace Galaxies
{

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Game
    {

        SpriteBatch spriteBatch;

        public static MainGame Singleton { get; private set; }

        public GraphicsDeviceManager Graphics { get; private set; }

        private static Color backgroundColor = new Color(18, 18, 18);

        public MainGame()
        {
            Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            Graphics.PreferredBackBufferWidth  = 1920;
            Graphics.PreferredBackBufferHeight = 1080;

            IsMouseVisible = true;

            Singleton = this;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content. Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            System.Console.WriteLine("init");
            
            DataController.Initialize();
            ContentHelper.Initialize();
            SaveFileController.Initialize();
            GameTipsController.Initialize();
            AudioController.Initialize();

            ShipyardController.Initialize();
            GalaxyController.Initialize();

            AudioController.PlayBackgroundSong("void");

            GameUIController.Initialize();
            //GameUIController.CreateMenuScreen();

            GameUIController.CreateLoadingScreen();

            Task.Run(() => Load());

            /*
            //TEST:
            ContentHelper.GetSprites("Sprites/Backgrounds/Animated/space-background-1");
            //ContentHelper.GetSprites("Sprites/Backgrounds/Animated/space-background-2");
            ContentHelper.GetSprites("Sprites/Backgrounds/Animated/citadel-background-1");
            ContentHelper.GetSprites("Sprites/Backgrounds/Animated/citadel-background-2");
            */

            //GameUIController.CreateMenuScreen();
        }

        private async void Load()
        {   
            await Task.WhenAll(
                Task.Run(() => ContentHelper.GetSprites("Sprites/Backgrounds/Animated/citadel-background-1")),
                Task.Run(() => ContentHelper.GetSprites("Sprites/Backgrounds/Animated/citadel-background-1")),
                Task.Run(() => ContentHelper.GetSprites("Sprites/Backgrounds/Animated/citadel-background-2"))
                );

            GameUIController.CreateMenuScreen();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            System.Console.WriteLine("load");
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            GameController.Update(gameTime);
            GameUIController.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColor);

            spriteBatch.Begin();
            GameController.Draw(spriteBatch);
            GameUIController.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }

}
