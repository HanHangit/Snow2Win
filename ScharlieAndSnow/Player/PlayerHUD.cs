
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ScharlieAndSnow
{
    class PlayerHUD
    {
        float currentTemperatur;
        float maxtemperatur;
        int playerId;
        Vector2 pos;
        Rectangle rectanglePlayer;
        int percentPlayerX;
        int percentPlayerY;
        Rectangle rectangleSnow;

        Texture2D playerBackground;
        Texture2D playerFront;

        Texture2D snowballBackGround;
        Texture2D snowBallFront;

        public PlayerHUD(int id) {
            maxtemperatur = GameInformation.Instance.playerInformation.maxHealth;
            currentTemperatur = maxtemperatur;
            playerId = id;

          

            playerBackground = MyContentManager.GetTexture(MyContentManager.TextureName.HUDBackPlayer);
            playerFront = MyContentManager.GetTexture(MyContentManager.TextureName.HUDFrontPlayer);

            snowballBackGround = MyContentManager.GetTexture(MyContentManager.TextureName.HUDSnowBack);
            snowBallFront = MyContentManager.GetTexture(MyContentManager.TextureName.HUDSnowFront);

            rectanglePlayer = new Rectangle((int)GameInformation.Instance.mapInformation.PositionHUD[playerId].X, (int)GameInformation.Instance.mapInformation.PositionHUD[playerId].Y,
                    playerBackground.Width, playerBackground.Height);
            rectangleSnow = new Rectangle((int)GameInformation.Instance.mapInformation.PositionHUD[playerId].X, (int)GameInformation.Instance.mapInformation.PositionHUD[playerId].Y,
                                snowballBackGround.Width, snowballBackGround.Height);

            percentPlayerX = (int)Constant.PercentFromValue(GameInformation.Instance.mapInformation.PositionHUD[playerId].X, Constant.x);
            percentPlayerY = (int)Constant.PercentFromValue(GameInformation.Instance.mapInformation.PositionHUD[playerId].Y, Constant.y);



        }

        public void Update(GameTime gameTime, float _currentTemperatur)
        {
            //pos = GUIStuff.Instance.camera.Bounds.Width;
            rectanglePlayer = new Rectangle((int)Constant.PercentFromValue(percentPlayerX, GUIStuff.Instance.camera.Bounds.Width), (int)Constant.PercentFromValue(percentPlayerY, GUIStuff.Instance.camera.Bounds.Height),
                                playerBackground.Width, playerBackground.Height);
            this.currentTemperatur = _currentTemperatur;
            //rectanglePlayer = new Rectangle()
            


        }
        public void Draw(SpriteBatch spriteBatch) {
            spriteBatch.Draw(playerBackground, rectanglePlayer, Color.White);
            spriteBatch.Draw(playerFront, rectanglePlayer, Color.White);
            //
            //spriteBatch.Draw(snowballBackGround, GameInformation.Instance.mapInformation.PositionHUD[playerId]);
            //spriteBatch.Draw(snowBallFront, GameInformation.Instance.mapInformation.PositionHUD[playerId]);


        }



    }
}