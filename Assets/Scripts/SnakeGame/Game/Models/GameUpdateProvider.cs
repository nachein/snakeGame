using System;

namespace SnakeGame.Game.Models
{
    public interface GameUpdateProvider
    {
        event Action OnTick;
        void Run();
        void Stop();
        float TickIntervalInSeconds { set; }
    }
}