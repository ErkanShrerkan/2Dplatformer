using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _2Dplatform
{
    class Game
    {
        Map map;
        List<Character> characters = new List<Character>();
        float gravity = 10;
        public double deltaT;
        double time1;
        double time2;
        string fps;
        char[,] fullMapArray;

        public Game()
        {
            GameSetup();

            Start();
            Update();
        }

        void Start()
        {
            DrawMap();
        }

        void Update()
        {
            while (true)
            {
                time1 = (new TimeSpan(DateTime.Now.Ticks)).TotalMilliseconds;
                deltaT = time1 - time2;
                time2 = time1;

                fps = (1 / (deltaT / 1000)).ToString("0.0");

                Console.SetCursorPosition(0, 16);
                Console.WriteLine(fps + " fps");

                DrawMap();
                DrawCharacters();
                Fall();
            }
        }

        void DrawCharacters()
        {
            for (int i = 0; i < characters.Count; i++)
            {
                int roundedPosX = (int)characters[i].Pos[0];
                int roundedPosY = (int)characters[i].Pos[1];

                //Console.WriteLine(roundedPosX + " "+ roundedPosY+ "                          ");
                //Console.ReadLine();

                Console.SetCursorPosition(CheckValidPosition(roundedPosX, "x"), CheckValidPosition(roundedPosY, "y"));

                if (characters[i].name == "Player")
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Write(" ");
                }
                else
                {
                    Console.Write(" ");
                    Console.BackgroundColor = ConsoleColor.Yellow;
                }
                Console.ResetColor();
            }
        }

        int CheckCollision(int xy, string axis)
        {
            for (int i = 0; i < characters.Count; i++)
            {
                switch (fullMapArray[(int)characters[i].Pos[0], (int)characters[i].Pos[1]])
                {
                    case '.':
                        break;
                    case '#':
                        // kolla vilka tiles som ligger runt spelaren
                        break;
                    case '¤':
                        break;
                }
            }

            return 1;
        }

        void DrawMap()
        {
            var tiles = GetMapAsArray();
            Console.SetCursorPosition(0, 0);

            for (int y = 0; y < map.height; y++)
            {
                for (int x = 0; x < map.width; x++)
                {
                    //Console.SetCursorPosition(x, y);

                    switch (tiles[x, y])
                    {
                        case '.':
                            Console.Write(" ");
                            Console.BackgroundColor = ConsoleColor.Blue;
                            break;
                        case '#':
                            Console.Write(" ");
                            Console.BackgroundColor = ConsoleColor.Green;
                            break;
                        case '¤':
                            Console.Write(" ");
                            Console.BackgroundColor = ConsoleColor.White;
                            break;
                    }
                }
            }
            Console.ResetColor();
        }

        char[,] GetMapAsArray()
        {
            char[] mapArray = map.level.ToCharArray();

            fullMapArray = new char[map.width, map.height];

            for (int y = 0; y < map.height; y++)
            {
                for (int x = 0; x < map.width; x++)
                {
                    fullMapArray[x, y] = mapArray[(64 * y) + x];
                }
            }
            return fullMapArray;
        }

        private void CharacterController()
        {
            while (true)
            {
                var key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.RightArrow)
                {
                    Walk(1);
                }
                if (key.Key == ConsoleKey.LeftArrow)
                {
                    Walk(-1);
                }
                if (key.Key == ConsoleKey.UpArrow)
                {
                    Jump();
                }
                if (key.Key == ConsoleKey.Escape)
                {
                    Environment.Exit(0);
                }
            }
        }

        void Fall()
        {
            if ((int)deltaT < 0)
            {
                deltaT = 0;
            }

            for (int i = 0; i < characters.Count; i++)
            {
                characters[i].Pos[1] += gravity * (float)deltaT / 1000;
            }
        }

        void Walk(int direction)
        {
            characters[0].Pos[0] += (characters[0].GetSpeed() * (float)deltaT / 1000) * direction;
        }

        void Jump()
        {
            characters[0].Pos[1] -= (characters[0].GetSpeed() * 3 * (float)deltaT / 1000);
        }

        void GameSetup()
        {
            map = new Map();
            Character player = new Player();
            new Task(() => { CharacterController(); }).Start();
            new Task(() => { Fall(); }).Start();
            Console.CursorVisible = false;
            characters.Add(player);
        }

        int CheckValidPosition(int i, string axis)
        {
            if (i < 0)
            {
                i = 0;
                return i;
            }
            if (axis == "x")
            {
                if (i >= map.width)
                {
                    i = map.width - 1;
                    return i;
                }
                else return i;
            }
            if (axis == "y")
            {
                if (i >= map.height)
                {
                    i = map.height - 1;
                    return i;
                }
                else return i;
            }
            else return 0;
        }
    }
}
