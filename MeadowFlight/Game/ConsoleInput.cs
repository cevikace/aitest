namespace MeadowFlight.Game;

public sealed class ConsoleInput : IGameInput
{
    public ConsoleInputState ReadState()
    {
        var flap = false;
        var quit = false;
        var restart = false;

        while (Console.KeyAvailable)
        {
            var key = Console.ReadKey(intercept: true).Key;
            switch (key)
            {
                case ConsoleKey.Spacebar:
                case ConsoleKey.UpArrow:
                    flap = true;
                    break;
                case ConsoleKey.Q:
                case ConsoleKey.Escape:
                    quit = true;
                    break;
                case ConsoleKey.R:
                    restart = true;
                    break;
            }
        }

        return new ConsoleInputState
        {
            FlapPressed = flap,
            QuitRequested = quit,
            RestartRequested = restart
        };
    }
}
