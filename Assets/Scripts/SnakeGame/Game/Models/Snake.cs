using System.Collections.Generic;
using SnakeGame.Snakes.Models;

namespace SnakeGame.Game.Models
{
    public class Snake
    {
        private readonly List<BoardPosition> _bodyPartPositions;
        private readonly SnakeMovementDirection _movementDirection;

        private int _snakeSize;

        public Snake(List<BoardPosition> startingPositions, SnakeMovementDirection startingMovementDirection)
        {
            _bodyPartPositions = startingPositions;
            _movementDirection = startingMovementDirection;

            _snakeSize = startingPositions.Count;
        }

        public List<BoardPosition> BodyPartPositions => _bodyPartPositions;
        public SnakeMovementDirection MoveDirection => _movementDirection;
        public int Size => _snakeSize;
    }
}