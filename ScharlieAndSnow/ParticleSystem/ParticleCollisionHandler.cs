using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScharlieAndSnow
{
    class ParticleCollisionHandler
    {
        Texture2D textParticle;
        Job_Collision[] gridCell;
        int partSpace;
        System.Diagnostics.Stopwatch stop;

        /// <summary>
        /// Beinhaltet konkret das GitterNetz für die Collisions-Abfragen.
        /// </summary>
        /// <param name="text">Just for Debugging</param>
        public ParticleCollisionHandler(Texture2D text)
        {
            stop = new System.Diagnostics.Stopwatch();

            partSpace = 8;

            gridCell = new Job_Collision[((MapStuff.Instance.map.realSize.X / partSpace) + 1) *(1 + (MapStuff.Instance.map.realSize.Y / partSpace))];

            for (int i = 0; i < gridCell.Length; ++i)
            {
                gridCell[i] = new Job_Collision();
            }

            textParticle = text;
        }

        /// <summary>
        /// Fügt ein Partikel dem Gitternetz hinzu.
        /// </summary>
        /// <param name="p">Das Partikel</param>
        public void AddParticle(Particle p)
        {
            Point size = MapStuff.Instance.map.realSize;

            int x = MathHelper.Clamp((int)p.position.X / partSpace, 0, size.X / partSpace);
            int y = MathHelper.Clamp((int)p.position.Y / partSpace, 0, size.X / partSpace);

            gridCell[x + y * (size.X / partSpace)].AddParticle(p);
        }

        public void Update(GameTime gTime)
        {


            //Startet, alle Jobs - für die Collisions-Abfragen - von Zellen, die Partikel beinhalten.
            Task collider = Task.Factory.StartNew(() => Parallel.ForEach(gridCell, item =>
            {

                if (item.hasParticles)
                    item.CalculateCollision();
            }));


            //Warten auf Beendigung
            collider.Wait();

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Point mapSize = MapStuff.Instance.map.realSize;

            //Some Debugging
            for (int i = 0; i < gridCell.Length; ++i)
            {
                if (gridCell[i].hasParticles)
                {
                    Vector2 position = new Vector2(i * partSpace % mapSize.X, ((i * partSpace) / mapSize.X) * partSpace);
                    //Console.WriteLine(position);
                    spriteBatch.Draw(textParticle, position + new Vector2(partSpace,partSpace), Color.White);
                    spriteBatch.Draw(textParticle, position + new Vector2(0, partSpace), Color.White);
                    spriteBatch.Draw(textParticle, position + new Vector2(partSpace, 0), Color.White);
                    spriteBatch.Draw(textParticle, position , Color.White);
                }

                gridCell[i].hasParticles = false;
            }
        }
    }
}
