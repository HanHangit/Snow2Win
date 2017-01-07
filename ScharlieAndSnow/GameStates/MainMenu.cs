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
    public class TextWriterDropShadow
    {
        public Vector2 Offset;
        public Color Color;

        public TextWriterDropShadow(Vector2 offset, Color color)
        {
            Offset = offset;
            Color = color;
        }
    }
    class MainMenu : IGameState
    {
        MyButton playButton;
        MyButton exitButton;
        MyButton shitClick;
        MyButton continueButton;
        MyButton exitButton2;
        Texture2D background;

        #region testfieldBird
        Texture2D birdTexture;
        Rectangle destRect;
        Rectangle sourceRect;

        float elapsed;
        float delay = 120f;
        int frames = 0;

        Vector2 pos;

        #endregion

        String headline = "ScharlieAndSnow";
        bool troll;
        public MainMenu()
        {
            background = MyContentManager.GetTexture(MyContentManager.TextureName.BackGround_1270x720);

            pos = new Vector2(-150, 15);

            troll = false;
            Console.WriteLine("MainMenu");
            continueButton = new MyButton("", MyContentManager.GetTexture(MyContentManager.TextureName.GoBUtton), new Vector2(95,85), MyContentManager.GetFont(MyContentManager.FontName.Arial));
            exitButton2 = new MyButton("", MyContentManager.GetTexture(MyContentManager.TextureName.ExitButton), new Vector2(5, 85), MyContentManager.GetFont(MyContentManager.FontName.Arial));
            playButton = new MyButton("PlayWithMe", 200, 40, new Vector2(40, 60), MyContentManager.GetFont(MyContentManager.FontName.Arial), Color.Red);
            exitButton = new MyButton("Exit Game", 200, 40, Constant.Calculate(40, 68), MyContentManager.GetFont(MyContentManager.FontName.Arial), Color.Red);
            shitClick = new MyButton("Keine Tasten drücken, sondern die Maus bedienen! Faule Studenten", 600, 40, Constant.Calculate(1, 80), MyContentManager.GetFont(MyContentManager.FontName.Arial), Color.Red);

            birdTexture = MyContentManager.GetTexture(MyContentManager.TextureName.littleBird);
            destRect = new Rectangle((int)pos.X, (int)pos.Y, 64, birdTexture.Height);

        }




        public void Initialize()
        {

        }

        public void LoadContent()
        {

        }

        public void UnloadContent()
        {

        }

        public EGameState Update(GameTime gTime)
        {
            FlyLittleBird(gTime);
            continueButton.Update(gTime);
            playButton.Update(gTime);
            exitButton.Update(gTime);
            shitClick.Update(gTime);
            KeyboardState state = Keyboard.GetState();

            if (continueButton.isClick())
                return EGameState.PlayState;
            if (state.IsKeyDown(Keys.Enter))
                return EGameState.PlayState;
            if (exitButton2.isClick())
                Console.WriteLine("EXIT");


            //DEBUG
            if (state.IsKeyDown(Keys.F1))
                troll = true;
            if (state.IsKeyDown(Keys.F2))
                troll = false;

            return EGameState.Mainmenu;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, new Vector2(0, 0));
            spriteBatch.DrawString(MyContentManager.GetFont(MyContentManager.FontName.Arial), headline, Constant.Calculate(25, 20), Color.Red, 0, new Vector2(0, 0), 3, SpriteEffects.None, 0);
            spriteBatch.Draw(birdTexture, destRect, sourceRect, Color.White);
            continueButton.Draw(spriteBatch);
            //playButton.Draw(spriteBatch);
            exitButton2.Draw(spriteBatch);
            //exitButton.Draw(spriteBatch);

            if (troll)
                shitClick.Draw(spriteBatch);
        }


        void FlyLittleBird(GameTime gTime)
        {
            if (pos.X > Constant.x)
                pos.X = -150;

            pos.X = pos.X + 1.8f;
            destRect = new Rectangle((int)pos.X, (int)pos.Y, 128, birdTexture.Height);

            if(elapsed >= delay)
            {
                if (frames >= 9)
                    frames = 0;
                else
                    frames++;
                elapsed = 0;
            }
            sourceRect = new Rectangle(birdTexture.Width / 10 * frames, 0, 128, 128);
            elapsed += (float)gTime.ElapsedGameTime.TotalMilliseconds;
        }

        public void DrawGUI(SpriteBatch spriteBatch)
        {
        }
    }
}
