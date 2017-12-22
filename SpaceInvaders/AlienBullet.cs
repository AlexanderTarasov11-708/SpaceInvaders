using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    class AlienBullet : Creature
    {
        public int X;
        public int Y;
        public bool flag = false;

        public AlienBullet(int y, int x) { X = x; Y = y; }

        public void Act()
        {
            if (!Conflict())
            {
                //для избежания повторного обращения к пуле во время цикла
                if (!(flag))    
                {
                    Game.Map[Y, X] = null;
                    Y++;
                    if (!(Game.Map[Y, X] is Border) && !(Game.Map[Y, X] is Player) && !(Game.Map[Y, X] is Alien))
                    {
                        Game.Map[Y, X] = this;
                        flag = true;
                    }
                    else if (Game.Map[Y, X] is Alien)
                    {
                        Game.Map[Y + 2, X] = this;
                        flag = true;
                    }
                    else if (Game.Map[Y, X] is Player)
                    {
                        var player = (Player)Game.Map[Y, X];
                        Game.yourHealth -= 20;
                        if (Game.yourHealth <= 100)
                            Console.ForegroundColor = ConsoleColor.Red;
                    }
                }
                else
                    flag = false;
            }
        }

        public bool Conflict()
        {
            if (Game.Map[Y + 1, X] is Bullet)
            {
                Game.Map[Y, X] = null;
                Game.Map[Y + 1, X] = null;
                return true;
            }
            return false;
        }
    }
}
