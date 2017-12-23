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
                    if (GoTo(-1, 1))
                        variant = Game.Rand.Next(2, 3);
                if (variant == 2)
                    if (GoTo(1, 1))
                        variant = 3;
                if (variant == 3)
                    GoTo(0, 1);
            }
            if (move % 15 == 0)
            {
                if (Y + 1 < Game.Map.GetLength(0))
                    Game.Map[Y + 1, X] = new AlienBullet(Y + 1, X);
            }
            move++;
        }

        public bool GoTo(int deltaX, int deltaY)
        {
            if (Game.Map[Y + deltaY, X + deltaX] is Border)
            {
                return true;
            }
            if (Game.Map[Y + deltaY, X + deltaX] is null)
            {
                Game.Map[Y, X] = null;
                X += deltaX;
                Y += deltaY;
                Game.Map[Y, X] = this;
            }
            if (Game.Map[Y + deltaY, X + deltaX] is Bullet)
            {
                Game.Map[Y, X] = null;
            }
            return false;
        }

        public bool Conflict()
        {

            return false;
        }
    }
}
