using SnakeGame.Game.Models;

namespace SnakeGame.Snakes.Models
{
    public static class SnakeMovementDirectionExtensionMethods
    {
        public static BoardPosition ToBoardPosition(this SnakeMovementDirection movementDirection)
        {
            switch (movementDirection)
            {
                case SnakeMovementDirection.DOWN:
                    return new BoardPosition(0, -1);
                case SnakeMovementDirection.UP:
                    return new BoardPosition(0, 1);
                case SnakeMovementDirection.RIGHT:
                    return new BoardPosition(1, 0);
                case SnakeMovementDirection.LEFT:
                    return new BoardPosition(-1, 0);
            }

            UnityEngine.Debug.LogWarning("Unsupported movement direction. Defaulting to Down direction");
            return new BoardPosition(-1, 0);
        }

        public static bool AreOppositeDirections(this SnakeMovementDirection a, SnakeMovementDirection b)
        {
            var sum = a.ToBoardPosition() + b.ToBoardPosition();
            return sum.X == 0 && sum.Y == 0;
        }
    }
}