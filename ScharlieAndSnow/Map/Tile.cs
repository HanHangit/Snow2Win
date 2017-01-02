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
    class Tile
    {
        public Vector2 position;
        public Texture2D texture;
        int id;
        Triangle triangle;
        public bool hasPowerUp;

        public Tile(Texture2D _texture, Vector2 _position, int _id)
        {
            hasPowerUp = false;
            texture = _texture;
            position = _position;
            id = _id;

        }

        public Tile(Texture2D _texture, Vector2 _position, int _id, Triangle _triangle)
        {
            texture = _texture;
            position = _position;
            id = _id;
            triangle = _triangle;
        }

        public void Update(GameTime gameTime)
        {

        }

        //Für die Initalisierung der Map vonnöten.
        public bool Walkable()
        {
            return id == 0;
        }

        public bool Walkable(Vector2 position)
        {
            if (triangle == null)
                return (id == 0);
            else
            {
                //Console.WriteLine("Triangle Intersect " + position);
                return !triangle.intersect(position);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
        }
    }
}
