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
        //public int hp = 1;

        public Alien(int y, int x) { X = x; Y = y; }

        public void Act()
        {
            if (Y == 18) Game.EndGame = true;
            int move = Game.Rand.Next(1, 33);
            switch (move)
            {
                case 1:
                    GoTo(0, 1);
                    break;
                case 2:
                    GoTo(-1, 0);
                    break;
                case 3:
                    GoTo(1, 0);
                    break;
                case 4:
                    if (Y+1 < Game.Map.GetLength(0))
                    Game.Map[Y + 1, X] = new AlienBullet(Y + 1, X);
                    break;

            }
        }

        public void GoTo(int deltaX, int deltaY)
        {
            if (Game.Map[Y + deltaY, X + deltaX] is null || Game.Map[Y + deltaY, X + deltaX] is Bullet)
            {
                Game.Map[Y, X] = null;
                X += deltaX;
                Y += deltaY;
                Game.Map[Y, X] = this;
            }
        }

        public bool Conflict()
        {

            return false;
        }
    }
}
