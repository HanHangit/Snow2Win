using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScharlieAndSnow
{
    class PowerUpSpawner
    {
        List<Tile> spawnTiles;
        List<PowerUp> powerUpList = new List<PowerUp>();
        List<Tile> powerUpTiles = new List<Tile>();
        float timer;
        float maxTimer;

        public PowerUpSpawner(List<Tile> _powerUpTiles)
        {
            spawnTiles = _powerUpTiles;
            timer = 0;
            maxTimer = GameInformation.Instance.mapInformation.powerUpTimer;
        }

        public void Update(GameTime gTime)
        {
            timer += (float)gTime.ElapsedGameTime.TotalSeconds;

            for (int i = 0; i < powerUpList.Count; ++i)
            {
                if (powerUpList[i].onPlayer)
                {
                    powerUpTiles[i].hasPowerUp = false;
                    powerUpTiles.RemoveAt(i);
                    powerUpList.RemoveAt(i--);
                }
                else
                    powerUpList[i].Update(gTime);
            }


            if (timer >= maxTimer)
            {
                timer -= maxTimer;
                Tile t = spawnTiles.Find(x => !x.hasPowerUp);
                if (t == null)
                    return;
                powerUpTiles.Add(t);
                powerUpTiles.Last().hasPowerUp = true;
                Vector2 RandomPos = powerUpTiles.Last().position;
                powerUpList.Add(new PowerUp(MyContentManager.GetTexture(MyContentManager.TextureName.Tree01), RandomPos, 30, new PlayerModifikator(30, 30, 20, 10)));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (PowerUp p in powerUpList)
                p.Draw(spriteBatch);
        }
    }
}
