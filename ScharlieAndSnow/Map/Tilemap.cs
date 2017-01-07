using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace ScharlieAndSnow
{
    class Tilemap
    {
        Tile[,] tileMap;
        int tileSize;
        public Texture2D[] tileTexture;
        public Texture2D snowTexture;
        public Point size;
        public Point realSize;
        Stopwatch stop;

        public RenderTarget2D rendTarget;

        public Color[] snowColor;

        public Particle[,] snowTiles;

        public List<Particle> particles = new List<Particle>();

        Texture2D[] textClouds;

        PowerUpSpawner powerUpSpawner;

        CloudSpawner cloudSpawner;

        public List<Tile> powerUpTiles;
        public List<Tile> cloudTiles;



        public Tilemap(int _tileSize)
        {
            powerUpTiles = new List<Tile>();
            cloudTiles = new List<Tile>();
            //Initialize Map
            tileTexture = new[]{ MyContentManager.GetTexture(MyContentManager.TextureName.SkyTile), MyContentManager.GetTexture(MyContentManager.TextureName.SnowTile),
                                    MyContentManager.GetTexture(MyContentManager.TextureName.SnowTile_down),MyContentManager.GetTexture(MyContentManager.TextureName.SnowTile_up)};
            Texture2D bitMap = MyContentManager.GetTexture(MyContentManager.TextureName.Map02);
            textClouds = new[]{ MyContentManager.GetTexture(MyContentManager.TextureName.Clouds) };

            snowColor = new Color[bitMap.Width * _tileSize + bitMap.Height * _tileSize];
            particles.Capacity = 10000;
            stop = new Stopwatch();
            snowTexture = new Texture2D(GraphicStuff.Instance.graphicDevice, bitMap.Width * _tileSize, bitMap.Height * _tileSize);
            tileSize = _tileSize;
            tileMap = new Tile[bitMap.Width, bitMap.Height];

            size = new Point(bitMap.Width, bitMap.Height);
            realSize = new Point(bitMap.Width * tileSize, bitMap.Height * tileSize);

            snowTiles = new Particle[bitMap.Width * tileSize, bitMap.Height * tileSize];

            rendTarget = new RenderTarget2D(GraphicStuff.Instance.graphicDevice, realSize.X, realSize.Y);

            BuildMap(tileTexture, bitMap);


            powerUpSpawner = new PowerUpSpawner(powerUpTiles);
            cloudSpawner = new CloudSpawner(cloudTiles);
        }

        private void BuildMap(Texture2D[] textures, Texture2D bitMap)
        {
            Color[] colores = new Color[bitMap.Width * bitMap.Height];

            Color[] clrTexture = new Color[realSize.X * realSize.Y];

            Color[][] tileColor = new Color[textures.Length][];

            //Wichtig!!
            int triangleOffset = -1;

            for (int i = 0; i < tileColor.Length; ++i)
            {
                tileColor[i] = new Color[tileSize * tileSize];
                textures[i].GetData(tileColor[i]);

                //Für die Dreiecke der TileMap
                if (i == 2 || i == 3)
                {
                    Triangle skyTriangle;
                    if (i == 2)
                        skyTriangle = new Triangle(new[] { new Vector2(triangleOffset, 0), new Vector2(tileSize - triangleOffset, tileSize - triangleOffset), new Vector2(triangleOffset, tileSize - triangleOffset) });
                    else
                        skyTriangle = new Triangle(new[] { new Vector2(tileSize, 0),
                                            new Vector2(tileSize, tileSize),
                                            new Vector2(0, tileSize) });
                    for (int j = 0; j < tileColor[i].Length; ++j)
                    {
                        if (!skyTriangle.intersect(new Vector2(j % tileSize, j / tileSize)))
                            tileColor[i][j] = tileColor[0][0];
                    }
                }
            }


            bitMap.GetData(colores);

            for (int x = 0; x < tileMap.GetLength(0); x++)
            {
                for (int y = 0; y < tileMap.GetLength(1); y++)
                {
                    if (colores[y * tileMap.GetLength(0) + x] == Color.White)
                    {
                        //Sky
                        tileMap[x, y] = new Tile(textures[0], new Vector2(x * tileSize, y * tileSize), ETile.Sky);
                        snowTexture.SetData(0, new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), tileColor[0], 0, tileSize * tileSize);
                    }
                    else if (colores[y * tileMap.GetLength(0) + x] == new Color(237,28,36))
                    {
                        //PowerUpSpawnTile
                        tileMap[x, y] = new Tile(textures[0], new Vector2(x * tileSize, y * tileSize), ETile.PowerUp);
                        snowTexture.SetData(0, new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), tileColor[0], 0, tileSize * tileSize);
                        powerUpTiles.Add(tileMap[x, y]);
                    }
                    else if (colores[y * tileMap.GetLength(0) + x] == new Color(0, 162, 232))
                    {
                        //CloudSpawnTile
                        tileMap[x, y] = new Tile(textures[0], new Vector2(x * tileSize, y * tileSize), ETile.Cloud);
                        snowTexture.SetData(0, new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), tileColor[0], 0, tileSize * tileSize);
                        cloudTiles.Add(tileMap[x, y]);
                    }
                    else
                    {
                        //Stone
                        tileMap[x, y] = new Tile(textures[1], new Vector2(x * tileSize, y * tileSize), ETile.Terrain);
                        snowTexture.SetData(0, new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), tileColor[1], 0, tileSize * tileSize);
                    }

                    if (x % bitMap.Width == 0)
                        Console.WriteLine((int)(100 * (x % bitMap.Width + y * bitMap.Width) / (float)(bitMap.Width * bitMap.Height)));

                }
            }

            for (int x = 1; x < tileMap.GetLength(0) - 1; x++)
            {
                for (int y = 1; y < tileMap.GetLength(1) - 1; y++)
                {
                    Vector2 position = new Vector2(x * tileSize, y * tileSize);
                    if (!tileMap[x - 1, y].Walkable()
                        && tileMap[x + 1, y].Walkable()
                        && tileMap[x, y].Walkable()
                        && !tileMap[x, y + 1].Walkable())
                    {
                        Triangle triangle = new Triangle(new[] {
                            new Vector2(triangleOffset + x * tileSize, y * tileSize),
                            new Vector2(tileSize - triangleOffset + x * tileSize,y * tileSize + tileSize - triangleOffset),
                            new Vector2(triangleOffset + x * tileSize,y * tileSize + tileSize - triangleOffset)
                        });
                        //Console.WriteLine("Position: " + new Vector2(x * tileSize, y * tileSize));
                        //Console.WriteLine(triangle.ToString());
                        tileMap[x, y] = new Tile(textures[2], new Vector2(x * tileSize, y * tileSize), triangle, ETile.Triangle);
                        snowTexture.SetData(0, new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), tileColor[2], 0, tileSize * tileSize);
                    }

                    else if (tileMap[x - 1, y].Walkable()
    && !tileMap[x + 1, y].Walkable()
    && tileMap[x, y].Walkable()
    && !tileMap[x, y + 1].Walkable())
                    {
                        Triangle triangle = new Triangle(new[] {
                            new Vector2(x * tileSize + tileSize, y * tileSize),
                            new Vector2(tileSize + x * tileSize,y * tileSize + tileSize ),
                            new Vector2(x * tileSize,y * tileSize + tileSize)
                        });
                        //Console.WriteLine("Position: " + new Vector2(x * tileSize, y * tileSize));
                        //Console.WriteLine(triangle.ToString());
                        tileMap[x, y] = new Tile(textures[3], new Vector2(x * tileSize, y * tileSize), triangle, ETile.Triangle);
                        snowTexture.SetData(0, new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize), tileColor[3], 0, tileSize * tileSize);
                    }
                }
            }


        }

        public void CreateDebugSnow()
        {
            for (int i = 0; i < 20; ++i)
            {
                int j = 0;
                while (tileMap[i, j].Walkable())
                {
                    ++j;
                }
                --j;
                for (int p = 0; p < 5; ++p)
                    for (int k = 0; k < tileSize; ++k)
                        for (int q = 0; q < tileSize; ++q)
                            AddSnow(new Particle(MyContentManager.GetTexture(MyContentManager.TextureName.SnowBall),
                                new Vector2((i - p) * tileSize + k, (j - p) * tileSize + q), 1, 1, Vector2.Zero));
            }

        }

        /// <summary>
        /// Prüft, ob die Position begehbar ist. (Snow + Map)
        /// </summary>
        /// <param name="currentPosition"></param>
        /// <returns></returns>
        public bool Walkable(Vector2 currentPosition)
        {

            if (currentPosition.X < 0 || currentPosition.X > tileMap.GetLength(0) * tileSize
                || currentPosition.Y < 0 || currentPosition.Y > tileMap.GetLength(1) * tileSize
                || currentPosition.X > snowTiles.GetLength(0) - 1 || currentPosition.Y > snowTiles.GetLength(1) - 1)
                return false;

            try
            {

                if (snowTiles[(int)currentPosition.X, (int)currentPosition.Y] != null)
                    return false;

                return tileMap[(int)(currentPosition.X / tileSize), (int)(currentPosition.Y / tileSize)].Walkable(currentPosition);
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
        }

        /// <summary>
        /// Prüft NUR, ob Schnee an der gegebenene Position ist.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        public bool CheckSnow(Vector2 pos)
        {
            if (!CheckPosition(pos))
                return false;

            return snowTiles[(int)pos.X, (int)pos.Y] != null;
        }


        /// <summary>
        /// Gibt den Partikel zurück, der an der gewissen Position liegt.
        ///
        /// </summary>
        /// <param name="pos"></param>
        /// <returns>Gibt Null zurück, wenn an der Position kein Schnee liegt.</returns>
        public Particle GetSnowParticle(Vector2 pos)
        {
            if (!CheckPosition(pos))
                return null;

            if (CheckSnow(pos))
                return snowTiles[(int)pos.X, (int)pos.Y];
            else
                return null;
        }

        public bool CollectSnow(Vector2 position, int radius)
        {
            for (int x = (int)position.X; x < position.X + radius + radius; ++x)
                for (int y = (int)position.Y; y < position.Y + radius + radius; ++y)
                {
                    if (CollectSnow(new Vector2(x, y)))
                        return true;
                }

            return false;
        }

        //Zum Schnee einsammeln von der MAP!
        public bool CollectSnow(Vector2 position)
        {
            if (!CheckPosition(position))
                return false;

            if (!CheckSnow(position))
                return false;

            rendTarget.SetData(0, new Rectangle((int)position.X, (int)position.Y, 2, 2), new[] { new Color(0, 0, 0, 0) }, 0, 1);
            snowTiles[(int)position.X, (int)position.Y] = null;

            if (CheckSnow(position + new Vector2(0, -1)))
            {
                AddSnowToMap(GetSnowParticle(position + new Vector2(0, -1)), position);
                CollectSnow(position + new Vector2(0, -1));
            }
            else
            {
                if (CheckSnow(position + new Vector2(-1, -1)))
                {
                    AddSnowToMap(GetSnowParticle(position - new Vector2(1, 1)), position);
                    CollectSnow(position + new Vector2(-1, -1));
                }
                if (CheckSnow(position + new Vector2(1, -1)))
                {
                    AddSnowToMap(GetSnowParticle(position + new Vector2(1, -1)), position);
                    CollectSnow(position + new Vector2(1, -1));
                }
            }
            return true;
        }

        public bool CheckPosition(Vector2 p)
        {
            if (float.IsNaN(p.X) || float.IsNaN(p.Y)
    || p.X >= snowTiles.GetLength(0) || p.Y >= snowTiles.GetLength(1)
    || p.X < 0 || p.Y < 0)
                return false;

            return true;
        }

        public bool CheckPosition(Particle p)
        {
            if (float.IsNaN(p.position.X) || float.IsNaN(p.position.Y)
    || p.position.X >= snowTiles.GetLength(0) || p.position.Y >= snowTiles.GetLength(1)
    || p.position.X < 0 || p.position.Y < 0)
                return false;

            return true;
        }
        public void AddParticle(Particle p )
        {

            while (MapStuff.Instance.map.Walkable(new Vector2(p.position.X, p.position.Y + 1))
                && CheckPosition(p.position))
                p.position.Y += 1;

            while (!MapStuff.Instance.map.Walkable(new Vector2(p.position.X, p.position.Y))
                && CheckPosition(p.position))
                p.position.Y -= 1;

            p.radius = 4;
            particles.Add(p);
        }

        public void AddSnowToMap(Particle p, Vector2 position)
        {

            try
            {
                snowTiles[(int)position.X, (int)position.Y] = p;
                rendTarget.SetData(0, new Rectangle((int)position.X, (int)position.Y, 2, 2), new[] { p.color, p.color, p.color, p.color }, 0, 1);
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine(position);
            }
        }


        /// <summary>
        /// Fügt den Partikel zur SchneeMap hinzu.
        /// Er liegt dann auf dem Boden.
        /// Wenn Masse >5, dann zersplittert der Partikel in weitere Partikel.
        /// Map-COllision
        /// </summary>
        /// <param name="p"></param>
        public void AddSnow(Particle p)
        {
            p.alive = false;

            if (!CheckPosition(p))
                return;

            Particle.SplitUpParticle(p);

            AddParticle(p);
        }


        public void AddSnow(Particle p, Vector2 position)
        {
            p.alive = false;

            if (!CheckPosition(p))
                return;

            AddParticle(p);

            int numberOfSnow = (int)p.mass;

            Particle.SplitUpParticle(p, new Rectangle(position.ToPoint(), new Point(1, 1)));
        }

        public void AddSnow(Particle p, Vector2 position, Rectangle collisionObject)
        {
            p.alive = false;

            if (!CheckPosition(p))
                return;

            AddParticle(p);

            int numberOfSnow = (int)p.mass;

            Particle.SplitUpParticle(p, collisionObject);
        }

        public void Update(GameTime gameTime)
        {

            powerUpSpawner.Update(gameTime);
            cloudSpawner.Update(gameTime);


            for(int i = 0; i < particles.Count; ++i)
            {
                if (!particles[i].snow)
                    particles[i].UpdateSnow();
                else
                {
                    AddSnowToMap(particles[i], particles[i].position);
                    particles.RemoveAt(i--);
                }
            }


        }

        public void Draw(SpriteBatch spriteBatch)
        {

            

            /*
            for (int x = 0; x < tileMap.GetLength(0); x++)
            {
                for (int y = 0; y < tileMap.GetLength(1); y++)
                {
                    tileMap[x, y].Draw(spriteBatch);
                }
            }
            */

            if (Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                CollectSnow(Mouse.GetState().Position.ToVector2() + GUIStuff.Instance.camera.position);
            }

            spriteBatch.Draw(snowTexture, Vector2.Zero, Color.White);

            cloudSpawner.Draw(spriteBatch);
            powerUpSpawner.Draw(spriteBatch);

            
            foreach (Particle p in particles)
            {
                if (p != null)
                    p.Draw(spriteBatch);
            }
            


        }

    }
}
