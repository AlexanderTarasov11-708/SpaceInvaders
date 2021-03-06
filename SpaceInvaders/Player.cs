﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    /* 
     * Space Invaders 
     * 3-я семестровая работа
     * Александр Тарасов 11-708
     * Никита Хохлов 11-708
     * 
     * Player - игрок
     */
    class Player : Creature
    {
        public int X;
        public int Y;
        public static int cd;

        public Player(int y, int x) { X = x; Y = y; }

        //действие
        public void Act()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                switch (key)
                {
                    case ConsoleKey.A:
                        cd = 0;
                        GoTo(-1, 0);
                        break;

                    case ConsoleKey.D:
                        cd = 0;
                        GoTo(1, 0);
                        break;

                    case ConsoleKey.W:
                        if (cd < 5)
                        {
                            Game.Map[Y - 1, X] = new Bullet(Y - 1, X);
                            cd += 1;
                       }
                        break;
                }
            }
        }

        //перемещение
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
