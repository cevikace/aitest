namespace MeadowFlight.Game;

public sealed class Obstacle
{
    public required float X { get; init; }

    public required float GapY { get; set; }

    public required float GapHeight { get; init; }

    public required float Width { get; init; }

    public bool HasBeenPassed { get; set; }
}
