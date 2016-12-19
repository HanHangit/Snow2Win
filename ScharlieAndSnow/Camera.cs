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
    class Camera
    {

        public Viewport viewport { get; private set; }

        public Vector2 position;
        public Vector2 origin;

        public Rectangle view;

        public float scale { get; private set; }

        public float speed { get; private set; }

        public Camera(Viewport _viewport)
        {


            speed = 1;
            scale = 1;
            viewport = _viewport;
            position = new Vector2(0, 0);
            origin = new Vector2(viewport.Width / 2f, viewport.Height / 2f);

        }



        public void Reset()
        {
            position = new Vector2(0, 0);
            scale = 1;
            origin = new Vector2(viewport.Width / 2f, viewport.Height / 2f);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }

        public Matrix GetViewMatrix(GameTime gTime)
        {
            //SomeInit Settings
            KeyboardState key = Keyboard.GetState();
            float speed = 0.6f;
            float minPosX = 0, minPosY = 0;
            float maxPosX = 0, maxPosY = 0;

            int offsetX = 0;
            int offsetY = 0;

            float help = float.MaxValue;

            Vector2 scaleVector = Vector2.One;

            Player[] playerArray = PlayerManager.Instance.playerArray;
            if (playerArray != null)
            {
                offsetX = 100;
                offsetY = 100;

                for (int i = 0; i < playerArray.Length; ++i)
                    if (playerArray[i]._pos.X < help)
                        help = playerArray[i]._pos.X;

                minPosX = help;
                help = float.MaxValue;

                for (int i = 0; i < playerArray.Length; ++i)
                    if (playerArray[i]._pos.Y < help)
                        help = playerArray[i]._pos.Y;

                minPosY = help;
                help = 0;

                for (int i = 0; i < playerArray.Length; ++i)
                    if (playerArray[i]._pos.X > help)
                        help = playerArray[i]._pos.X;

                maxPosX = help;
                help = 0;

                for (int i = 0; i < playerArray.Length; ++i)
                    if (playerArray[i]._pos.Y > help)
                        help = playerArray[i]._pos.Y;

                maxPosY = help;

                scaleVector = new Vector2(Math.Min(Math.Min(viewport.Width / (maxPosX - minPosX + 2*offsetX), 1), Math.Min(viewport.Height / (maxPosY - minPosY + 2*offsetY), 1)));
            }

            view = new Rectangle(new Point((int)position.X, (int)position.Y), new Point(viewport.Width, viewport.Height));

            return
                Matrix.CreateTranslation(new Vector3(-new Vector2(minPosX - offsetX, minPosY - offsetY), 1))
                * Matrix.CreateScale(new Vector3(scaleVector, 1));
        }

    }
}
