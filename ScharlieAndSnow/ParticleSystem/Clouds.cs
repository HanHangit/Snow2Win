using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScharlieAndSnow
{
    class Clouds
    {
        Texture2D texture;
        Vector2 position, move;

        float speed,spawnTime, lifeTimer;
        float spawnTimer;

        int mass, radius;

        public bool alive;

        public Clouds(Texture2D _text, Vector2 _position, Vector2 _move, float _speed, float _spawnTime,float _lifeTime, int partMass, int partRadius)
        {
            mass = partMass;
            radius = partRadius;
            texture = _text;
            position = _position;
            speed = _speed;
            spawnTime = _spawnTime;
            lifeTimer = _lifeTime;
            move = _move;
            alive = true;
        }

        public void Update(GameTime gTime)
        {
            lifeTimer -= (float)gTime.ElapsedGameTime.TotalSeconds;
            spawnTimer -= (float)gTime.ElapsedGameTime.TotalSeconds;

            if (lifeTimer <= 0)
                alive = true;

            if(spawnTimer <= 0)
            {
                spawnTimer = spawnTime;

                int offsetX = MapStuff.Instance.rnd.Next(-40, 40) + texture.Width / 2;
                int offsetY = texture.Height / 2;

                MapStuff.Instance.partCollHandler.AddParticle(position + new Vector2(offsetX,offsetY), mass, radius,new Vector2(0,1));

            }

            if (position.X + move.X<= 0 || position.X + move.X + texture.Width >= MapStuff.Instance.map.realSize.X)
                move *= -1;

            position += move;
        }

        public void Draw(SpriteBatch batch)
        {
            batch.Draw(texture, position, Color.White);
        }

    }
}
