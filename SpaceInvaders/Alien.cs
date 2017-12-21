using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    class Alien : Creature
    {
        public int X;
        public int Y;

        public Alien(int y, int x) { X = x; Y = y; }

        public void Act()
        {
            int randomNum = Game.Rand.Next(1, 25);

            if (randomNum == 1)
                Game.Map[Y+1, X] = new AlienBullet(Y+1,X);
            
        }

        public bool Conflict()
        {
            return false;
        }
    }
}
