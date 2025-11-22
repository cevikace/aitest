namespace MeadowFlight.Game;

public sealed class Player
{
    public float X { get; set; } = 12f;

    public float Y { get; set; } = 10f;

    public float VelocityY { get; set; }

    public void Flap(float impulse)
    {
        VelocityY = impulse;
    }

    public void Integrate(float gravity, float deltaTime)
    {
        VelocityY -= gravity * deltaTime;
        Y += VelocityY * deltaTime;
    }
}
