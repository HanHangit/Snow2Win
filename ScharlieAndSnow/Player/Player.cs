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
    public enum State { Start, grounded, jumping, Stun, Dying };

    class Player
    {
        float speed = 2;
        int _playerId;
        Vector2 _mov;
        Vector2 _pos;
        Texture2D playerTexture;
        State _currentState;
        

        public Player(int _id, Vector2 _startPosition) {
            _currentState = State.Start;
            _playerId = _id;
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
                                  where PlayerManager.validKeys[_playerId].Contains(k)
                                  select k).ToArray();

            while (!MapStuff.Instance.map.Walkable(new Vector2(_pos.X, _pos.Y - 10)))
                _pos.Y = _pos.Y - 1;
            Console.WriteLine("X: " + _mov.X + " | Y:" + _mov.Y);

            _mov.Y += PlayerManager.gravity;
            //_velocity.Y += PlayerManager.gravity;
            _mov.X = 0;
            if (pressedKeys.Length == 1)
            {
                for (int i = 0; i < pressedKeys.Length; i++)
                    Console.WriteLine(pressedKeys[i].ToString());

                //_mov = Vector2.Zero;

                //UP
                if ((pressedKeys[0] == PlayerManager.validKeys[_playerId][0] && _currentState == State.grounded) || _currentState == State.Start)
                {
                    _mov.Y = -4;
                    _currentState = State.jumping;
                }

                //Left
                if (pressedKeys[0] == PlayerManager.validKeys[_playerId][1])
                {
                    _mov.X -= 1;
                }
                //Right
                if (pressedKeys[0] == PlayerManager.validKeys[_playerId][2])
                {
                    _mov.X += 1;
                }
            }

            //Debug Shit


            if (MapStuff.Instance.map.Walkable(new Vector2(_pos.X + _mov.X + playerTexture.Bounds.Size.X - 1, _pos.Y  + playerTexture.Bounds.Size.X)))
            {
                _pos.X += _mov.X;

            }
            if (MapStuff.Instance.map.Walkable(new Vector2(_pos.X + playerTexture.Bounds.Size.X - 1, _pos.Y + _mov.Y + playerTexture.Bounds.Size.X)))
            {

                _pos.Y += _mov.Y;
            }
            

            else
            {
                _currentState = State.grounded;
                _mov.Y = 0;

            }

            



            //MapStuff.Instance.map.Walkable(); //position Walkable and CheckSnow
            //MapStuff.Instance.map.CheckSnow(); Checkt ob dort Schnell liegt
            //MapStuff.Instance.map.CollectSnow(); Sammelt Schnell auf (Funktion entfernt schnee und lässt nachrutschen)


        }
    }
}
