using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScharlieAndSnow
{

    enum EGameState
    {
        None = -1,
        Mainmenu,
        PlayState,
        CreateGame,
    }

    interface IGameState
    {

        void Initialize();
        void LoadContent();
        void UnloadContent();
        void Draw(SpriteBatch spriteBatch);
        EGameState Update(GameTime gTime);

    }
}
