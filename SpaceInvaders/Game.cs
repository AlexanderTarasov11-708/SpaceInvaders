using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvaders
{
    public static class Game
    {
        public static Creature[,] Map;
        public static bool EndGame = false;
        public static int MaxHealth = 200;
        public static int YourHealth = MaxHealth;
        public static Random Rand = new Random();
        public static int numberOfAliens;
        

        private static char[,] NewMap = new char[20, 21]
        {
            {'#','#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
            {'#',' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '@', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            {'#',' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            {'#',' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            {'#',' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            {'#',' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            {'#',' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            {'#',' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            {'#',' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            {'#',' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            {'#',' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            {'#',' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            {'#',' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            {'#',' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            {'#',' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            {'#',' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            {'#',' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            {'#',' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            {'#',' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'W', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
            {'#','#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' }
        };

        public static Creature[,] MapCreator(char[,] map)
        {
            var gameMap = new Creature[map.GetLength(0), map.GetLength(1)];
            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    switch (map[i, j])
                    {
                        case 'W':
                            gameMap[i, j] = new Player(i, j);
                            break;
                        case '@':
                            gameMap[i, j] = new Alien(i, j);
                            break;
                        case '#':
                            gameMap[i, j] = new Border(i, j);
                            break;
                        case ' ':
                            gameMap[i, j] = null;
                            break;
                    }
                }
            return gameMap;
        }

        public static void Main (string[] args)
        {
            Map = MapCreator(NewMap);
            while (!EndGame)
            {
                Console.Clear();
                WriteMap();
                Console.WriteLine("Health " + YourHealth + "/" + MaxHealth);

                foreach (var e in Map)
                    if (e != null)
                    e.Act();
                if (numberOfAliens == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    EndGame = true;
                }
                if (YourHealth <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    EndGame = true;
                }
                System.Threading.Thread.Sleep(100);
            }
            Console.Clear();
            if (numberOfAliens == 0 && YourHealth > 0)
                Console.WriteLine("You win!");
            if (YourHealth <= 0)
                Console.WriteLine("You lost!");
        }

        private static void WriteMap()
        {
            numberOfAliens = 0;
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 21; j++)
                {
                    if (Map[i, j] is Player)
                        Console.Write('W');
                    if (Map[i, j] is Alien)
                    {
                        numberOfAliens++;
                        Console.Write('@');
                    }
                    if (Map[i, j] is Bullet)
                        Console.Write('|');
                    if (Map[i, j] is AlienBullet)
                        Console.Write('*');
                    if (Map[i, j] is Border)
                        Console.Write('#');
                    if (Map[i, j] is null)
                        Console.Write(' ');
                }
                Console.WriteLine();
            }
        }
    }
}
