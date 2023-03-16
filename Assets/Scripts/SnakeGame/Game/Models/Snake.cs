using System.Collections.Generic;

namespace SnakeGame.Game.Models
{
    public class Snake
    {
        private readonly List<BoardPosition> _bodyPartPositions;
        private readonly BoardPosition _movementDirection;

        private int _snakeSize;

        public Snake(List<BoardPosition> startingPositions, BoardPosition startingMovementDirection)
        {
            _bodyPartPositions = startingPositions;
            _movementDirection = startingMovementDirection;

            _snakeSize = startingPositions.Count;
        }

        public List<BoardPosition> BodyPartPositions => _bodyPartPositions;
        public BoardPosition MoveDirection => _movementDirection;
        public int Size => _snakeSize;
    }
}