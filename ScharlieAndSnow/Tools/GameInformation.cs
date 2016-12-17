using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScharlieAndSnow
{
    class PlayerInformation
    {
        public float speed = 1;
        public float jumpSpeed = 4;
        public float maxHealth = 100;
        public int particleForSnowball = 10;
        public Vector2 snowballMove = new Vector2(10, -3);
    }
    class GameInformation
    {


        public PlayerInformation playerInformation = new PlayerInformation();
        static GameInformation instance;
        public static GameInformation Instance
        {
            get
            {
                if (instance == null)
                    instance = new GameInformation();

                return instance;
            }
        }
    }
}
