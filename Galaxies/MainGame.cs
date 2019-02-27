using Galaxies.Controllers;
using Galaxies.Extensions;
using Galaxies.Progression;
using Galaxies.UIControllers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            graphics.PreferredBackBufferHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            //graphics.ToggleFullScreen();

            //graphics.PreferredDepthStencilFormat = DepthFormat.Depth24Stencil8;

            IsMouseVisible = true;

            Singleton = this;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();

            DataController.Initialize();
            SpriteHelper.Initialize(Content);

            //TEST: Adding a planetary system
            GalaxyController.Visitables.Add(new Space.PlanetarySystem(DataController.LoadData<Datas.Space.PlanetarySystemData>("test", DataFileType.PlanetarySystems)));

            GameUIController.Window = Window;
            GameUIController.CreateGalaxyScreen();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            GameController.LoadGame(new SaveFile());
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

            // TODO: Add your update logic here

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
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            spriteBatch.Begin();
            GameController.Draw(spriteBatch);
            GameUIController.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

    }

}
