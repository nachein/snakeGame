using System;

namespace SnakeGame.Snakes.Models
{
    public interface MovementInput
    {
        event Action<SnakeMovementDirection> OnDirectionChanged;
    }
}