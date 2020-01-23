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
        public double delta;
        double time1;
        double time2;

        public Game()
        {
            map = new Map();
            Character player = new Player();
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
                delta = time1 - time2;
                time2 = time1;

                string fps = (1 / (delta / 1000)).ToString("0.0");

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

            for (int i = 0; i < tiles.Length; i++)
            {
                switch (tiles[i])
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
            Console.ResetColor();

            /*
            for (int y = 0; y < map.width; y++)
            {
                for (int x = 0; x < map.height; x++)
                {
                    


                }
            }*/
        }

        char[] GetMapAsArray()
        {
            char[] mapArray = map.level.ToCharArray();
            return mapArray;
        }
    }
}
