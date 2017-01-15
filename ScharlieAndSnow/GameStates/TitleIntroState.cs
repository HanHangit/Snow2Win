using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScharlieAndSnow
{
    class TitleIntroState :IGameState
    {

        TimeSpan elapsed;
        Vector2 position;
        SpriteFont font;
        string message;

        public TitleIntroState()
        {
            Console.WriteLine("TITLEINTROSTATE");
            font = MyContentManager.GetFont(MyContentManager.FontName.Arial);
            message = "PRESS SPACE TO CONTINUE";
            elapsed = TimeSpan.Zero;

            Vector2 size = font.MeasureString(message);
            position = new Vector2((Constant.ScreenRectangle.Width - size.X) / 2,
                        Constant.ScreenRectangle.Bottom - 50 - font.LineSpacing);
        }


        public void Initialize()
        {

        }

        public EGameState Update(GameTime gTime)
        {
            KeyboardState state = Keyboard.GetState();
            if (state.IsKeyDown(Keys.Space))
            {
                return EGameState.Mainmenu;
            }

            elapsed += gTime.ElapsedGameTime;





            return EGameState.TitleIntroState;
        }

        public void LoadContent()
        {

        }

        public void UnloadContent()
        {

        }

        public void DrawGUI(SpriteBatch spriteBatch)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            Color color = new Color(142f, 39f, 39f) * (float)Math.Abs(Math.Sin(elapsed.TotalSeconds * 2));
            spriteBatch.DrawString(font, message, position, color);


        }

    }
}