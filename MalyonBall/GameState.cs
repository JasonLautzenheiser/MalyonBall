namespace MalyonBall
{
  public enum State
  {
    Title,
    Menu,
    Playing,
    GameOver
  }

  public static class GameState
  {
    public static int Score { get; set; }
    public static int Lives { get; set; } = 3;

    public static State State { get; set; } = State.Playing;
  }
}