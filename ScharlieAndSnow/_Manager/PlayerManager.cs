using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScharlieAndSnow
{


    class PlayerManager
    {
        public Player[] playerArray;
        public static Keys[][] validKeys =  {
            new []{ Keys.W, Keys.A, Keys.D }, 
            new [] { Keys.Up, Keys.Left, Keys.Right }};
        public static float gravity = 0.1f;

        private PlayerManager()
        {

        }
        static PlayerManager instance;

        public static PlayerManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new PlayerManager();

                return instance;
            }
        }
    }
}
