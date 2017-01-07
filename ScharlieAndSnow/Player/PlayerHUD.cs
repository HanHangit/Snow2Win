
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ScharlieAndSnow
{
    class PlayerHUD
    {
        #region Variable

        float currentTemperatur;
        int currentSnowball;
        int maxSnowball;
        float maxtemperatur;
        int playerId;
        //PlayerTemperatur
        Rectangle rectanglePlayer;
        Rectangle updateRectangelPlayer;
        Rectangle sourceRectanglePlayer;
        int debugValue = 150;
        //PlayerSnowball
        Rectangle rectangleSnow;
        Rectangle updateRectangelSnow;
        Rectangle sourceRectangleSnow;
        int debugValuesnowball = 50;

        Texture2D playerBackground;
        Texture2D playerFront;
        Texture2D snowballBackGround;
        Texture2D snowBallFront;

        #endregion


        public PlayerHUD(int id) {
            maxtemperatur = GameInformation.Instance.playerInformation.maxHealth;
            maxSnowball = GameInformation.Instance.playerInformation.maxAmountSnowball;
            currentTemperatur = maxtemperatur;
            playerId = id;

          
            //LoadTexture
            playerBackground = MyContentManager.GetTexture(MyContentManager.TextureName.HUDBackPlayer);
            playerFront = MyContentManager.GetTexture(MyContentManager.TextureName.HUDFrontPlayer);
            snowballBackGround = MyContentManager.GetTexture(MyContentManager.TextureName.HUDSnowBack);
            snowBallFront = MyContentManager.GetTexture(MyContentManager.TextureName.HUDSnowFront);


            //RectangleSnow
            rectangleSnow = new Rectangle((int)GameInformation.Instance.mapInformation.PositionHUD[playerId].X, (int)GameInformation.Instance.mapInformation.PositionHUD[playerId].Y,
                            debugValuesnowball, debugValuesnowball);//snowballBackGround.Width, snowballBackGround.Height);
            updateRectangelSnow = new Rectangle((int)GameInformation.Instance.mapInformation.PositionHUD[playerId].X, (int)GameInformation.Instance.mapInformation.PositionHUD[playerId].Y,
                                debugValuesnowball, debugValuesnowball);
            sourceRectangleSnow = new Rectangle(0, 0, snowBallFront.Width, snowBallFront.Height); ;

            //RectanglePlayer
            rectanglePlayer = new Rectangle((int)GameInformation.Instance.mapInformation.PositionHUD[playerId].X, (int)GameInformation.Instance.mapInformation.PositionHUD[playerId].Y,
                                debugValue, debugValue);//playerBackground.Width, playerBackground.Height);
            updateRectangelPlayer = new Rectangle((int)GameInformation.Instance.mapInformation.PositionHUD[playerId].X, (int)GameInformation.Instance.mapInformation.PositionHUD[playerId].Y,
                                debugValue, debugValue);
            sourceRectanglePlayer = new Rectangle(0, 0, playerFront.Width, playerFront.Height);


        }

        public void Update(GameTime gameTime, float _currentTemperatur, int _currentSnowball)
        {
            this.currentSnowball = _currentSnowball;

            this.currentTemperatur = _currentTemperatur;


            KeyboardState newState = Keyboard.GetState();
            if (newState.IsKeyDown(Keys.K))
            {
                currentTemperatur--;
                Console.WriteLine("BLA");
            }

            CheckTemperatur();
            CheckSnowball();


        }
        public void CheckTemperatur()
        {
            float diff = currentTemperatur / maxtemperatur;
            updateRectangelPlayer = new Rectangle((int)GameInformation.Instance.mapInformation.PositionHUD[playerId].X, (int)(GameInformation.Instance.mapInformation.PositionHUD[playerId].Y + debugValue - (debugValue * diff)),
                                        debugValue, (int)(debugValue * diff));
            sourceRectanglePlayer = new Rectangle(0, playerFront.Height - (int)(playerFront.Height * diff), playerFront.Width, (int)(playerFront.Height * diff));
        }
        public void CheckSnowball()
        {
            float diff = (float)currentSnowball / (float)maxSnowball;
            updateRectangelSnow = new Rectangle((int)GameInformation.Instance.mapInformation.PositionHUD[playerId].X, (int)(GameInformation.Instance.mapInformation.PositionHUD[playerId].Y + debugValuesnowball - (debugValuesnowball * diff)),
                                        debugValuesnowball, (int)(debugValuesnowball * diff)); 
            sourceRectangleSnow = new Rectangle(0, snowBallFront.Height - (int)(snowBallFront.Height * diff), snowBallFront.Width, (int)(snowBallFront.Height * diff)); ;
        }
        public void Draw(SpriteBatch spriteBatch) {
            
            spriteBatch.Draw(playerBackground, rectanglePlayer, Color.White);

            spriteBatch.Draw(playerFront, updateRectangelPlayer, sourceRectanglePlayer, Color.White);

            spriteBatch.Draw(snowballBackGround, rectangleSnow, Color.White);
            spriteBatch.Draw(snowBallFront, updateRectangelSnow, sourceRectangleSnow, Color.White);
            


        }



    }
}