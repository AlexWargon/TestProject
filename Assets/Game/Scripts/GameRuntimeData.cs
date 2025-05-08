namespace Wargon.TestGame
{
    public sealed class GameRuntimeData
    {
        public GameState State;
        
        public enum GameState
        {
            Win,
            Lose,
            GameRunning
        }
    }
}