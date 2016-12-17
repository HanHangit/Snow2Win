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
        public Vector2 _pos;
        public float temperature { get; private set; }
        float maxTemperature;
        public bool isAlive { get; private set; }
        float speed, jumpSpeed;
        float _points = 0;
        int _playerId;
        Vector2 _mov;
        Vector2 snowballMove;
        Texture2D playerTexture;
        State _currentState;
        Direction _currentDirection;
        List<PowerUp> modifikator;


        public Player(int _id, Vector2 _startPosition, Texture2D _playerTexture)
        {
            _currentState = State.Start;
            _currentDirection = Direction.Right;
            playerTexture = _playerTexture;
            isAlive = true;
            _playerId = _id;
            temperature = GameInformation.Instance.playerInformation.maxHealth;
            maxTemperature = GameInformation.Instance.playerInformation.maxHealth;
            speed = GameInformation.Instance.playerInformation.speed;
            jumpSpeed = GameInformation.Instance.playerInformation.jumpSpeed;
            snowballMove = GameInformation.Instance.playerInformation.snowballMove;
            _pos = _startPosition;
            modifikator = new List<PowerUp>();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (_currentDirection == Direction.Right)
                spriteBatch.Draw(playerTexture, _pos);
            else
                spriteBatch.Draw(playerTexture, _pos, null, Color.White, (float)Math.PI, new Vector2(playerTexture.Width, playerTexture.Height), 1, SpriteEffects.None, 0);
        }
        public void Update(GameTime gTime)
        {
            if (!isAlive) return;

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                new PowerUp(null, Vector2.Zero, 10, new PlayerModifikator(5, 2, 1, 0)).ApplyToPlayer(this);
            }

            if (Keyboard.GetState().IsKeyDown(Keys.K))
                ApplyDamage(1);

            ControllerCheckInput();

            CheckPowerUps(gTime);

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

            for (int i = 0; i < pressedKeys.Length; ++i)
            {
                if (pressedKeys[i] == PlayerManager.validKeys[_playerId][0] && _currentState != State.jumping)
                {
                    float realJump = jumpSpeed;
                    foreach (PowerUp mode in modifikator)
                        realJump += realJump * mode.mode.jump / 100;
                    _mov.Y = -realJump;
                    //Ist ganz praktisch, den Charakter einen Pixel nach oben zu bewegen, damit er auf jedenfall springen darf.
                    _pos.Y -= 1;
                    _currentState = State.jumping;
                }

                //Move UP
                if ((pressedKeys[i] == PlayerManager.validKeys[_playerId][0] && _currentState == State.grounded) || _currentState == State.Start)
                {
                    _mov.Y = -4;
                    //Ist ganz praktisch, den Charakter einen Pixel nach oben zu bewegen, damit er auf jedenfall springen darf.
                    _pos.Y -= 1;
                    _currentState = State.jumping;
                }

                //Move Left
                if (pressedKeys[i] == PlayerManager.validKeys[_playerId][1])
                {
                    _mov.X = -1;
                    Flip(Direction.Left);
                }
                //Move Right
                if (pressedKeys[i] == PlayerManager.validKeys[_playerId][2])
                {
                    _mov.X = 1;
                    Flip(Direction.Right);
                }
                //Collect Snow
                if (pressedKeys[i] == PlayerManager.validKeys[_playerId][3])
                {
                    if (_currentDirection == Direction.Right)
                    {
                        if (MapStuff.Instance.map.CheckSnow(new Vector2(_pos.X + playerTexture.Bounds.Size.X, _pos.Y + playerTexture.Bounds.Size.Y + 8)))
                        {
                            MapStuff.Instance.map.CollectSnow(new Vector2(_pos.X + playerTexture.Bounds.Size.X, _pos.Y + playerTexture.Bounds.Size.Y + 8), 4);
                            _points++;
                            Console.WriteLine(_points);
                        }
                    }
                    else
                    {
                        if (MapStuff.Instance.map.CheckSnow(new Vector2(_pos.X, _pos.Y + playerTexture.Bounds.Size.Y + 8)))
                        {
                            MapStuff.Instance.map.CollectSnow(new Vector2(_pos.X, _pos.Y + playerTexture.Bounds.Size.Y + 8), 4);
                            _points++;
                            Console.WriteLine(_points);
                        }
                    }
                }

                if (pressedKeys[i] == PlayerManager.validKeys[_playerId][4])
                {
                    if (_points > 0)
                    {
                        if (_currentDirection == Direction.Right)
                            MapStuff.Instance.partCollHandler.AddParticle(_pos + new Vector2(playerTexture.Bounds.Size.X + 5,0), 5, 3, snowballMove);
                        else
                            MapStuff.Instance.partCollHandler.AddParticle(_pos + new Vector2(playerTexture.Bounds.Size.X - 5,0), 5, 3, new Vector2(snowballMove.X * -1, snowballMove.Y));
                        Console.WriteLine(_points);
                        _points--;
                    }
                }
            }

            float realSpeed = speed;
            foreach (PowerUp mode in modifikator)
                realSpeed += realSpeed * mode.mode.speed / 100;
            _mov.X *= realSpeed;

            CheckCollision(_mov); //Check ob die Bewegung funktioniert
        }

        public void ApplyPowerUp(PowerUp powerUp)
        {
            modifikator.Add(powerUp);
            ToWarumUp((int)powerUp.mode.health);
        }

        void CheckPowerUps(GameTime gTime)
        {
            for (int i = 0; i < modifikator.Count; ++i)
            {
                modifikator[i].Update(gTime);
                if (!modifikator[i].alive)
                    modifikator.RemoveAt(i--);
            }
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
                _pos.X += _mov.X;

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

        /// <summary>
        /// jegliche Schadenquelle für den Spieler über diese Funktion
        /// </summary>
        /// <param name="damage"></param>
        /// <returns></returns>
        public void ApplyDamage(int damage)
        {
            float realDamage = damage;
            foreach (PowerUp p in modifikator)
                realDamage -= realDamage * (p.mode.armor / 100f);

            Console.WriteLine("Damage To Player" + realDamage);

            temperature -= realDamage;
            if (temperature <= 0)
            {
                isAlive = false;
            }

        }

        /// <summary>
        /// Gibt den Spieler wärme (max. 100)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public void ToWarumUp(int value)
        {
            Console.WriteLine("HealthUp " + value);
            temperature += value;
            if (temperature > maxTemperature)
                temperature = maxTemperature;
        }
    }
}
