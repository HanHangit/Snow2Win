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


    public enum Direction { Up, Left, Right };
    public enum State { Normal, Dying };

    class Player
    {
        float speed;
        Vector2 _mov;
        Vector2 _pos;
        Texture2D playerTexture;

        public Player(Vector2 _startPosition) {
            _pos = _startPosition;
        }
        public void LoadContent(ContentManager content)
        {
            playerTexture = content.Load<Texture2D>("Player1");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(playerTexture, _pos);
        }
        public void Update(GameTime gTime)
        {
            Keys[] pressedKeys = (from k in Keyboard.GetState().GetPressedKeys()
                                  where PlayerManager.validKeys.Contains(k)
                                  select k).ToArray();



            if (pressedKeys.Length != 1)
                return;
            //Debug Shit
            for (int i = 0; i < pressedKeys.Length; i++)
                Console.WriteLine(pressedKeys[i].ToString());

            _mov = Vector2.Zero;
            //if (pressedKeys[0]) == PlayerManager.


            //MapStuff.Instance.map.Walkable(); //position Walkable and CheckSnow
            //MapStuff.Instance.map.CheckSnow(); Checkt ob dort Schnell liegt
            //MapStuff.Instance.map.CollectSnow(); Sammelt Schnell auf (Funktion entfernt schnee und lässt nachrutschen)


        }
    }
}
