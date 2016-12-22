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
        public float speed = 5;
        public float jumpSpeed = 4;
        public float maxHealth = 100;
        public int particleForSnowball = 10;
        public Vector2 snowballMove = new Vector2(10, -3);
        public float attackSpeed = 0.3f; //Seconds
    }

    class CameraInformation
    {
        public float speed = 0.1f; //For Vector.Lerp (Range 0-1)
        public int offsetX = 200;
        public int offsetY = 200;
    }

    class SnowballInformation
    {
        public float damage = 5f;
    }
    class GameInformation
    {


        public PlayerInformation playerInformation = new PlayerInformation();
        public CameraInformation cameraInformation = new CameraInformation();
        public SnowballInformation snowballInformation = new SnowballInformation();

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
