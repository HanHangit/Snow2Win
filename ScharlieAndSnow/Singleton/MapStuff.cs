using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScharlieAndSnow
{
    class MapStuff
    {
        public Tilemap map;
        public GlobalParticleHandler partCollHandler;
        public Random rnd = new Random(Guid.NewGuid().GetHashCode());
        public Player[] player = new Player[1];

        private MapStuff()
        {

        }

        static MapStuff instance;
        public static MapStuff Instance
        {
            get
            {
                if (instance == null)
                    instance = new MapStuff();

                return instance;
            }
        }
    }
}
