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
        float timer;
        float maxTimer;
        int maxClouds;

        public CloudSpawner()
        {
            timer = 0;
            maxTimer = GameInformation.Instance.mapInformation.cloudSpawnTime;
            maxClouds = GameInformation.Instance.mapInformation.maxAmountClouds;
            Texture2D text = MyContentManager.GetTexture(MyContentManager.TextureName.Clouds);
            cloudList.Add(new Clouds(text, new Vector2(100, 50), new Vector2(1f, 0), 1, 0.1f, 300, 10, 6));
            cloudList.Add(new Clouds(text, new Vector2(500, 40), new Vector2(1f, 0), 1, 0.01f, 20, 7, 6));
            cloudList.Add(new Clouds(text, new Vector2(400, 20), new Vector2(4f, 0), 1, 0.2f, 50, 6, 6));
            cloudList.Add(new Clouds(text, new Vector2(700, 80), new Vector2(2f, 0), 1, 0.07f, 180, 13, 6));
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
