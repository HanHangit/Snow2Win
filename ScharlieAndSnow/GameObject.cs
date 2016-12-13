using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScharlieAndSnow
{
    abstract class GameObject
    {
        /// <summary>
        /// Simple GameObject. Textur wird gezeichnet und Position aktualisiert 
        /// auf Grundlage des Moves Vector
        /// </summary>
        /// <param name="_text">Texture</param>
        /// <param name="_position">Position</param>
        public GameObject(Texture2D _text, Vector2 _position)
        {
            text = _text;
            position = _position;
            move = Vector2.Zero;
        }

        public Texture2D text;
        public Vector2 position;
        protected Vector2 move;
        public virtual void Update(GameTime gTime)
        {
            position += move;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(text, position, Color.White);
        }
    }
}
