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

        ContentManager Content;

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
            KeyboardState key = Keyboard.GetState();
            float speed = 3;

            if (key.IsKeyDown(Keys.Left))
                position.X -= speed;
            if (key.IsKeyDown(Keys.Right))
                position.X += speed;
            if (key.IsKeyDown(Keys.Up))
                position.Y -= speed;
            if (key.IsKeyDown(Keys.Down))
                position.Y += speed;

            position.X = (int)position.X;
            position.Y = (int)position.Y;


            view = new Rectangle(new Point((int)position.X, (int)position.Y), new Point(viewport.Width, viewport.Height));

            return Matrix.CreateScale(scale)
                * Matrix.CreateTranslation(new Vector3(-position, 1));
        }

    }
}
