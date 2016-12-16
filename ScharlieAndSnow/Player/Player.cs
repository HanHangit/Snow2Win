using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ScharlieAndSnow
{
    
    class Player
    {
        Texture2D playerTexture;
        Vector2 startPosition;
        public Player(Vector2 _startPosition) {
            startPosition = _startPosition;
        }
        public void LoadContent(ContentManager content)
        {
            playerTexture = content.Load<Texture2D>("Player1");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerTexture, startPosition);
        }
        public void Update(GameTime gTime)
        {

        }
    }
}
