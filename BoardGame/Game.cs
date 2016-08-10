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
            this.Status = GameStatus.Ongoing;
            this.Moves = new List<string>();
            this.PlayerPosition = Tuple.Create(0, 0);
        }

        public GameStatus Status { get; private set; }

        public MoveResult Move(string move)
        {
            if(move == "RIGHT")
            {
                if (this.PlayerPosition.Item1 == 7)
                {
                    return new MoveResult
                    {
                        Status = "Failure",
                        Message = "Already on the rightmost column"
                    };
                }

                this.PlayerPosition = Tuple.Create(this.PlayerPosition.Item1 + 1, this.PlayerPosition.Item2);
            }
            else if (move == "LEFT")
            {
                if (this.PlayerPosition.Item1 == 0)
                {
                    return new MoveResult
                    {
                        Status = "Failure",
                        Message = "Already on the leftmost column"
                    };
                }

                this.PlayerPosition = Tuple.Create(this.PlayerPosition.Item1 - 1, this.PlayerPosition.Item2);
            }
            else if (move == "UP")
            {
                if (this.PlayerPosition.Item2 == 7)
                {
                    return new MoveResult
                    {
                        Status = "Failure",
                        Message = "Already on the top row"
                    };
                }

                this.PlayerPosition = Tuple.Create(this.PlayerPosition.Item1, this.PlayerPosition.Item2 + 1);
            }
            else if (move == "DOWN")
            {
                if(this.PlayerPosition.Item2 == 0)
                {
                    return new MoveResult
                    {
                        Status = "Failure",
                        Message = "Already on the bottom row"
                    };
                }

                this.PlayerPosition = Tuple.Create(this.PlayerPosition.Item1, this.PlayerPosition.Item2 - 1);
            }
            else
            {
                return new MoveResult
                {
                    Status = "Failure",
                    Message = "Unknown Move - " + move
                };
            }

            Moves.Add(move);

            if(this.PlayerPosition.Item2 == 7)
            {
                this.Status = GameStatus.Win;
                return new MoveResult
                {
                    Status = "Complete"
                };
            }

            return new MoveResult
            {
                Status = "Success"
            };
        }

        public override string ToString()
        {
            var board = new Board(8, 8);
            board.SetMarker(this.PlayerPosition.Item1, this.PlayerPosition.Item2, 'X');
            return board.ToString();
        }

        public class MoveResult
        {
            public string Status { get; set; }
            public string Message { get; set; }
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

public class GameStatus
{
    public static GameStatus Win = new GameStatus { Status = "WIN" };
    public static GameStatus Lose = new GameStatus { Status = "LOSE" };
    public static GameStatus Ongoing = new GameStatus { Status = "ONGOING" };

    private string Status { get; set; }
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
