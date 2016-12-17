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


    public enum Direction { Left, Right };
    public enum State { Start, grounded, jumping, Stun, Dying };

    class Player
    {
        float speed = 2;
        float _points = 0;
        int _playerId;
        Vector2 _mov;
        Vector2 _pos;
        Texture2D playerTexture;
        State _currentState;
        Direction _currentDirection;


        public Player(int _id, Vector2 _startPosition, Texture2D _playerTexture)
        {
            playerTexture = _playerTexture;
            _currentState = State.Start;
            _currentDirection = Direction.Right;
            _playerId = _id;
            _pos = _startPosition;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(_currentDirection == Direction.Right)
                spriteBatch.Draw(playerTexture, _pos);
            else
            {
                spriteBatch.Draw(playerTexture, _pos, null, Color.White, (float)Math.PI, new Vector2(playerTexture.Width, playerTexture.Height), 1, SpriteEffects.None, 0);
            }
        }
        public void Update(GameTime gTime)
        {

            ControllerCheckInput();

        }

        /*  MapStuff.Instance.map.Walkable(); //position Walkable and CheckSnow
            MapStuff.Instance.map.CheckSnow(); Checkt ob dort Schnell liegt
            MapStuff.Instance.map.CollectSnow(); Sammelt Schnell auf (Funktion entfernt schnee und lässt nachrutschen)
            MapStuff.Instance.partCollHandler.AddParticle(Vector2 _position, float _mass, float _radius, Vector2 move);
        */



        void ControllerCheckInput()
        {
            Keys[] pressedKeys = (from k in Keyboard.GetState().GetPressedKeys()
                                  where PlayerManager.validKeys[_playerId].Contains(k)
                                  select k).ToArray();

            //Nur die Gravitation abziehen, wenn ich nicht auf dem Grund stehe.
            if (_currentState != State.grounded)
                _mov.Y += PlayerManager.gravity;

            _mov.X = 0;
            if (pressedKeys.Length == 1 || pressedKeys.Length == 2)
            {
                // --- 2 Inputs! ---
                if (pressedKeys.Length == 2)
                    if ((pressedKeys[1] == PlayerManager.validKeys[_playerId][0] && _currentState == State.grounded) || _currentState == State.Start)
                    {
                        _mov.Y = -4;
                        //Ist ganz praktisch, den Charakter einen Pixel nach oben zu bewegen, damit er auf jedenfall springen darf.
                        _pos.Y -= 1;
                        _currentState = State.jumping;
                    }

                // --- 1 Input! ---



                //Move UP
                if ((pressedKeys[0] == PlayerManager.validKeys[_playerId][0] && _currentState == State.grounded) || _currentState == State.Start)
                {
                    _mov.Y = -4;
                    //Ist ganz praktisch, den Charakter einen Pixel nach oben zu bewegen, damit er auf jedenfall springen darf.
                    _pos.Y -= 1;
                    _currentState = State.jumping;
                }

                //Move Left
                if (pressedKeys[0] == PlayerManager.validKeys[_playerId][1])
                {
                    _mov.X = -1;
                    Flip(Direction.Left);
                }


                //Move Right
                if (pressedKeys[0] == PlayerManager.validKeys[_playerId][2])
                {
                    _mov.X = +1;
                    Flip(Direction.Right);
                }

                //Collect Snow
                if (pressedKeys[0] == PlayerManager.validKeys[_playerId][3])
                {
                    if (_currentDirection ==  Direction.Right)
                    {
                        if (MapStuff.Instance.map.CheckSnow(new Vector2(_pos.X + playerTexture.Bounds.Size.X, _pos.Y + playerTexture.Bounds.Size.Y + 8)))
                        {
                            MapStuff.Instance.map.CollectSnow(new Vector2(_pos.X + playerTexture.Bounds.Size.X, _pos.Y + playerTexture.Bounds.Size.Y + 8));
                            _points++;
                            Console.WriteLine(_points);
                        }
                    }
                    else
                    {
                        if (MapStuff.Instance.map.CheckSnow(new Vector2(_pos.X, _pos.Y + playerTexture.Bounds.Size.Y + 8)))
                        {
                            MapStuff.Instance.map.CollectSnow(new Vector2(_pos.X, _pos.Y + playerTexture.Bounds.Size.Y + 8));
                            _points++;
                            Console.WriteLine(_points);
                        }
                    }

                }


            }
            CheckCollision(_mov); //Check ob die Bewegung funktioniert
        }
        public void Flip(Direction newDirection)
        {
            _currentDirection = newDirection;

        }
        void CheckCollision(Vector2 _mov)
        {
            //Collision der linken obere Ecke sowie der rechten obere Ecke
            //Collision Check X-Achse
            if (MapStuff.Instance.map.Walkable(new Vector2(_pos.X + _mov.X + playerTexture.Bounds.Size.X, _pos.Y + playerTexture.Bounds.Size.Y - 17))
            && MapStuff.Instance.map.Walkable(new Vector2(_pos.X + _mov.X, _pos.Y + playerTexture.Bounds.Size.Y - 17)))
            {
                _pos.X += _mov.X * speed;

            }
            //Collision Check Y-Achse
            if (MapStuff.Instance.map.Walkable(new Vector2(_pos.X, _pos.Y + _mov.Y + playerTexture.Bounds.Size.Y))
                && MapStuff.Instance.map.Walkable(new Vector2(_pos.X + playerTexture.Bounds.Size.X, _pos.Y + _mov.Y + playerTexture.Bounds.Size.Y)))
            {

                _pos.Y += _mov.Y;
                //Wenn ich nach unten fallen DARF, dann befinde ich mich ja in der Luft -> state = state.jumping
                _currentState = State.jumping;
            }
            else
            {
                _currentState = State.grounded;
                _mov.Y = 0;
            }
            while (!MapStuff.Instance.map.Walkable(new Vector2(_pos.X, _pos.Y + playerTexture.Bounds.Size.Y))
            || !MapStuff.Instance.map.Walkable(new Vector2(_pos.X + playerTexture.Bounds.Size.X, _pos.Y + playerTexture.Bounds.Size.Y)))
                _pos.Y = _pos.Y - 1;
        }
    }
}
