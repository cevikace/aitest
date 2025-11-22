diff --git a/MeadowFlight/Game/GameRunner.cs b/MeadowFlight/Game/GameRunner.cs
new file mode 100644
index 0000000000000000000000000000000000000000..b2d0ddfdfb83ab6abf49e26393d3621794207ccf
--- /dev/null
+++ b/MeadowFlight/Game/GameRunner.cs
@@ -0,0 +1,155 @@
+using System.Diagnostics;
+
+namespace MeadowFlight.Game;
+
+public sealed class GameRunner
+{
+    private readonly GameConfig _config;
+    private readonly IGameInput _input;
+    private readonly IGameRenderer _renderer;
+    private readonly GameState _state = new();
+    private readonly Random _random = new();
+
+    private float _nextObstacleOffset;
+
+    public GameRunner(GameConfig config, IGameInput input, IGameRenderer renderer)
+    {
+        _config = config;
+        _input = input;
+        _renderer = renderer;
+    }
+
+    public void Run()
+    {
+        Reset();
+        var targetDelta = 1f / _config.TargetFramesPerSecond;
+        var stopwatch = Stopwatch.StartNew();
+        var previous = stopwatch.Elapsed;
+
+        while (true)
+        {
+            var now = stopwatch.Elapsed;
+            var delta = (float)(now - previous).TotalSeconds;
+            previous = now;
+
+            var inputs = _input.ReadState();
+            if (inputs.QuitRequested)
+            {
+                return;
+            }
+
+            if (_state.IsGameOver)
+            {
+                if (inputs.RestartRequested)
+                {
+                    Reset();
+                }
+
+                Thread.Sleep(80);
+                continue;
+            }
+
+            if (inputs.FlapPressed)
+            {
+                _state.Player.Flap(_config.FlapImpulse);
+            }
+
+            Update(delta);
+            _renderer.Render(_state);
+
+            var sleepTime = targetDelta - delta;
+            if (sleepTime > 0)
+            {
+                Thread.Sleep((int)(sleepTime * 1000));
+            }
+        }
+    }
+
+    private void Reset()
+    {
+        _state.Player.X = 12f;
+        _state.Player.Y = _config.WorldHeight / 2f;
+        _state.Player.VelocityY = 0;
+        _state.Obstacles.Clear();
+        _state.ScrollOffset = 0;
+        _state.Score = 0;
+        _state.IsGameOver = false;
+        _state.DayNightTimer = 0;
+        _nextObstacleOffset = _config.WorldWidth + _config.ObstacleSpacing;
+
+        for (var i = 0; i < 3; i++)
+        {
+            SpawnObstacle(_config.WorldWidth + i * _config.ObstacleSpacing);
+        }
+    }
+
+    private void Update(float deltaTime)
+    {
+        _state.DayNightTimer += deltaTime;
+        _state.ScrollOffset += _config.ObstacleSpeed * deltaTime;
+        _state.Player.Integrate(_config.Gravity, deltaTime);
+
+        UpdateObstacles(deltaTime);
+        CheckCollisions();
+    }
+
+    private void UpdateObstacles(float deltaTime)
+    {
+        foreach (var obstacle in _state.Obstacles)
+        {
+            obstacle.X -= _config.ObstacleSpeed * deltaTime;
+            if (!obstacle.HasBeenPassed && obstacle.X + obstacle.Width < _state.Player.X)
+            {
+                obstacle.HasBeenPassed = true;
+                _state.Score++;
+            }
+        }
+
+        _state.Obstacles.RemoveAll(o => o.X + o.Width < 0);
+
+        _nextObstacleOffset -= _config.ObstacleSpeed * deltaTime;
+        if (_nextObstacleOffset <= 0)
+        {
+            SpawnObstacle(_config.WorldWidth);
+            _nextObstacleOffset = _config.ObstacleSpacing;
+        }
+    }
+
+    private void SpawnObstacle(float startX)
+    {
+        var minGapY = _config.GroundHeight + 2f;
+        var maxGapY = _config.WorldHeight - _config.ObstacleGapHeight - 2f;
+        var jitter = (float)_random.NextDouble() * _config.MaxGapJitter * 2 - _config.MaxGapJitter;
+        var gapY = Math.Clamp((_config.WorldHeight / 2f) + jitter, minGapY, maxGapY);
+
+        _state.Obstacles.Add(new Obstacle(startX, gapY, _config.ObstacleGapHeight, _config.ObstacleWidth)
+        {
+            HasBeenPassed = false
+        });
+    }
+
+    private void CheckCollisions()
+    {
+        if (_state.Player.Y <= _config.GroundHeight - 0.2f || _state.Player.Y >= _config.WorldHeight - 1)
+        {
+            _state.IsGameOver = true;
+            _renderer.RenderGameOver(_state);
+            return;
+        }
+
+        foreach (var obstacle in _state.Obstacles)
+        {
+            if (_state.Player.X < obstacle.X - 0.5f || _state.Player.X > obstacle.X + obstacle.Width + 0.5f)
+            {
+                continue;
+            }
+
+            if (_state.Player.Y < obstacle.GapY || _state.Player.Y > obstacle.GapY + obstacle.GapHeight)
+            {
+                _state.IsGameOver = true;
+                _renderer.RenderGameOver(_state);
+                return;
+            }
+        }
+    }
+}
