namespace MeadowFlight.Game;

public interface IGameRenderer
{
    void Render(GameState state);

    void RenderGameOver(GameState state);
}
