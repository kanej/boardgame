using System;
using System.Collections.Generic;

namespace BoardGame
{
    class Program
    {
        private static Dictionary<string, string> KeyMap = new Dictionary<string, string>
        {
            { "l", "LEFT" },
            { "r", "RIGHT" },
            { "u", "UP" },
            { "d", "DOWN" },
        };

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Boardgame!");
            Console.WriteLine("Enter U, D, L or R to move");
            Console.WriteLine("");

            var mines = Game.GenerateRandomMines(7);
            //var mines = new Tuple<int, int>[] { new Tuple<int, int>(0, 1), new Tuple<int, int>(0, 2) };
            var g = new Game(mines);

            while(g.Status == GameStatus.Ongoing)
            {
                Console.WriteLine(g);
                Console.WriteLine("Please enter next move:");
                var entry = Console.ReadLine().ToString();

                var move = ConvertEntryToMove(entry);

                if(move == null)
                {
                    continue;
                }

                g.Move(move);
            }

            if(g.Status == GameStatus.Win)
            {
                Console.WriteLine("!!! You Win !!!");
            } else
            {
                Console.WriteLine("--- You Lose ---");
            }

            Console.WriteLine(g);
            Console.WriteLine("Thank you for playing");
            Console.Read();
        }

        private static string ConvertEntryToMove(string entry)
        {
            var lowerEntry = entry.ToLower();

            if(!KeyMap.ContainsKey(lowerEntry))
            {
                return null;

            }

            return KeyMap[lowerEntry];
        }
    }
}
