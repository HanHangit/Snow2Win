using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ScharlieAndSnow
{
    class PlayState : IGameState
    {

        public PlayState()
        {

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            MapStuff.Instance.map.Draw(spriteBatch);

            MapStuff.Instance.partCollHandler.Draw(spriteBatch);

 

           

            //spriteBatch.End();

            RenderTarget2D rendTarget2D = MapStuff.Instance.map.rendTarget;

            //spriteBatch.Begin(transformMatrix: GUIStuff.Instance.camera.GetViewMatrix(gameTime));

            spriteBatch.Draw(rendTarget2D, Vector2.Zero, Color.White);

            //Draw Player
            for (int i = 0; i < PlayerManager.Instance.playerArray.Length; i++)
                PlayerManager.Instance.playerArray[i].Draw(spriteBatch);
        }

        public void Initialize()
        {
            PlayerManager.Instance.playerArray = new Player[2];
            PlayerManager.Instance.playerArray[0] = new Player(0,new Vector2(200, 200), MyContentManager.GetTexture(MyContentManager.TextureName.Player1));
            PlayerManager.Instance.playerArray[1] = new Player(1, new Vector2(300, 300), MyContentManager.GetTexture(MyContentManager.TextureName.Player1));

        }

        public void LoadContent()
        {

        }

        public void UnloadContent()
        {

        }

        public EGameState Update(GameTime gameTime)
        {

            //Update Player
            for (int i = 0; i < PlayerManager.Instance.playerArray.Length; i++)
                PlayerManager.Instance.playerArray[i].Update(gameTime);

            MapStuff.Instance.map.Update(gameTime);
            MapStuff.Instance.partCollHandler.Update(gameTime);

            KeyboardState state = Keyboard.GetState();

            if (state.IsKeyDown(Keys.Back))
                return EGameState.Mainmenu;
            return EGameState.PlayState;
        }
    }
}
