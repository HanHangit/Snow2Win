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
        public float speed = 3;
        public float jumpSpeed = 7f;
        public float maxHealth = 100;
        public int particleForSnowball = 10;
        public Vector2 snowballMove = new Vector2(15, 0);
        public float attackSpeed = 0.3f; //Seconds
    }

    class MapInformation
    {
        public float powerUpTimer = 20;
        public float cloudSpawnTime = 20;
        public int maxAmountClouds = 5;
        public Vector2 gravity = new Vector2(0, 0.16f);
        public float drag = 0.001f;
        //ToDo Berechnen der Positionen der HUD abhängig der Spieleranzahl!
        public Vector2[] PositionHUD = {new Vector2(100,420), new Vector2( 1000, 420)};
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
        public int size = 10;
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
