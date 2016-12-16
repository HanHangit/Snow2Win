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
        public bool alive, noCollision;
        public float radius;

        public Color color;

        public Rectangle destRectangle;

        public static Texture2D particleText;

        static float drag = -0.003f;

        static Vector2 gravity = new Vector2(0, 0.05f);

        public Particle(Texture2D _text, Vector2 _position, float _mass, float _radius, Vector2 move)
        {
            color = Color.White;
            position = _position + new Vector2(radius,radius);
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

            spriteBatch.Draw(particleText,destRectangle , Color.White);
        }


    }
}
