namespace MeadowFlight.Game;

public sealed record ConsoleInputState
{
    public bool FlapPressed { get; init; }

    public bool QuitRequested { get; init; }

    public bool RestartRequested { get; init; }
}
