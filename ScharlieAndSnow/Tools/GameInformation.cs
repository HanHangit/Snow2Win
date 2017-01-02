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
        public Vector2 snowballMove = new Vector2(10, 0);
        public float attackSpeed = 0.3f; //Seconds
    }

    class MapInformation
    {
        public float powerUpTimer = 20;
        public float cloudSpawnTime = 20;
        public int maxAmountClouds = 5;
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
        public int size = 6;
    }
    class GameInformation
    {


        public PlayerInformation playerInformation = new PlayerInformation();
        public CameraInformation cameraInformation = new CameraInformation();
        public SnowballInformation snowballInformation = new SnowballInformation();
        public MapInformation mapInformation = new MapInformation();

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
