using System;
using System.Collections.Generic;
using System.Linq;

namespace BoardGame
{
    public class Game
    {
        private Tuple<int, int> PlayerPosition { get; set; }
        private List<Tuple<int, int>> PreviousPlayerPositions { get; set; }
        private IEnumerable<Tuple<int, int>> Mines { get; set; }

        public GameStatus Status { get; private set; }

        public Game() : this(new List<Tuple<int, int>>())
        {
        }

        public Game(IEnumerable<Tuple<int, int>> mines)
        {
            this.Status = GameStatus.Ongoing;
            
            this.PlayerPosition = Tuple.Create(0, 0);
            this.PreviousPlayerPositions = new List<Tuple<int, int>>() { this.PlayerPosition };

            this.Mines = mines;
        }

        public static IEnumerable<Tuple<int, int>> GenerateRandomMines(int numberOfMines)
        {
            var mines = new List<Tuple<int, int>>();
            var rand = new Random();

            while (mines.Count() < numberOfMines)
            {
                var x = rand.Next(8);
                var y = rand.Next(8);

                // Ignore start position
                if (x == 0 && y == 0)
                {
                    continue;
                }

                var p = new Tuple<int, int>(x, y);

                if (mines.Contains(p))
                {
                    continue;
                }

                mines.Add(p);
            }

            return mines;
        }

        public MoveResult Move(string move)
        {
            var result = ValidMove(move);

            if(result != null)
            {
                return result;
            }

            UpdatePosition(move);

            if (this.PreviousPlayerPositions.Intersect(this.Mines).Count() >= 2)
            {
                this.Status = GameStatus.Lose;
                return new MoveResult
                {
                    Status = MoveResult.StatusComplete
                };
            }

            if (this.PlayerPosition.Item2 == 7)
            {
                this.Status = GameStatus.Win;
                return new MoveResult
                {
                    Status = MoveResult.StatusComplete
                };
            }

            this.Status = GameStatus.Ongoing;
            return new MoveResult
            {
                Status = MoveResult.StatusSuccess
            };
        }

        private MoveResult ValidMove(string move)
        {
            if (move == "RIGHT")
            {
                if (this.PlayerPosition.Item1 == 7)
                {
                    return new MoveResult
                    {
                        Status = MoveResult.StatusFailure,
                        Message = "Already on the rightmost column"
                    };
                }
            }
            else if (move == "LEFT")
            {
                if (this.PlayerPosition.Item1 == 0)
                {
                    return new MoveResult
                    {
                        Status = MoveResult.StatusFailure,
                        Message = "Already on the leftmost column"
                    };
                }
            }
            else if (move == "UP")
            {
                if (this.PlayerPosition.Item2 == 7)
                {
                    return new MoveResult
                    {
                        Status = MoveResult.StatusFailure,
                        Message = "Already on the top row"
                    };
                }
            }
            else if (move == "DOWN")
            {
                if (this.PlayerPosition.Item2 == 0)
                {
                    return new MoveResult
                    {
                        Status = MoveResult.StatusFailure,
                        Message = "Already on the bottom row"
                    };
                }
            }
            else
            {
                return new MoveResult
                {
                    Status = MoveResult.StatusFailure,
                    Message = "Unknown Move - " + move
                }; ;
            }

            return null;
        }

        private void UpdatePosition(string move)
        {
            if (move == "RIGHT")
            {
                this.PlayerPosition = Tuple.Create(this.PlayerPosition.Item1 + 1, this.PlayerPosition.Item2);
            }
            else if (move == "LEFT")
            {
                this.PlayerPosition = Tuple.Create(this.PlayerPosition.Item1 - 1, this.PlayerPosition.Item2);
            }
            else if (move == "UP")
            {
                this.PlayerPosition = Tuple.Create(this.PlayerPosition.Item1, this.PlayerPosition.Item2 + 1);
            }
            else if (move == "DOWN")
            {
                this.PlayerPosition = Tuple.Create(this.PlayerPosition.Item1, this.PlayerPosition.Item2 - 1);
            }

            this.PreviousPlayerPositions.Add(this.PlayerPosition);
        }

        public override string ToString()
        {
            var board = new Board(8, 8);

            board.SetMarker(this.PlayerPosition.Item1, this.PlayerPosition.Item2, 'X');

            foreach (var mine in this.Mines)
            {
                if(this.PreviousPlayerPositions.Contains(mine))
                {
                    board.SetMarker(mine.Item1, mine.Item2, '+');
                }
            }

            if(this.Mines.Contains(this.PlayerPosition))
            {
                board.SetMarker(this.PlayerPosition.Item1, this.PlayerPosition.Item2, '*');
            }
            else
            {
                board.SetMarker(this.PlayerPosition.Item1, this.PlayerPosition.Item2, 'X');
            }

            return board.ToString();
        }

        private bool IsPlayerOnMine()
        {
            return this.Mines.Contains(this.PlayerPosition);
        }
    }
}
