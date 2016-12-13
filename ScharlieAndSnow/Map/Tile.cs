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
        Vector2 position;
        public Texture2D texture;
        int id;
        public Triangle[] triangles;

        public Tile(Texture2D _texture, Vector2 _position, int _id)
        {
            texture = _texture;
            position = _position;
            id = _id;
            triangles = new Triangle[2];
        }

        public void Update(GameTime gameTime)
        {

        }

        public bool Walkable()
        {
            return (id == 0);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, Color.White);
            
            for(int i = 0; i < triangles.Length; ++i)
            {
                if (triangles[i] != null)
                    triangles[i].Draw(spriteBatch);
            }                    
        }
    }
}
