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

            PlayerManager.Instance.playerArray = new Player[2];
            PlayerManager.Instance.playerArray[0] = new Player(0,new Vector2(200, 200));
            PlayerManager.Instance.playerArray[1] = new Player(1, new Vector2(300, 300));


            GraphicStuff.Instance.graphicDevice = GraphicsDevice;

            graphics.PreferredBackBufferWidth = 1200;
            graphics.PreferredBackBufferHeight = 1300;

            //graphics.ToggleFullScreen();

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

            //font = Content.Load<SpriteFont>("FPS");

            //Initialize Player
            for (int i = 0; i < PlayerManager.Instance.playerArray.Length; i++)
                PlayerManager.Instance.playerArray[i].LoadContent(Content);

            //Initialize Map
            Texture2D[] tiles = { Content.Load<Texture2D>("SkyTile"), Content.Load<Texture2D>("SnowTile"),
                                    Content.Load<Texture2D>("MapTiles/SnowTile_down"),Content.Load<Texture2D>("MapTiles/SnowTile_Up")};
            Texture2D bitMap = Content.Load<Texture2D>("Map02");
            Texture2D[] clouds = { Content.Load<Texture2D>("Clouds") };

            MapStuff.Instance.map = new Tilemap(tiles,clouds, bitMap, 16);


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

            //Update Player
            for (int i = 0; i < PlayerManager.Instance.playerArray.Length; i++)
                PlayerManager.Instance.playerArray[i].Update(gameTime);;

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

            spriteBatch.DrawString(MyContentManager.GetFont(MyContentManager.FontName.Arial),fps.ToString(), GUIStuff.Instance.camera.position, Color.White);

            //spriteBatch.End();
            
            RenderTarget2D rendTarget2D = MapStuff.Instance.map.rendTarget;

            //spriteBatch.Begin(transformMatrix: GUIStuff.Instance.camera.GetViewMatrix(gameTime));

            spriteBatch.Draw(rendTarget2D, Vector2.Zero, Color.White);

            //Draw Player
            for (int i = 0; i < PlayerManager.Instance.playerArray.Length; i++)
                PlayerManager.Instance.playerArray[i].Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
