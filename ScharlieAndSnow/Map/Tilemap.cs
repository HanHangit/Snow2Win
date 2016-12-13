﻿using System;
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

        public Particle[,] snowTiles;
        public List<Particle> particles = new List<Particle>();

        public Tilemap(Texture2D[] textures, Texture2D bitMap, int _tileSize)
        {
            particles.Capacity = 10000;
            stop = new Stopwatch();
            snowTexture = new Texture2D(GraphicStuff.Instance.graphicDevice, bitMap.Width * _tileSize, bitMap.Height * _tileSize);
            tileTexture = textures;
            tileSize = _tileSize;
            tileMap = new Tile[bitMap.Width, bitMap.Height];

            size = new Point(bitMap.Width, bitMap.Height);
            realSize = new Point(bitMap.Width * tileSize, bitMap.Height * tileSize);

            snowTiles = new Particle[bitMap.Width * tileSize, bitMap.Height * tileSize];

            snowTexture = new Texture2D(textures[0].GraphicsDevice, bitMap.Width * tileSize, bitMap.Height * tileSize);

            BuildMap(textures, bitMap);
        }

        private void BuildMap(Texture2D[] textures, Texture2D bitMap)
        {
            Color[] colores = new Color[bitMap.Width * bitMap.Height];

            Color[] clrTexture = new Color[realSize.X * realSize.Y];

            

            bitMap.GetData(colores);

            for (int y = 0; y < tileMap.GetLength(1); y++)
            {
                for (int x = 0; x < tileMap.GetLength(0); x++)
                {
                    if (colores[y * tileMap.GetLength(0) + x] == Color.White)
                    {
                        // Grass
                        tileMap[x, y] = new Tile(textures[0], new Vector2(x * tileSize, y * tileSize), 0);
                    }
                    else
                    {
                        // Stein
                        tileMap[x, y] = new Tile(textures[1], new Vector2(x * tileSize, y * tileSize), 1);
                    }
                    /*
                    Color[] tileColor = new Color[tileSize * tileSize];

                    tileMap[x, y].texture.GetData(tileColor);

                    snowTexture.SetData(0,new Rectangle(x*tileSize,y*tileSize,tileSize,tileSize),tileColor, 0, tileSize * tileSize);

                    if(x % bitMap.Width == 0)
                        Console.WriteLine((int)(100 * (x % bitMap.Width + y * bitMap.Width) / (float)(bitMap.Width * bitMap.Height)));
                    */
                }
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

                return tileMap[(int)(currentPosition.X / tileSize), (int)(currentPosition.Y / tileSize)].Walkable();
            }
            catch(IndexOutOfRangeException)
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
            if (CheckSnow(pos))
                return snowTiles[(int)pos.X, (int)pos.Y];
            else
                return null;
        }

        /// <summary>
        /// Fügt den Partikel zur SchneeMap hinzu.
        /// Er liegt dann auf dem Boden.
        /// Wenn Masse >5, dann zersplittert der Partikel in weitere Partikel.
        /// </summary>
        /// <param name="p"></param>
        public void AddSnow( Particle p)
        {
            Vector2 position = p.position;
            p.alive = false;

            int numberOfSnow = (int)p.mass;

            try
            {
                snowTiles[(int)position.X, (int)position.Y] = p;
                particles.Add(p);
            }
            catch(IndexOutOfRangeException)
            {

            }


            p.radius = 1;

            if (numberOfSnow <= 5)
                return;

            for (int i = 0; i < numberOfSnow; ++i)
            {
                Vector2 move = new Vector2(0, -1);
                move = MyRectangle.rotate(move, MathHelper.ToRadians(MapStuff.Instance.rnd.Next(-30,30)));
                MapStuff.Instance.partCollHandler.AddParticle(position + new Vector2(0,-10), 1, 1, move);
            }
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            for (int x = 0; x < tileMap.GetLength(0); x++)
            {
                for (int y = 0; y < tileMap.GetLength(1); y++)
                {
                    tileMap[x, y].Draw(spriteBatch);
                }
            }
            

            //spriteBatch.Draw(snowTexture,Vector2.Zero,Color.White);

            
            foreach (Particle p in particles)
            {
                if(p != null)
                    p.Draw(spriteBatch);
            }
            

        }
    }
}
