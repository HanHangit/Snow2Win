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


        public int ButtonX
        {
            get
            {
                return buttonX;
            }
        }

        public int ButtonY
        {
            get
            {
                return buttonY;
            }
        }

        public MyButton(string _name, int _width, int _height, Vector2 _vec, SpriteFont _font, Color _color)
        {
            name = _name;
            width = _width;
            height = _height;
            buttonX = (int)_vec.X;
            buttonY = (int)_vec.Y;
            fnd = _font;
            color = _color;

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
            this.buttonX = (int)_vec.X;
            this.buttonY = (int)_vec.Y;
        }

        /**
         * @return true: If a player enters the button with mouse
         */
        public bool EnterButton()
        {
            var mouseState = Mouse.GetState();
            if (mouseState.X < buttonX + texture.Width &&
                    mouseState.X > buttonX &&
                    mouseState.Y < buttonY + texture.Height &&
                    mouseState.Y > buttonY)
            {
                return true;
            }
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
                return true;

            }
            else
                return false;
        }
        //ToDO Text margin
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)ButtonX, (int)ButtonY, texture.Width, texture.Height), Color.White);


            //spriteBatch.Draw(texture, new Rectangle(Constant.Calculate(20,20, width, height).ToPoint(), new Point(width,height)), Color.White);
            //spriteBatch.DrawString(fnd, name, new Vector2((int)ButtonX, (int)ButtonY), Color.White);
            
            spriteBatch.DrawString(fnd, name, new Vector2(Constant.Calculate(4f, 2f, ButtonX, ButtonY).X + buttonX, Constant.Calculate(4f, 0.5f, ButtonX, ButtonY).Y + ButtonY), Color.White);
        }
    }
}