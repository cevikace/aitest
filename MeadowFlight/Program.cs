using MeadowFlight.Game;

var config = GameConfig.Default;
var runner = new GameRunner(config, new ConsoleInput(), new ConsoleRenderer(config));

Console.WriteLine("Welcome to Meadow Flight! Tap space to flap. Press Q to quit.");
runner.Run();
