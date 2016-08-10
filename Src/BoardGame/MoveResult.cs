namespace BoardGame
{
    public class MoveResult
    {
        public static readonly string StatusComplete = "COMPLETE";
        public static readonly string StatusSuccess = "SUCCESS";
        public static readonly string StatusFailure = "FAILURE";

        public string Status { get; set; }
        public string Message { get; set; }
    }
}
