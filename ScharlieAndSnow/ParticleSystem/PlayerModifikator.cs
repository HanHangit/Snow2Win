using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScharlieAndSnow
{
    struct PlayerModifikator
    {
        public PlayerModifikator(float _speed, float _jump, float _armor, float _health)
        {
            speed = _speed;
            jump = _jump;
            armor = _armor;
            health = _health;
        }

        public float speed;
        public float jump;
        public float armor;
        public float health;
    }
}
