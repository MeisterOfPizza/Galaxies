﻿using Galaxies.Controllers;
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

        public GraphicsDeviceManager Graphics { get; private set; }

        public static MainGame Singleton { get; private set; }

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
            
            DataController.Initialize();
            SaveFileController.Initialize();
            GameTipsController.Initialize();
            AudioController.Initialize();

            ShipyardController.Initialize();
            GalaxyController.Initialize();

            AudioController.PlayBackgroundSong("void");

            GameUIController.Initialize();
            GameUIController.CreateLoadingScreen();

            LoadGame();
        }

        private async void LoadGame()
        {
            await Task.Run(() => ContentHelper.Initialize());

            GameUIController.CreateMenuScreen();
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
            GraphicsDevice.Clear(Color.Purple);

            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
            GameUIController.Draw(spriteBatch);
            GameController.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }

}
