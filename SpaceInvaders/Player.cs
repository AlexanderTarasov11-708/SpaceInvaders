using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    class Player : Creature
    {
        public int X;
        public int Y;

        public Player(int y, int x) { X = x; Y = y; }

        public void Act()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.A:
                        GoTo(-1, 0);
                        break;

                    case ConsoleKey.D:
                        GoTo(1, 0);
                        break;

                    case ConsoleKey.W:
                        Game.Map[Y - 1, X] = new Bullet(Y - 1, X);
                        break;
                }
            }
        }

        public void GoTo(int deltaX, int deltaY)
        {
            if (Game.Map[Y + deltaY, X + deltaX] == null)
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
