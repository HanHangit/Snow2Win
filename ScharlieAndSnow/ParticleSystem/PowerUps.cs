using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScharlieAndSnow
{
    abstract class PowerUp
    {
        protected Texture2D text;
        public Vector2 position;
        bool onPlayer, temporaer;
        public bool alive;
        float timer;
        public PlayerModifikator mode;

        public PowerUp(Texture2D _text, Vector2 _position, float _Duration, PlayerModifikator _mode)
        {
            mode = _mode;
            alive = true;
            timer = _Duration;
            if (_Duration == 0)
                temporaer = false;
            else
                temporaer = true;



            text = _text;
            position = _position;
        }

        //Das PowerUp wird dem jeweiligem Spieler zugeordnet
        public abstract void ApplyToPlayer(Player player);

        public virtual void Update(GameTime gTime)
        {
            if(onPlayer && temporaer)
            {
                timer -= (float)gTime.ElapsedGameTime.TotalSeconds;
                if (timer <= 0)
                    alive = false;
            }
        }

        public virtual void Draw(SpriteBatch batch)
        {
            if (!onPlayer)
                batch.Draw(text, position, Color.White);
        }
    }
}
