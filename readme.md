# Meadow Flight

A lightweight C# implementation of a nature-inspired Flappy-style side scroller. The console build focuses on core mechanics (flap-to-fly controls, scrolling obstacles, scoring, and a simple day/night cycle) so you can iterate on art and sound assets.

## Running the console prototype

1. Install the .NET 6 SDK.
2. From the repo root, run:
   ```bash
   dotnet run --project MeadowFlight
   ```
3. Controls:
   - **Space / Up Arrow**: flap
   - **R**: restart after a collision
   - **Q / Esc**: quit

> Note: The container environment used for authoring does not have the .NET SDK installed, so compilation was not executed here.

## Project layout

- `MeadowFlight/Program.cs`: entry point that wires the game config, input, and renderer.
- `MeadowFlight/Game/`: core gameplay logic (state management, loop, console rendering, and input).
- `nature_flight_concept.md`: original art direction and feature notes.

## Extending the prototype

- Swap `ConsoleRenderer` with a real renderer (e.g., MonoGame or Unity) by implementing `IGameRenderer`.
- Replace `ConsoleInput` with platform-specific input.
- Tune values in `GameConfig` to adjust difficulty, pacing, or the day/night cycle.
