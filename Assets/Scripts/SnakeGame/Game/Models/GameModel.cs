using System;
using System.Collections.Generic;
using SnakeGame.Board.Services;
using SnakeGame.Snakes.Models;

namespace SnakeGame.Game.Models
{
    public class GameModel
    {
        private readonly GameConfig _gameConfig;
        private readonly GameUpdateProvider _gameUpdateProvider;
        private readonly BoardService _boardService;

        private List<Snake> _snakes = new List<Snake>();

        public GameModel(GameConfig gameConfig, GameUpdateProvider gameUpdateProvider,
            BoardService boardService)
        {
            _gameConfig = gameConfig;
            _gameUpdateProvider = gameUpdateProvider;
            _boardService = boardService;

            _gameUpdateProvider.Stop();
            _gameUpdateProvider.OnTick += SnakeMovementStep;
        }

        public Action OnUpdateSnakePositions = delegate { };
        public Action<Snake> OnRemoveSnake = delegate { };
        public List<Snake> Snakes => _snakes;
        public int StartingSnakeSize => _gameConfig.StartingSnakeSize;
        public SnakeMovementDirection StartingMovementDirection => _gameConfig.StartingMovementDirection;

        public void StartGame()
        {
            _gameUpdateProvider.Run();
        }

        public void Dispose()
        {
            _gameUpdateProvider.Stop();
            _gameUpdateProvider.OnTick -= SnakeMovementStep;
        }

        public void AddSnake(Snake snake)
        {
            _snakes.Add(snake);

            foreach (var startingPosition in snake.BodyPartPositions)
            {
                _boardService.OccupySlot(startingPosition);
            }
        }

        private void SnakeMovementStep()
        {
            MoveSnakes();
            CheckForDeaths();
            OnUpdateSnakePositions();
        }

        private void MoveSnakes()
        {
            foreach (var snake in _snakes)
            {
                var currentHeadPosition = snake.BodyPartPositions[0];
                var nextHeadPosition = currentHeadPosition + snake.CurrentMovementDirection().ToBoardPosition();

                if (_boardService.IsOutOfBoundaries(nextHeadPosition) || _boardService.IsSlotOccupied(nextHeadPosition))
                {
                    snake.IsAlive = false;
                    return;
                }

                for (var bodyPartIndex = snake.BodyPartPositions.Count - 1; bodyPartIndex > 0; bodyPartIndex--)
                {
                    var currentBodyPartPosition = snake.BodyPartPositions[bodyPartIndex];
                    var nextBodyPartPosition = snake.BodyPartPositions[bodyPartIndex-1];

                    _boardService.FreeSlot(currentBodyPartPosition);
                    _boardService.OccupySlot(nextBodyPartPosition);

                    snake.BodyPartPositions[bodyPartIndex] = snake.BodyPartPositions[bodyPartIndex - 1];
                }

                _boardService.FreeSlot(currentHeadPosition);
                _boardService.OccupySlot(nextHeadPosition);

                snake.BodyPartPositions[0] = nextHeadPosition;
            }
        }

        private void CheckForDeaths()
        {
            var snakeCount = _snakes.Count;
            for (var snakeIndex = snakeCount - 1; snakeIndex >= 0; snakeIndex --)
            {
                var snake = _snakes[snakeIndex];
                if (!snake.IsAlive)
                {
                    for (var bodyPartIndex = 0; bodyPartIndex < snake.Size; bodyPartIndex++)
                    {
                        var bodyPartPosition = snake.BodyPartPositions[bodyPartIndex];
                        _boardService.FreeSlot(bodyPartPosition);
                    }

                    OnRemoveSnake(snake);
                    _snakes.RemoveAt(snakeIndex);
                }
            }
        }
    }
}