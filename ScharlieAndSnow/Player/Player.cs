﻿using System;
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
        float _points = 0;
        int _playerId;
        Vector2 _mov;
        public Vector2 _pos;
        Texture2D playerTexture;
        State _currentState;


        public Player(int _id, Vector2 _startPosition)
        {
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

            ControllerCheckInput();

        }

        /*  MapStuff.Instance.map.Walkable(); //position Walkable and CheckSnow
            MapStuff.Instance.map.CheckSnow(); Checkt ob dort Schnell liegt
            MapStuff.Instance.map.CollectSnow(); Sammelt Schnell auf (Funktion entfernt schnee und lässt nachrutschen)
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
                    _mov.X -= 1;
                    for (int i = 0; i < 1; ++i)
                    {
                        Vector2 move = new Vector2((_mov.X * -1)/4, 0);
                        move = MyRectangle.rotate(move, MathHelper.ToRadians(MapStuff.Instance.rnd.Next(-10, 10)));
                        MapStuff.Instance.partCollHandler.AddParticle(_pos + playerTexture.Bounds.Size.ToVector2() / 2 + new Vector2(playerTexture.Bounds.Size.X / 2, 0) + new Vector2(0,playerTexture.Bounds.Size.Y / 2) + new Vector2(0, -5), 1, 3, move);
                    }
                }

                //Move Right
                if (pressedKeys[0] == PlayerManager.validKeys[_playerId][2])
                {
                    _mov.X += 1;
                    for (int i = 0; i < 1; ++i)
                    {
                        Vector2 move = new Vector2((_mov.X * -1)/4, 0);
                        move = MyRectangle.rotate(move, MathHelper.ToRadians(MapStuff.Instance.rnd.Next(-10, 10)));
                        MapStuff.Instance.partCollHandler.AddParticle(_pos + playerTexture.Bounds.Size.ToVector2() / 2 - new Vector2(playerTexture.Bounds.Size.X / 2, 0) + new Vector2(0, playerTexture.Bounds.Size.Y / 2) + new Vector2(0, -5), 1, 3, move);
                    }
                }
                //Collect Snow
                if(pressedKeys[0] == PlayerManager.validKeys[_playerId][3])
                {
                    if (MapStuff.Instance.map.CheckSnow(new Vector2(_pos.X + playerTexture.Bounds.Size.X, _pos.Y + playerTexture.Bounds.Size.Y + 8)))
                    {
                        //new Vector2(_pos.X + playerTexture.Bounds.Size.X, _pos.Y + playerTexture.Bounds.Size.Y + 5)
                        MapStuff.Instance.map.CollectSnow(new Vector2(_pos.X + playerTexture.Bounds.Size.X, _pos.Y + playerTexture.Bounds.Size.Y + 8));
                        _points++;
                        Console.WriteLine(_points);
                    }
                }
                

            }
            CheckCollision(_mov); //Check ob die Bewegung funktioniert
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
