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

            partSpace = 32;

            gridCell = new Job_Collision[(MapStuff.Instance.map.realSize.X / partSpace) * (MapStuff.Instance.map.realSize.X / partSpace)];

            for (int i = 0; i < gridCell.GetLength(0); ++i)
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

            int x = MathHelper.Clamp((int)p.position.X / partSpace, 0, gridCell.Length - 1);
            int y = MathHelper.Clamp((int)p.position.Y / partSpace, 0, gridCell.Length - 1);

            gridCell[x % partSpace + y * partSpace].AddParticle(p);
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

            //Some Debugging
            for (int i = 0; i < gridCell.GetLength(0); ++i)
            {
                if (gridCell[i].hasParticles)
                {
                    spriteBatch.Draw(textParticle, new Vector2(i % partSpace * partSpace, i / partSpace * partSpace), Color.White);
                    spriteBatch.Draw(textParticle, new Vector2(i % partSpace * partSpace + partSpace, i / partSpace * partSpace), Color.White);
                    spriteBatch.Draw(textParticle, new Vector2(i % partSpace * partSpace + partSpace, i / partSpace * partSpace + partSpace), Color.White);
                    spriteBatch.Draw(textParticle, new Vector2(i % partSpace * partSpace, i / partSpace * partSpace + partSpace), Color.White);
                }

                gridCell[i].hasParticles = false;
            }
        }
    }
}
