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

            while (MapStuff.Instance.map.Walkable(p.position))
            {
                while(MapStuff.Instance.map.Walkable(p.position))
                    p.position.Y += 1f;

                if (MapStuff.Instance.map.Walkable(p.position - new Vector2(1, 0)))
                    p.position.X -= 1;
                else if (MapStuff.Instance.map.Walkable(p.position + new Vector2(1, 0)))
                    p.position.X += 1;
            }

            p.position.Y -= 1f;
            MapStuff.Instance.map.AddSnow(p);
            

        }

        void CalculateBallToPlayer(Particle p)
        {
            Player[] players = PlayerManager.Instance.playerArray;

            foreach(Player ply in players)
            {
                //Bounds vom Player added
                if((ply._pos + new Vector2(8,16) - p.position).Length() < p.radius + 8)
                {

                    p.force = Vector2.Reflect(p.force, new Vector2(0, -1));

                    int numberOfSnow = (int)p.mass / 5;
                    for (int i = 0; i < numberOfSnow; ++i)
                    {
                        Vector2 move = new Vector2(0, -1);
                        move = MyRectangle.rotate(move, MathHelper.ToRadians(MapStuff.Instance.rnd.Next(-30, 30)));
                        MapStuff.Instance.partCollHandler.AddParticle(p.position + new Vector2(0, -20), 1, 1, move);
                    }
                    p.mass = 1;
                    return;
                }
            }
        }

        public void CalculateCollision()
        {
            for (int i = 0; i < particles.Count; ++i)
            {
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
                CalculateBallToMap(particles[i]);
                CalculateBallToPlayer(particles[i]);
                
            }

            hasParticles = false;

            particles.Clear();
        }
    }
}
