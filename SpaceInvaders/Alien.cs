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
        public int move;

        public Alien(int y, int x) { X = x; Y = y; }

        public void Act()
        {
            if (Y == 18) Game.EndGame = true;
            int variant = Game.Rand.Next(1, 3);
            if (move != 0 && move % 50 == 0)
            {
                if (variant == 1)
                    GoTo(0, 1);
                else if (variant == 2)
                    GoTo(-1, 1);
                else if (variant == 3)
                    GoTo(1, 1);
            }
            if (move % 15 == 0)
            {
                if (Y + 1 < Game.Map.GetLength(0))
                    Game.Map[Y + 1, X] = new AlienBullet(Y + 1, X);
            }
            move++;
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
