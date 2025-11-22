namespace MeadowFlight.Game;

public sealed record GameConfig
{
    public static GameConfig Default => new()
    {
        TargetFramesPerSecond = 60,
        Gravity = 36f,
        FlapImpulse = 14f,
        GroundHeight = 2,
        WorldHeight = 22,
        WorldWidth = 48,
        ObstacleSpacing = 18f,
        ObstacleSpeed = 9f,
        ObstacleGapHeight = 8f,
        ObstacleWidth = 5f,
        MaxGapJitter = 3f,
        DayNightCycleDurationSeconds = 120f
    };

    public int TargetFramesPerSecond { get; init; }

    public float Gravity { get; init; }

    public float FlapImpulse { get; init; }

    public int GroundHeight { get; init; }

    public int WorldHeight { get; init; }

    public int WorldWidth { get; init; }

    public float ObstacleSpacing { get; init; }

    public float ObstacleSpeed { get; init; }

    public float ObstacleGapHeight { get; init; }

    public float ObstacleWidth { get; init; }

    public float MaxGapJitter { get; init; }

    public float DayNightCycleDurationSeconds { get; init; }
}
