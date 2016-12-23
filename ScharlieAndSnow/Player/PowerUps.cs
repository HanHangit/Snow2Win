using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScharlieAndSnow
{
    class PowerUp
    {
        protected Texture2D text;
        public Vector2 position;
        public bool onPlayer, temporaer;
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


            onPlayer = false;
            text = _text;
            position = _position;
        }

        //Das PowerUp wird dem jeweiligem Spieler zugeordnet
        public void ApplyToPlayer(Player player)
        {
            onPlayer = true;
            player.ApplyPowerUp(this);
        }

        public virtual void Update(GameTime gTime)
        {
            if (!onPlayer)
            {
                foreach (Player ply in PlayerManager.Instance.playerArray)
                {
                    if (ply.Bounds.Intersects(new Rectangle(position.ToPoint(), text.Bounds.Size)))
                    {
                        ApplyToPlayer(ply);
                        break;
                    }
                }
            }

            if (onPlayer && temporaer)
            {
                timer -= (float)gTime.ElapsedGameTime.TotalSeconds;
                if (timer <= 0)
                    alive = false;
            }
        }

        public virtual void Draw(SpriteBatch batch)
        {
            if (!onPlayer)
            {
                
                batch.Draw(text, position, Color.White);
            }
        }
    }
}
