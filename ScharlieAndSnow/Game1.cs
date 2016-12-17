﻿using Microsoft.Xna.Framework;
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
        IGameState state;
        EGameState prev = EGameState.None, curr = EGameState.Mainmenu;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            MyContentManager myContentManager = new MyContentManager(Content);
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
            this.IsMouseVisible = true;
            
            GraphicStuff.Instance.graphicDevice = GraphicsDevice;

            graphics.PreferredBackBufferWidth = Constant.x;
            graphics.PreferredBackBufferHeight = Constant.y;

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
            

            MapStuff.Instance.map = new Tilemap(16);

            MapStuff.Instance.partCollHandler = new GlobalParticleHandler();

            MapStuff.Instance.map.CreateDebugSnow();

            //Initialize Camera
            GUIStuff.Instance.camera = new Camera(graphics.GraphicsDevice.Viewport);

            

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
            HandleGameStates();
            curr = state.Update(gameTime);


            // TODO: Add your update logic here


            

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
            state.Draw(spriteBatch);

            int fps = (int)(1 / (float)gameTime.ElapsedGameTime.TotalSeconds);
            spriteBatch.DrawString(MyContentManager.GetFont(MyContentManager.FontName.Arial), fps.ToString(), GUIStuff.Instance.camera.position, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
        void HandleGameStates()
        {

            if (prev != curr)
            {

                if (state != null)
                    state.UnloadContent();

                switch (curr)
                {
                    case EGameState.Mainmenu:
                        state = new MainMenu();
                        state.Initialize();
                        state.LoadContent();
                        break;
                    case EGameState.PlayState:
                        state = new PlayState();
                        state.Initialize();
                        state.LoadContent();
                        break;
                }
            }
            prev = curr;
        }
    }
}
