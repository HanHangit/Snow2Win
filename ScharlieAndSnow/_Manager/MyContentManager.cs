using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace ScharlieAndSnow
{
    public class MyContentManager
    {
        private static Dictionary<TextureName, Texture2D> textureDictionary = new Dictionary<TextureName, Texture2D>();
        private static Dictionary<FontName, SpriteFont> fontDictionary = new Dictionary<FontName, SpriteFont>();
        static ContentManager Content;

        public MyContentManager(ContentManager _content)
        {
            Content = _content;

        }
        public static Texture2D GetTexture(TextureName textureName)
        {
            if (textureDictionary.Count == 0)
            {
                LoadTexture();
            }
            return textureDictionary[textureName];
        }
        public static SpriteFont GetFont(FontName FontName)
        {

            if (fontDictionary.Count == 0)
            {

                LoadFont();
            }
            return fontDictionary[FontName];
        }

        static void LoadTexture()
        {
            textureDictionary.Add(TextureName.Tree01, Content.Load<Texture2D>("Tree01"));
            textureDictionary.Add(TextureName.SkyTile, Content.Load<Texture2D>("SkyTile"));
            textureDictionary.Add(TextureName.SnowTile, Content.Load<Texture2D>("SnowTile"));
            textureDictionary.Add(TextureName.SnowTile_down, Content.Load<Texture2D>("MapTiles/SnowTile_down"));
            textureDictionary.Add(TextureName.SnowTile_up, Content.Load<Texture2D>("MapTiles/SnowTile_Up"));
            textureDictionary.Add(TextureName.Map02, Content.Load<Texture2D>("Map03"));
            textureDictionary.Add(TextureName.Clouds, Content.Load<Texture2D>("Clouds"));
            textureDictionary.Add(TextureName.SnowBall, Content.Load<Texture2D>("Schneeball"));
            textureDictionary.Add(TextureName.Player1, Content.Load<Texture2D>("Player1"));
            textureDictionary.Add(TextureName.GoBUtton, Content.Load <Texture2D>("PlayButton"));
            textureDictionary.Add(TextureName.BackGround, Content.Load<Texture2D>("BackGround"));
            textureDictionary.Add(TextureName.ExitButton, Content.Load<Texture2D>("ExitButton"));
            textureDictionary.Add(TextureName.BackGround_1270x720, Content.Load<Texture2D>("BackGround_1270x720"));
            textureDictionary.Add(TextureName.littleBird, Content.Load<Texture2D>("SnowBird"));
            textureDictionary.Add(TextureName.mage, Content.Load<Texture2D>("MoveMent"));

        }
        static void LoadFont()
        {
            fontDictionary.Add(FontName.Arial, Content.Load<SpriteFont>("FPS"));
        }


        public enum TextureName
        {
            SkyTile,
            SnowTile,
            SnowTile_down,
            SnowTile_up,
            Map02,
            Clouds,
            SnowBall,
            Player1,
            GoBUtton,
            ExitButton,
            BackGround,
            BackGround_1270x720,
            Tree01,
            littleBird,
            mage
                
        }
        public enum FontName
        {
            Arial,

        }

    }
}
