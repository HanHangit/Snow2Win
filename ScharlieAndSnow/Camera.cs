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
        Vector3 lastPos;
        public Vector3 scale, lastScale;
        public Vector2 origin;

        public Rectangle view;

        public float speed { get; private set; }


        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(position.ToPoint(), (view.Size.ToVector2() * new Vector2(lastScale.X,lastScale.Y)).ToPoint());
            }
        }

        public Camera(Viewport _viewport)
        {


            speed = GameInformation.Instance.cameraInformation.speed;
            scale = Vector3.One;
            viewport = _viewport;
            position = Vector2.Zero;
            lastPos = Vector3.One;
            origin = new Vector2(viewport.Width / 2f, viewport.Height / 2f);

        }



        public void Reset()
        {
            position = Vector2.Zero;
            scale = Vector3.One;
            origin = new Vector2(viewport.Width / 2f, viewport.Height / 2f);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
        }

        public Matrix GetViewMatrix(GameTime gTime)
        {
            //SomeInit Settings
            KeyboardState key = Keyboard.GetState();
            float minPosX = 0, minPosY = 0;
            float maxPosX = 0, maxPosY = 0;

            int offsetX = 0;
            int offsetY = 0;

            float help = float.MaxValue;

            Player[] playerArray = PlayerManager.Instance.playerArray;

            if (playerArray != null)
            {
                offsetX = GameInformation.Instance.cameraInformation.offsetX;
                offsetY = GameInformation.Instance.cameraInformation.offsetY;

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

                scale = new Vector3(Math.Min(Math.Min(viewport.Width / (maxPosX - minPosX + 2*offsetX),(viewport.Height / (maxPosY - minPosY + 2*offsetY))),1),
                    Math.Min(Math.Min(viewport.Width / (maxPosX - minPosX + 2 * offsetX), viewport.Height / (maxPosY - minPosY + 2 * offsetY)),1),
                     1);
            }

            view = new Rectangle(new Point((int)position.X, (int)position.Y), new Point(viewport.Width, viewport.Height));
            position = new Vector2(minPosX - offsetX, minPosY - offsetY);

            Vector3 newPos = Vector3.Lerp( lastPos, new Vector3(position, 1), speed);
            Vector3 newScale = Vector3.Lerp(lastScale, scale,  speed);

            position = new Vector2((int)newPos.X,(int)newPos.Y);

            lastPos = newPos;
            lastScale = newScale;

            return
                Matrix.CreateTranslation(new Vector3(-(int)newPos.X,-(int)newPos.Y,1))
                * Matrix.CreateScale(newScale);
        }

    }
}
