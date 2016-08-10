using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardGame
{
    public class MineGameState : GameState
    {
        public Tuple<int, int> PlayerPosition { get; set; }
        public List<Tuple<int, int>> PreviousPlayerPositions { get; set; }
        public IEnumerable<Tuple<int, int>> Mines { get; set; }

        public MineGameState(IEnumerable<Tuple<int, int>> mines)
        {
            this.Status = GameStatus.Ongoing;
            this.Mines = mines;
            this.PlayerPosition = Tuple.Create(0, 0);
            this.PreviousPlayerPositions = new List<Tuple<int, int>>() { this.PlayerPosition };
        }

        public MineGameState() : this(new List<Tuple<int, int>>())
        {
        }
    }
}
