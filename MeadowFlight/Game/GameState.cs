namespace MeadowFlight.Game;

public sealed class GameState
{
    public Player Player { get; } = new();

    public List<Obstacle> Obstacles { get; } = new();

    public int Score { get; set; }

    public bool IsGameOver { get; set; }

    public float ScrollOffset { get; set; }

    public float DayNightTimer { get; set; }
}
