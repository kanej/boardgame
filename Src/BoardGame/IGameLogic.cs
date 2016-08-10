namespace BoardGame
{
    public interface IGameLogic
    {
        MoveResult ValidateMove(GameState state, string move);
        GameState ApplyMove(GameState gameState, string move);
        string BoardToString(GameState gameState);
        GameState InitialiseState();
    }
}
