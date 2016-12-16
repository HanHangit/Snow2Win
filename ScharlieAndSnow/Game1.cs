using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ScharlieAndSnow
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont font;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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

            GraphicStuff.Instance.graphicDevice = GraphicsDevice;

            graphics.PreferredBackBufferWidth = 1920;
            graphics.PreferredBackBufferHeight = 1080;

            //graphics.ToggleFullScreen();

            IsMouseVisible = true;

            graphics.ApplyChanges();
            

            


            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            font = Content.Load<SpriteFont>("FPS");

            //Initialize Map
            Texture2D[] tiles = { Content.Load<Texture2D>("SkyTile"), Content.Load<Texture2D>("SnowTile") };
            Texture2D bitMap = Content.Load<Texture2D>("SnowMap");
            Texture2D[] clouds = { Content.Load<Texture2D>("Clouds") };

            MapStuff.Instance.map = new Tilemap(tiles,clouds, bitMap, 8);


            //Initialize Camera
            GUIStuff.Instance.camera = new Camera(graphics.GraphicsDevice.Viewport);

            MapStuff.Instance.partCollHandler = new GlobalParticleHandler(Content.Load<Texture2D>("SnowBall"));

            // TODO: use this.Content to load your game content here
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

            MapStuff.Instance.map.Update(gameTime);



            MapStuff.Instance.partCollHandler.Update(gameTime);

            

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            GraphicsDevice.SetRenderTarget(null);

            spriteBatch.Begin(transformMatrix: GUIStuff.Instance.camera.GetViewMatrix(gameTime));

            MapStuff.Instance.map.Draw(spriteBatch);

            MapStuff.Instance.partCollHandler.Draw(spriteBatch);

            int fps =(int)( 1 / (float)gameTime.ElapsedGameTime.TotalSeconds);

            spriteBatch.DrawString(font,fps.ToString(), GUIStuff.Instance.camera.position, Color.White);

            //spriteBatch.End();
            
            RenderTarget2D rendTarget2D = MapStuff.Instance.map.rendTarget;

            //spriteBatch.Begin(transformMatrix: GUIStuff.Instance.camera.GetViewMatrix(gameTime));

            spriteBatch.Draw(rendTarget2D, Vector2.Zero, Color.White);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
