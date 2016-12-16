using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ScharlieAndSnow
{
    class GlobalParticleHandler
    {

        Texture2D text;
        ParticleCollisionHandler partHandler;
        public List<Particle> particles;
        float timer = 0f;
        float maxTimer = 0.1f;
        JobParticleDelete jopParticleDelete;
        Stopwatch stop;


        /// <summary>
        /// Ein globales Konstrukt, um eine Anzahl von Partikel und deren Collision
        /// zu behandeln.
        /// </summary>
        /// <param name="_number"></param>
        /// <param name="_text"></param>
        public GlobalParticleHandler(Texture2D _text)
        {
            stop = new Stopwatch();
            jopParticleDelete = new JobParticleDelete();
            particles = new List<Particle>();
            text = _text;
            partHandler = new ParticleCollisionHandler(_text);
        }

        /// <summary>
        /// Fügt ein Partikel der Liste hinzu.
        /// Dieses Partikel wird auf Collision mit anderen Partikel und mit der Map
        /// überprüft.
        /// </summary>
        /// <param name="_position">Position</param>
        /// <param name="_mass">Masse des Partikel</param>
        /// <param name="_radius">Radius des Partikel</param>
        /// <param name="move">Move bzw. seine derzeitige "Kraft"</param>
        public void AddParticle(Vector2 _position, float _mass, float _radius, Vector2 move)
        {
            particles.Add(new Particle(text, _position + move, _mass, _radius, move));
        }

        public void Update(GameTime gTime)
        {

            //Just for Debugging
            /*
            timer -= (float)gTime.ElapsedGameTime.TotalSeconds;

            if (timer <= 0)
            {
                timer = maxTimer;
                Vector2 move = new Vector2(0,1);

                for (int i = 0; i < 10; ++i)
                {
                    move = MyRectangle.rotate(move, MathHelper.ToRadians(MapStuff.Instance.rnd.Next(360)));
                    particles.Add(new Particle(text, new Vector2(200 + 100 * i, 50), 7, 2, move));
                }

            }
            */

            //Update Partikels und fügt es dem Collision-Gitter hinzu
            foreach (Particle p in particles)
            {
                if (p == null ||  !p.alive)
                    continue;
                p.Update(gTime);

                partHandler.AddParticle(p);
            }


            //Startet den Task, um nicht mehr lebendige Partikel zu entfernen.
            Task delete = Task.Factory.StartNew(() => jopParticleDelete.DeleteParticle());

            //Startet den Task, um die Collisions zu behandeln.
            Task collider = Task.Factory.StartNew(() => partHandler.Update(gTime));


            //Wartet auf Beendigung der Tasks
            collider.Wait();
            delete.Wait();

        }

        public void Draw(SpriteBatch spriteBatch)
        {

            
            //zeichnet alle Partikel
            foreach (Particle p in particles)
            {
                if(p != null)
                    p.Draw(spriteBatch);
            }
            
            //partHandler.Draw(spriteBatch);
            
        }
    }
}
