using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScharlieAndSnow
{
    class GUIStuff
    {
        public Camera camera;


        private GUIStuff(){ }

        static GUIStuff instance;

        public static GUIStuff Instance
        {
            get
            {
                if (instance == null)
                    instance = new GUIStuff();

                return instance;
            }
        }
            
    }
}
