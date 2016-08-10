using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Boardgame!");
            Console.WriteLine("Enter U,D,L or R to move");
            Console.WriteLine("");

            var mines = Game.GenerateRandomMines(7);
            //var mines = new Tuple<int, int>[] { new Tuple<int, int>(0, 1), new Tuple<int, int>(0, 2) };
            var g = new Game(mines);

            while(g.Status == GameStatus.Ongoing)
            {
                Console.WriteLine(g);
                Console.WriteLine("Please enter next move:");
                var entry = Console.ReadLine().ToString().ToLower();

                if (entry == "l")
                {
                    g.Move("LEFT");
                }
                else if(entry == "r")
                {
                    g.Move("RIGHT");
                }
                else if(entry == "u")
                {
                    g.Move("UP");
                }
                else if (entry == "d")
                {
                    g.Move("DOWN");
                }
                else
                {
                    continue;
                }
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
    }
}
