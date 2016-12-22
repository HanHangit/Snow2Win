using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace ScharlieAndSnow
{
    class Particle
    {
        public Vector2 position;
        public Vector2 force;
        public float mass;
        public bool alive, noCollision, playerCollision;
        public float radius;

        public Color color;

        public float damage;

        public Rectangle destRectangle;

        public static Texture2D particleText;

        static float drag = -0.003f;

        static Vector2 gravity = new Vector2(0, 0.05f);
        public Particle(Texture2D _text, Vector2 _position, float _mass, float _radius, Vector2 move, float _damage)
        {
            playerCollision = true;
            damage = _damage;
            color = Color.White;
            position = _position + new Vector2(radius, radius);
            if (Particle.particleText == null)
                particleText = _text;
            mass = _mass;
            alive = true;
            radius = _radius;
            force = move;
            noCollision = false;

            destRectangle = new Rectangle((int)_position.X, (int)_position.Y, (int)_radius, (int)_radius);
        }

        public Particle(Texture2D _text, Vector2 _position, float _mass, float _radius, Vector2 move, bool _PlayerCollision)
        {
            playerCollision = _PlayerCollision;
            damage = 0;
            color = Color.White;
            position = _position + new Vector2(radius, radius);
            if (Particle.particleText == null)
                particleText = _text;
            mass = _mass;
            alive = true;
            radius = _radius;
            force = move;
            noCollision = false;

            destRectangle = new Rectangle((int)_position.X, (int)_position.Y, (int)_radius, (int)_radius);
        }

        public Particle(Texture2D _text, Vector2 _position, float _mass, float _radius, Vector2 move)
        {
            playerCollision = true;
            damage = 0;
            color = Color.White;
            position = _position + new Vector2(radius, radius);
            if (Particle.particleText == null)
                particleText = _text;
            mass = _mass;
            alive = true;
            radius = _radius;
            force = move;
            noCollision = false;

            destRectangle = new Rectangle((int)_position.X, (int)_position.Y, (int)_radius, (int)_radius);
        }

        public void ApplyForce(Vector2 _force)
        {
            force += _force;
        }

        public void SetVelocity(Vector2 _velocity)
        {
            force = _velocity;
        }

        public void Update(GameTime gTime)
        {
            force += gravity;

            force += drag * force;

            position += force;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            destRectangle.Location = (position).ToPoint();

            spriteBatch.Draw(particleText, destRectangle, Color.White);
        }

        public static void SplitUpParticle(Particle p, Rectangle collisionObject)
        {
            Vector2 lastPartPos = p.position;

            Vector2 move = p.force;



            while (move.X == p.force.X && move.Y == p.force.Y && !(move.X == 0 && move.Y == 0))
            {
                lastPartPos -= p.force;

                if (lastPartPos.X < collisionObject.X || lastPartPos.X > collisionObject.X + collisionObject.Width)
                    move.X *= -1;

                if (lastPartPos.Y < collisionObject.Y || lastPartPos.Y > collisionObject.Y + collisionObject.Height)
                    move.Y *= -1;

            }


            p.radius = 1;

            int numberOfSnow = (int)p.mass;

            if (numberOfSnow <= 5)
                return;

            p.mass = 1;
            p.alive = false;

            for (int i = 0; i < numberOfSnow; ++i)
            {
                move = MyRectangle.rotate(move, MathHelper.ToRadians(MapStuff.Instance.rnd.Next(-30, 30)));

                Rectangle particleRect = new Rectangle(p.position.ToPoint(), new Point(2, 2));

                move = Vector2.Normalize(move);
                move *= 2;

                while (particleRect.Intersects(collisionObject))
                {
                    p.position += move;
                    particleRect = new Rectangle(p.position.ToPoint(), new Point(2, 2));
                }

                MapStuff.Instance.partCollHandler.AddParticle(p.position, 1, 1, move, false);
            }
        }
        public static void SplitUpParticle(Particle p)
        {
            p.radius = 1;

            int numberOfSnow = (int)p.mass;

            if (numberOfSnow <= 5)
                return;

            for (int i = 0; i < numberOfSnow; ++i)
            {
                Vector2 move = new Vector2(0, -1);
                move = MyRectangle.rotate(move, MathHelper.ToRadians(MapStuff.Instance.rnd.Next(-30, 30)));
                MapStuff.Instance.partCollHandler.AddParticle(p.position + new Vector2(0, -10), 1, 1, move);
            }
        }


    }
}
