using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    class Bullet : Creature
    {
        public int X;
        public int Y;
        public bool Hit = false;

        public Bullet(int y, int x) { X = x; Y = y; }

        public void Act()
        {
            if (!Conflict())
            {
                Game.Map[Y, X] = null;
                Y--;
                if (!(Game.Map[Y, X] is Border) && !(Game.Map[Y, X] is Alien) && !(Game.Map[Y, X] is AlienBullet))
                    Game.Map[Y, X] = this;
            }
        }

        public bool Conflict()
        {
            if (Game.Map[Y - 1, X] is AlienBullet)
            {
                Game.Map[Y, X] = null;
                Game.Map[Y - 1, X] = null;
                return true;
            }
            else if (Game.Map[Y - 1, X] is Alien)
            {
                Game.Map[Y, X] = null;
                Game.Map[Y - 1, X] = null;
                Game.yourPoints += 10;
                return true;
            }
            return false;
        }
    }
}
