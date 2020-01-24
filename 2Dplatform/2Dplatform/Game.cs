using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2Dplatform
{
    class Game
    {
        Map map;
        List<Character> characters = new List<Character>();
        float gravity = 30;
        public double deltaT;
        double time1;
        double time2;

        public Game()
        {
            map = new Map();
            Character player = new Player();
            new Task(() => { CharacterController(); }).Start();
            characters.Add(player);

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

                string fps = (1 / (deltaT / 1000)).ToString("0.0");

                Console.SetCursorPosition(0, 16);
                Console.WriteLine(fps + " fps");

                Console.CursorVisible = false;

                DrawMap();
                DrawCharacters();
            }
        }

        void DrawCharacters()
        {
            for (int i = 0; i < characters.Count; i++)
            {
                int roundedPosX = (int)characters[i].position[0];
                int roundedPosY = (int)characters[i].position[1];
                Console.SetCursorPosition(roundedPosX, roundedPosY);

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

        void DrawMap()
        {
            Console.SetCursorPosition(0, 0);

            var tiles = GetMapAsArray();

            for (int y = 0; y < map.height; y++)
            {
                for (int x = 0; x < map.width; x++)
                {
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

            /*
            for (int y = 0; y < map.width; y++)
            {
                for (int x = 0; x < map.height; x++)
                {
                    


                }
            }*/
        }

        char[,] GetMapAsArray()
        {
            char[] mapArray = map.level.ToCharArray();

            char[,] fullMapArray = new char[map.width, map.height];

            for (int y = 0; y < map.height; y++)
            {
                for (int x = 0; x < map.width; x++)
                {
                    fullMapArray[x, y] = mapArray[(64*y)+x];
                }
            }
            return fullMapArray;
        }

        private void CharacterController()
        {
            while (true)
            {
                Fall();
                if (Console.ReadKey(true).Key == ConsoleKey.D)
                {
                    WalkRight();
                }
                if (Console.ReadKey(true).Key == ConsoleKey.A)
                {
                    WalkLeft();
                }
            }
        }

        void Fall()
        {
            characters[0].position[1] += gravity * (float)deltaT / 1000;
            characters[0].position[1] = CheckValidPosition(characters[0].position[1], "y");
        }

        void WalkLeft()
        {
            characters[0].position[0] -= characters[0].GetSpeed() * (float)deltaT/1000;
            characters[0].position[0] = CheckValidPosition(characters[0].position[0], "x");
        }

        void WalkRight()
        {
            characters[0].position[0] += characters[0].GetSpeed() * (float)deltaT/1000;
            characters[0].position[0] = CheckValidPosition(characters[0].position[0], "x");
        }

        float CheckValidPosition(float f, string xy)
        {
            if (f < 0)
            {
                f = 0;
                return f;
            }
            if (xy == "x")
            {
                if (f > 64)
                {
                    f = 64;
                    return f;
                }
                else return f;
            }
            if (xy == "y")
            {
                if (f > 16)
                {
                    f = 16;
                    return f;
                }
                else return f;
            }
            else return 0;
        }
    }
}
