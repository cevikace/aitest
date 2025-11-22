namespace MeadowFlight.Game;

public sealed class Obstacle
{
    public float X { get; init; }

    public float GapY { get; set; }

    public float GapHeight { get; init; }

    public float Width { get; init; }

    public bool HasBeenPassed { get; set; }
}
