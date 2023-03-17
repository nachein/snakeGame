using System.Collections.Generic;
using SnakeGame.Snakes.Models;

namespace SnakeGame.Game.Models
{
    public class Snake
    {
        private readonly List<BoardPosition> _bodyPartPositions;
        private readonly MovementInput _movementInput;

        private SnakeMovementDirection _currentMovementDirection;
        private SnakeMovementDirection _nextMovementDirection;
        private int _snakeSize;

        public Snake(List<BoardPosition> startingPositions, SnakeMovementDirection startingMovementDirection,
            MovementInput movementInput)
        {
            _bodyPartPositions = startingPositions;
            _currentMovementDirection = startingMovementDirection;
            _nextMovementDirection = startingMovementDirection;
            _movementInput = movementInput;

            _snakeSize = startingPositions.Count;
            IsAlive = true;

            _movementInput.OnDirectionChanged += OnMovementDirectionChange;
        }

        public List<BoardPosition> BodyPartPositions => _bodyPartPositions;
        public int Size => _snakeSize;
        public bool IsAlive { get; set; }

        private void OnMovementDirectionChange(SnakeMovementDirection newMovementDirection)
        {
            if (_currentMovementDirection == newMovementDirection ||
                _currentMovementDirection.AreOppositeDirections(newMovementDirection))
                return;

            _nextMovementDirection = newMovementDirection;
        }

        public SnakeMovementDirection CurrentMovementDirection()
        {
            _currentMovementDirection = _nextMovementDirection;
            return _currentMovementDirection;
        }
    }
}