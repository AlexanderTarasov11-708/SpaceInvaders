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
        public static int wave;
        

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
            Map = MapCreator(NewMap);
            while (!EndGame)
            {
                Console.WindowHeight = 23;
                Console.WindowWidth = 22;
                Console.Clear();
                WriteMap();
                Console.WriteLine("Health " + yourHealth + "/" + maxHealth);
                Console.WriteLine("Score: " + yourPoints);
                Console.Write("Cooldown: ");
                if (Player.cd == 5)
                    Console.Write("Yes");
                else Console.Write("No");

                foreach (var e in Map)
                    if (e != null)
                    e.Act();
                if (numberOfAliens == 0)
                {
                    if (wave < 19)
                    {
                        List<int> check = new List<int>();
                        for (int i = 0; i < wave; i++)
                        {
                            int newX = Game.Rand.Next(1, 19);
                            if (!check.Contains(newX))
                            {
                                check.Add(newX);
                                Game.Map[1, newX] = new Alien(1, newX);
                            }
                            else i--;
                        }
                    }
                    else
                        for (int i = 1; i < 20; i++)
                            for (int j = 1; j < wave; j++)
                                Game.Map[i, j] = new Alien(i, j);
                    wave += 1;
                }
                if (yourHealth <= 0)
                {
                    EndGame = true;
                }
                System.Threading.Thread.Sleep(50);
            }
            Console.Clear();
            if (numberOfAliens == 0 && yourHealth > 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("You Win! Your score: " + yourPoints);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Game Over! Your score: " + yourPoints);
            }
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
                Console.WriteLine();
            }
        }
    }
}
