using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScharlieAndSnow
{
    public class MyButton
    {

        int buttonX, buttonY, width, height;
        string name;
        Texture2D texture;
        SpriteFont fnd;
        Color color;
        bool isTouching;
        bool isClicking;


        public int ButtonX{ get { return buttonX; } }
        public int ButtonY { get { return buttonY; } }

        public MyButton(string _name, int _width, int _height, Vector2 _vec, SpriteFont _font, Color _color)
        {
            name = _name;
            width = _width;
            height = _height;
            buttonX = (int)Constant.PercentFromWindow(_vec.X, 0);
            buttonY = (int)Constant.PercentFromWindow(_vec.Y, 1);
            fnd = _font;
            color = _color;
            isClicking = false;
            isTouching = false;

            texture = new Texture2D(GraphicStuff.Instance.graphicDevice, width, height);
            Color[] data1 = new Color[texture.Width * texture.Height];
            for (int i = 0; i < data1.Length; ++i) data1[i] = color;
            texture.SetData(data1);

        }
        public MyButton(string name, Texture2D texture, Vector2 _vec, SpriteFont font)
        {
            fnd = font;
            this.name = name;
            this.texture = texture;
            buttonX = (int)Constant.PercentFromWindow(_vec.X, 0);
            buttonY = (int)Constant.PercentFromWindow(_vec.Y, 1);
            isTouching = false;
            isClicking = false;
        }

        public bool EnterButton()
        {
            var mouseState = Mouse.GetState();
            if (mouseState.X < buttonX + texture.Width &&
                    mouseState.X > buttonX &&
                    mouseState.Y < buttonY + texture.Height &&
                    mouseState.Y > buttonY)
            {
                isTouching = true;
                return true;
            }
            isTouching = false;
            return false;
        }

        public void Update(GameTime gameTime)
        {


        }
        public bool isClick()
        {
            var mouseState = Mouse.GetState();
            bool isInputPressed = false;
            isInputPressed = mouseState.LeftButton == ButtonState.Pressed;

            if (EnterButton() && isInputPressed)
            {
                isClicking = true;
                return true;

            }
            else
            {
                isClicking = false;
                return false;
            }

        }
        //ToDO Text margin
        public void Draw(SpriteBatch spriteBatch)
        {
            Color color = Color.White;
            if (isTouching) {
                color = Color.Yellow;
                if (isClicking) color = Color.Red;
            }

            spriteBatch.Draw(texture, new Rectangle(ButtonX, ButtonY, texture.Width, texture.Height), color);
            /*ToDo
             * ---------------
             * |    ----    |
             * |    |   |   |
             * |    |   |   |
             * |    -----   |
             * --------------
             * Im zweiten Rechteck den Padding bereich Prozentual noch definieren
             * http://www.avajava.com/tutorials/cascading-style-sheets/how-are-margins-borders-padding-and-content-related/how-are-margins-borders-padding-and-content-related-01.gif
             * z.B Oben 5%, links 5% etc
             */

            spriteBatch.DrawString(fnd, name, new Vector2(ButtonX + (int)Constant.PercentFromValue(2, ButtonX), ButtonY + (int)Constant.PercentFromValue(1, ButtonY)), Color.White);
        }
    }
}