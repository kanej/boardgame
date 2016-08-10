namespace BoardGame
{
    public class GameStatus
    {
        public static GameStatus Win = new GameStatus { Status = "WIN" };
        public static GameStatus Lose = new GameStatus { Status = "LOSE" };
        public static GameStatus Ongoing = new GameStatus { Status = "ONGOING" };

        private string Status { get; set; }
    }
}
