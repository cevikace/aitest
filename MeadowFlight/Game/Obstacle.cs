namespace MeadowFlight.Game;

/// <summary>
/// Represents a single vine/branch obstacle segment with a vertical gap the player must fly through.
/// Explicit constructor parameters avoid the newer <c>required</c> keyword so the class compiles on older C# language versions.
/// </summary>
public sealed class Obstacle
{
    public Obstacle(float x, float gapY, float gapHeight, float width)
    {
        X = x;
        GapY = gapY;
        GapHeight = gapHeight;
        Width = width;
    }

    public float X { get; set; }

    public float GapY { get; set; }

    public float GapHeight { get; set; }

    public float Width { get; set; }

    public bool HasBeenPassed { get; set; }
}
