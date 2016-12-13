using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScharlieAndSnow
{
    class JobParticleDelete
    {
        public JobParticleDelete()
        {

        }


        public void DeleteParticle()
        {
            List<Particle> particles = MapStuff.Instance.partCollHandler.particles;

            for (int i = 0; i < particles.Count && particles[i] != null; ++i)
            {
                if (!particles[i].alive)
                {
                    MapStuff.Instance.map.particles.Add(particles[i]);
                    particles.RemoveAt(i--);
                }
            }
        }
    }
}
