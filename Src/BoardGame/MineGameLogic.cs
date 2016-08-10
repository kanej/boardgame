using System;
using System.Collections.Generic;
using System.Linq;

namespace BoardGame
{
    public class MineGameLogic : IGameLogic
    {
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

        public GameState InitialiseState()
        {
            return new MineGameState();
        }

        public MoveResult ValidateMove(GameState gameState, string move)
        {
            var state = (MineGameState)gameState;

            if (move == "RIGHT")
            {
                if (state.PlayerPosition.Item1 == 7)
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
                if (state.PlayerPosition.Item1 == 0)
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
                if (state.PlayerPosition.Item2 == 7)
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
                if (state.PlayerPosition.Item2 == 0)
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
                };
            }

            return null;
        }

        public  GameState ApplyMove(GameState gameState, string move)
        {
            var updatedPositionState = UpdatePosition(gameState, move);
            return UpdateGameStatus(updatedPositionState);
        }

        private static GameState UpdatePosition(GameState gameState, string move)
        {
            var state = (MineGameState)gameState;

            if (move == "RIGHT")
            {
                state.PlayerPosition = Tuple.Create(state.PlayerPosition.Item1 + 1, state.PlayerPosition.Item2);
            }
            else if (move == "LEFT")
            {
                state.PlayerPosition = Tuple.Create(state.PlayerPosition.Item1 - 1, state.PlayerPosition.Item2);
            }
            else if (move == "UP")
            {
                state.PlayerPosition = Tuple.Create(state.PlayerPosition.Item1, state.PlayerPosition.Item2 + 1);
            }
            else if (move == "DOWN")
            {
                state.PlayerPosition = Tuple.Create(state.PlayerPosition.Item1, state.PlayerPosition.Item2 - 1);
            }

            state.PreviousPlayerPositions.Add(state.PlayerPosition);

            return state;
        }

        private static GameState UpdateGameStatus(GameState gameState)
        {
            var state = (MineGameState)gameState;

            if (state.PreviousPlayerPositions.Intersect(state.Mines).Count() >= 2)
            {
                state.Status = GameStatus.Lose;
            }

            else if (state.PlayerPosition.Item2 == 7)
            {
                state.Status = GameStatus.Win;
            }
            else
            {
                state.Status = GameStatus.Ongoing;
            }

            return state;
        }

        public string BoardToString(GameState gameState)
        {
            var state = (MineGameState)gameState;

            var board = new Board(8, 8);

            board.SetMarker(state.PlayerPosition.Item1, state.PlayerPosition.Item2, 'X');

            foreach (var mine in state.Mines)
            {
                if (state.PreviousPlayerPositions.Contains(mine))
                {
                    board.SetMarker(mine.Item1, mine.Item2, '+');
                }
            }

            if (state.Mines.Contains(state.PlayerPosition))
            {
                board.SetMarker(state.PlayerPosition.Item1, state.PlayerPosition.Item2, '*');
            }
            else
            {
                board.SetMarker(state.PlayerPosition.Item1, state.PlayerPosition.Item2, 'X');
            }

            return board.ToString();
        }
    }
}
