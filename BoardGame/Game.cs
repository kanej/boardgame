using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame
{
    public class Game
    {
        private List<string> Moves { get; set; }
        private Tuple<int, int> PlayerPosition { get; set; }

        public Game()
        {
            this.Moves = new List<string>();
            this.PlayerPosition = Tuple.Create(0, 0);
        }

        public override string ToString()
        {
            var board = new Board(8, 8);
            board.SetMarker(this.PlayerPosition.Item1, this.PlayerPosition.Item2, 'X');
            return board.ToString();
        }

        public void Move(string move)
        {
            if(move == "RIGHT")
            {
                this.PlayerPosition = Tuple.Create(this.PlayerPosition.Item1 + 1, this.PlayerPosition.Item2);
            }
            if(move == "UP")
            {
                this.PlayerPosition = Tuple.Create(this.PlayerPosition.Item1, this.PlayerPosition.Item2 + 1);
            }

            Moves.Add(move);
        }

        public class Board
        {
            private int Height { get; set; }
            private int Width { get; set; }

            private char[] Markers { get; set; }

            public Board(int height, int width)
            {
                this.Height = height;
                this.Width = width;
                this.Markers = new char[this.Height * this.Width];

                for (var i = 0; i < this.Markers.Length; i++)
                {
                    this.Markers[i] = '0';
                }
            }

            public void SetMarker(int x, int y, char marker)
            {
                var index = (this.Width * y) + x;
                this.Markers[index] = marker;
            }

            public override string ToString()
            {
                var rows = this.Markers.Partition(this.Width);

                var combinedRows = rows.Reverse().Select(row => { return string.Concat(row); }); 

                return string.Join("\r\n", combinedRows);
            }
        }
    }

    public static class FunctionalExtensions {

        public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> l, int sizeOfSubsequences)
        {
            return l.Select((x, i) => new { Group = i / sizeOfSubsequences, Value = x })
                     .GroupBy(item => item.Group, g => g.Value)
                     .Select(g => g.Where(x => true));
        }
    }
}
