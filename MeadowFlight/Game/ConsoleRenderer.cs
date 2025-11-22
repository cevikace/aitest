using System.Text;

namespace MeadowFlight.Game;

public sealed class ConsoleRenderer : IGameRenderer
{
    private readonly GameConfig _config;

    public ConsoleRenderer(GameConfig config)
    {
        _config = config;
        Console.CursorVisible = false;
    }

    public void Render(GameState state)
    {
        var buffer = new char[_config.WorldHeight, _config.WorldWidth];
        FillBackground(buffer, state);
        DrawGround(buffer);
        DrawObstacles(buffer, state);
        DrawPlayer(buffer, state);

        var sb = new StringBuilder();
        for (var y = _config.WorldHeight - 1; y >= 0; y--)
        {
            for (var x = 0; x < _config.WorldWidth; x++)
            {
                sb.Append(buffer[y, x]);
            }

            sb.Append('\n');
        }

        sb.Append($"Score: {state.Score}    Cycle: {DescribeCycle(state)}\n");
        sb.Append("(SPACE/UP = flap, R = restart, Q/Esc = quit)\n");

        Console.SetCursorPosition(0, 0);
        Console.Write(sb.ToString());
    }

    public void RenderGameOver(GameState state)
    {
        Console.WriteLine();
        Console.WriteLine("🌸 Game over! Press R to restart or Q to quit.");
        Console.WriteLine($"Final score: {state.Score}");
    }

    private void FillBackground(char[,] buffer, GameState state)
    {
        var cycleValue = DayNightValue(state);
        for (var y = 0; y < _config.WorldHeight; y++)
        {
            for (var x = 0; x < _config.WorldWidth; x++)
            {
                buffer[y, x] = SkySymbol(cycleValue, y);
            }
        }
    }

    private void DrawGround(char[,] buffer)
    {
        for (var y = 0; y < _config.GroundHeight && y < _config.WorldHeight; y++)
        {
            for (var x = 0; x < _config.WorldWidth; x++)
            {
                buffer[y, x] = '_';
            }
        }
    }

    private void DrawObstacles(char[,] buffer, GameState state)
    {
        foreach (var obstacle in state.Obstacles)
        {
            var startX = (int)Math.Round(obstacle.X);
            var endX = (int)Math.Round(obstacle.X + obstacle.Width);
            for (var x = startX; x <= endX; x++)
            {
                if (x < 0 || x >= _config.WorldWidth)
                {
                    continue;
                }

                for (var y = 0; y < _config.WorldHeight; y++)
                {
                    if (y < obstacle.GapY || y > obstacle.GapY + obstacle.GapHeight)
                    {
                        buffer[y, x] = '|';
                    }
                }
            }
        }
    }

    private void DrawPlayer(char[,] buffer, GameState state)
    {
        var px = (int)Math.Round(state.Player.X);
        var py = (int)Math.Round(state.Player.Y);
        if (px >= 0 && px < _config.WorldWidth && py >= 0 && py < _config.WorldHeight)
        {
            buffer[py, px] = 'B';
        }
    }

    private char SkySymbol(float cycleValue, int y)
    {
        var gradientThreshold = (float)y / _config.WorldHeight;
        if (cycleValue > 0.6f && gradientThreshold > 0.7f)
        {
            return '✿';
        }

        if (cycleValue < 0.35f)
        {
            return '·';
        }

        return '˚';
    }

    private float DayNightValue(GameState state)
    {
        var t = (state.DayNightTimer % _config.DayNightCycleDurationSeconds) / _config.DayNightCycleDurationSeconds;
        return (float)(Math.Sin(t * Math.PI * 2) * 0.5 + 0.5);
    }

    private string DescribeCycle(GameState state)
    {
        var value = DayNightValue(state);
        return value switch
        {
            > 0.65f => "Day",
            > 0.45f => "Sunset",
            > 0.25f => "Night",
            _ => "Dawn"
        };
    }
}
