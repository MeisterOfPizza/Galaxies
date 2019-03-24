using Galaxies.Controllers;
using Galaxies.Extensions;
using Galaxies.UIControllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Galaxies
{

    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Game
    {

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static MainGame Singleton { get; private set; }

        private static Color backgroundColor = new Color(18, 18, 18);

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //graphics.PreferredBackBufferWidth  = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            //graphics.PreferredBackBufferHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            //graphics.ToggleFullScreen();

            graphics.PreferredBackBufferWidth  = 1920;
            graphics.PreferredBackBufferHeight = 1080;

            //graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;

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
            
            DataController.Initialize();
            ContentHelper.Initialize();
            SaveFileController.Initialize();
            GameTipsController.Initialize();

            ShipyardController.Initialize();
            GalaxyController.Initialize();

            GameUIController.Initialize();
            GameUIController.CreateMenuScreen();

            AudioController.PlayBackgroundSong("a_fly-through_of_the_proxima_centauri_system");
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
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
