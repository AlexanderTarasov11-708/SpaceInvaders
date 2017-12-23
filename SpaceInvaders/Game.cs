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
        public static int maxHealth = 200;
        public static int yourPoints = 0;
        public static int yourHealth = maxHealth;
        public static Random Rand = new Random();
        public static int numberOfAliens;
        public static int wave = 1;
        

        private static char[,] NewMap = new char[20, 21]
        {
            {'#','#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#', '#' },
            {'#',' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 'V', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', '#' },
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
                        case 'V':
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
            Console.WindowHeight = 26;
            Console.WindowWidth = 110;
            Console.WriteLine("SPACE INVADERS");
            Console.WriteLine("Some rules:");
            Console.WriteLine("  A - Left move");
            Console.WriteLine("  D - Right move");
            Console.WriteLine("  W - shoot");
            Console.WriteLine("After 5 shoots from one position you have 'cooldown' and have to move left/right");
            Console.WriteLine("Every next wave you get +1 alien");
            Console.WriteLine("Every 5th wave you get +20 health");
            Console.WriteLine("19th wave is the last one \n");
            Console.WriteLine("Press Enter to start");
            do
            {
                // ваши действия
            } while (Console.ReadKey().Key != ConsoleKey.Enter);
            Map = MapCreator(NewMap);
            while (!EndGame)
            {
                WriteMap();
                Console.WriteLine("Health " + yourHealth + "/" + maxHealth);
                Console.WriteLine("Score: " + yourPoints);
                Console.WriteLine("Wave: " + wave);
                Console.Write("Cooldown: ");
                if (Player.cd == 5)
                    Console.Write("Yes");
                else Console.Write("No");

                foreach (var e in Map)
                    if (e != null)
                    e.Act();
                if (numberOfAliens == 0)
                {
                    wave++;
                    if (wave % 5 == 0)
                        yourHealth += 20;
                    if (wave < 19)
                    {
                        List<int> check = new List<int>();
                        for (int i = 0; i < wave; i++)
                        {
                            int newX = Rand.Next(1, 19);
                            if (!check.Contains(newX))
                            {
                                check.Add(newX);
                                Map[1, newX] = new Alien(1, newX);
                            }
                            else i--;
                        }
                    }
                    else
                        EndGame = true;
                }
                if (yourHealth <= 0)
                {
                    EndGame = true;
                }
                System.Threading.Thread.Sleep(50);
                Console.Clear();
            }
            if (numberOfAliens == 0 && yourHealth > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You Win! Your score: " + yourPoints);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Game Over! \nLast wave:" + wave +"\nYour score: " + yourPoints);
            }
            Console.WriteLine("Press Esc to exit");
            do
            {
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
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
                        Console.Write('V');
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
                if (i == 0)
                    Console.Write("\t SPACE INVADERS");
                if (i == 1)
                    Console.Write("\t Some rules:");
                if (i == 2)
                    Console.Write("\t A - Left move");
                if (i == 3)
                    Console.Write("\t D - Right move");
                if (i == 4)
                    Console.Write("\t W - shoot");
                if (i == 5)
                    Console.Write("\t After 5 shoots from one position you have 'cooldown' and have to move left/right");
                if (i == 6)
                    Console.Write("\t Every next wave you get +1 alien");
                if (i == 7)
                    Console.Write("\t Every 5th wave you get +20 health");
                if (i == 8)
                    Console.Write("\t 19th wave is the last one");
                Console.WriteLine();
            }
        }
    }
}
