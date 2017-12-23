using System;

namespace SpaceInvaders
{
    /* 
     * Space Invaders 
     * 3-я семестровая работа
     * Александр Тарасов 11-708
     * Никита Хохлов 11-708
     * 
     * AlienBullet - пуля противника 
     */
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
                    if (!(Game.Map[Y, X] is Border) && !(Game.Map[Y, X] is Player) && !(Game.Map[Y, X] is Alien) && !(Game.Map[Y, X] is AlienBullet))
                    {
                        Game.Map[Y, X] = this;
                        flag = true;
                    }
                    else if (Game.Map[Y, X] is AlienBullet)
                    {
                        Y--;
                        Game.Map[Y, X] = this;
                    }
                    else if (Game.Map[Y, X] is Player)
                    {
                        var player = (Player)Game.Map[Y, X];
                        Game.yourHealth -= 20;
                        if (Game.yourHealth <= 80)
                            Console.ForegroundColor = ConsoleColor.Red;
                    }
                }
                else
                    flag = false;
            }
        }

        public bool Conflict()
        {
            if ((Y + 1 < Game.Map.GetLength(0)) && (Game.Map[Y + 1, X] is Bullet))
            {
                Game.Map[Y, X] = null;
                Game.Map[Y + 1, X] = null;
                return true;
            }
            return false;
        }
    }
}
