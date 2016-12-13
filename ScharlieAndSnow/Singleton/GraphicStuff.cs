using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScharlieAndSnow
{
    class GraphicStuff
    {
        public GraphicsDevice graphicDevice;

        private GraphicStuff()
        {

        }

        static GraphicStuff instance;

        public static GraphicStuff Instance
        {
            get
            {
                if (instance == null)
                    instance = new GraphicStuff();

                return instance;
            }
        }
    }
}
