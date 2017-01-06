using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScharlieAndSnow
{
    class Job_Collision
    {
        List<Particle> particles = new List<Particle>();

        public bool hasParticles;


        public Job_Collision()
        {

        }

        public void AddParticle(Particle p)
        {
            particles.Add(p);
            hasParticles = true;
        }

        void CalculateBallToBallCollision(Particle p1, Particle p2)
        {
            if (p1.noCollision)
                return;
            Vector2 collision = p1.position - p2.position;
            float distance = collision.Length();
            if (distance == 0.0)
            {
                return;
            }
            if (distance > p1.radius + p2.radius)
                return;

            p1.force -= (2 * p2.mass) / (p1.mass + p2.mass) * Vector2.Dot(p1.force - p2.force, p1.position - p2.position) / (float)Math.Pow((p1.position - p2.position).Length(), 2) * (p1.position - p2.position);
            p2.force -= (2 * p1.mass) / (p2.mass + p1.mass) * Vector2.Dot(p2.force - p1.force, p2.position - p1.position) / (float)Math.Pow((p2.position - p1.position).Length(), 2) * (p2.position - p1.position);
            p1.position += p1.force;
            p2.position += p2.force;
        }

        void CalculateBallToMap(Particle p)
        {
            if (MapStuff.Instance.map.Walkable(p.position + p.force))
                return;

            p.radius = 1;

            while (MapStuff.Instance.map.Walkable(new Vector2(p.position.X, p.position.Y + 1)))
                p.position.Y += 1;



            MapStuff.Instance.map.AddSnow(p);        

        }

        void CalculateBallToPlayer(Particle p)
        {
            if (!p.playerCollision)
                return;
            Player[] players = PlayerManager.Instance.playerArray;

            foreach(Player ply in players)
            {
                Rectangle player = ply.Bounds;
                Rectangle particle = p.destRectangle;
                particle.Location = (particle.Location.ToVector2()).ToPoint();
                //Bounds vom Player added
                if(player.Intersects(particle))
                {
                    p.alive = false;
                    ply.ApplyDamage(p.damage);
                    Particle.SplitUpParticle(p,player);
                    return;
                }
            }
        }

        public void CalculateCollision()
        {
            for (int i = 0; i < particles.Count; ++i)
            {
                CalculateBallToMap(particles[i]);
                CalculateBallToPlayer(particles[i]);
                if (!particles[i].alive)
                    continue;
                for (int j = i + 1; j < particles.Count; ++j)
                {
                    
                    Particle p1 = particles[i];
                    Particle p2 = particles[j];

                    // get the mtd
                    Vector2 delta = (p1.position - p2.position);
                    float d = delta.Length();

                    if (d > (p1.radius + p2.radius))
                        continue;

                    CalculateBallToBallCollision(p1, p2);

                }
                
            }

            hasParticles = false;

            particles.Clear();
        }
    }
}
