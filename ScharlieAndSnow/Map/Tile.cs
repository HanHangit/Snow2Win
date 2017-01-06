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
    enum ETile
    {
        Sky,
        Terrain,
        PowerUp,
        Cloud,
        Triangle
    }
    class Tile
    {
        public Vector2 position;
        public Texture2D texture;
        Triangle triangle;
        public bool hasPowerUp;
        public ETile type;

        public Tile(Texture2D _texture, Vector2 _position, ETile _type)
        {
            hasPowerUp = false;
            texture = _texture;
            position = _position;
            type = _type;
        }

        public Tile(Texture2D _texture, Vector2 _position, Triangle _triangle, ETile _type)
        {
            hasPowerUp = false;
            texture = _texture;
            position = _position;
            triangle = _triangle;
            type = _type;
        }

        public void Update(GameTime gameTime)
        {

        }

        //Für die Initalisierung der Map vonnöten.
        public bool Walkable()
        {
            return type != ETile.Terrain;
        }

        public bool Walkable(Vector2 position)
        {
            if (triangle == null)
                return Walkable();
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
