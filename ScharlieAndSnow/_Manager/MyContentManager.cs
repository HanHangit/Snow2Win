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
        public static Texture GetTexture(TextureName textureName)
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
            textureDictionary.Add(TextureName.SkyTile, Content.Load<Texture2D>("SkyTile"));
            textureDictionary.Add(TextureName.SnowTile, Content.Load<Texture2D>("SkyTile"));
            textureDictionary.Add(TextureName.SnowTile_down, Content.Load<Texture2D>("MapTiles/SnowTile_down"));
            textureDictionary.Add(TextureName.SnowTile_up, Content.Load<Texture2D>("MapTiles/SnowTile_Up"));
            textureDictionary.Add(TextureName.Map02, Content.Load<Texture2D>("Map02"));
            textureDictionary.Add(TextureName.Clouds, Content.Load<Texture2D>("Clouds"));
            textureDictionary.Add(TextureName.SnowBall, Content.Load<Texture2D>("SnowBall"));
            textureDictionary.Add(TextureName.Player1, Content.Load<Texture2D>("Player1"));

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
            Player1
        }
        public enum FontName
        {
            Arial,

        }

    }
}
