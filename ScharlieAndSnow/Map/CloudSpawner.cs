using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScharlieAndSnow
{
    class CloudSpawner
    {

        public List<Clouds> cloudList = new List<Clouds>();
        public List<Tile> spawnTiles;
        float timer;
        float maxTimer;
        int maxClouds;

        public CloudSpawner(List<Tile> _cloudTiles)
        {
            spawnTiles = _cloudTiles;
            timer = 0;
            maxTimer = GameInformation.Instance.mapInformation.cloudSpawnTime;
            maxClouds = GameInformation.Instance.mapInformation.maxAmountClouds;
            Texture2D text = MyContentManager.GetTexture(MyContentManager.TextureName.Clouds);
            for (int i = 0; i < maxClouds; ++i)
            {
                Vector2 spawnPosition = spawnTiles[MapStuff.Instance.rnd.Next(0, spawnTiles.Count)].position;
                cloudList.Add(new Clouds(text, spawnPosition, new Vector2(1f, 0), 1, 0.1f, 300, 9, 6));
            }
        }

        public void Update(GameTime gTime)
        {

            for (int i = 0; i < cloudList.Count; ++i)
            {
                if (cloudList[i].alive)
                    cloudList[i].Update(gTime);
                else
                    cloudList.RemoveAt(i--);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Clouds c in cloudList)
                c.Draw(spriteBatch);
        }

    }
}
