namespace BoardGame
{
    public class GameRunner
    {
        private GameState State { get; set; }
        private IGameLogic Logic { get; set; }

        public GameRunner(IGameLogic logic, GameState state)
        {
            this.State = state;
            this.Logic = logic;
        }

        public GameRunner(IGameLogic logic)
        {
            this.Logic = logic;
            this.State = logic.InitialiseState();
        }

        public GameStatus Status {  get { return this.State.Status;  } }

        public MoveResult Move(string move)
        {
            var result = this.Logic.ValidateMove(this.State, move);

            if(result != null)
            {
                return result;
            }

            this.State = this.Logic.ApplyMove(this.State, move);

            if (this.State.Status == GameStatus.Ongoing)
            {
                return new MoveResult
                {
                    Status = MoveResult.StatusSuccess
                };
            }
            else
            {
                return new MoveResult
                {
                    Status = MoveResult.StatusComplete
                };
            }
        }

        public override string ToString()
        {
            return this.Logic.BoardToString(this.State);
        }
    }
}
