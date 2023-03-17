using System;
using System.Collections.Generic;
using SnakeGame.Board.Configs;
using SnakeGame.Game.Views;
using SnakeGame.Snakes.Models;

namespace SnakeGame.Game.Models
{
    public class GameModel
    {
        private readonly GameConfig _gameConfig;
        private readonly BoardConfig _boardConfig;
        private readonly GameUpdateProvider _gameUpdateProvider;

        private List<Snake> _snakes = new List<Snake>();
        private int[,] _board;

        public GameModel(GameConfig gameConfig, BoardConfig boardConfig, GameUpdateProvider gameUpdateProvider)
        {
            _gameConfig = gameConfig;
            _boardConfig = boardConfig;
            _gameUpdateProvider = gameUpdateProvider;

            _board = new int[_boardConfig.BoardWidth, _boardConfig.BoardHeight];

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
                _board[startingPosition.X, startingPosition.Y]++;
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

                if (nextHeadPosition.IsOutOfBoundaries(_boardConfig.BoardWidth, _boardConfig.BoardHeight))
                {
                    snake.IsAlive = false;
                    return;
                }

                for (var bodyPartIndex = snake.BodyPartPositions.Count - 1; bodyPartIndex > 0; bodyPartIndex--)
                {
                    var currentBodyPartPosition = snake.BodyPartPositions[bodyPartIndex];
                    var nextBodyPartPosition = snake.BodyPartPositions[bodyPartIndex-1];

                    _board[currentBodyPartPosition.X, currentBodyPartPosition.Y]--;
                    _board[nextBodyPartPosition.X, nextBodyPartPosition.Y]++;

                    snake.BodyPartPositions[bodyPartIndex] = snake.BodyPartPositions[bodyPartIndex - 1];
                }

                _board[currentHeadPosition.X, currentHeadPosition.Y]--;
                _board[nextHeadPosition.X, nextHeadPosition.Y]++;

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
                        _board[bodyPartPosition.X, bodyPartPosition.Y]--;
                    }

                    OnRemoveSnake(snake);
                    _snakes.RemoveAt(snakeIndex);
                }
            }
        }

        /// <summary>
        /// Snakes are positioned horizontally, leaving room on top and below, adding a random vertical offset
        /// </summary>
        private List<List<BoardPosition>> CalculateStartingPositions(int numberOfSnakes, int startingSnakeSize, int boardWidth, int boardHeight)
        {
            var allStartingPositions = new List<List<BoardPosition>>();

            var widthPerSnake = boardWidth / numberOfSnakes;
            var y = startingSnakeSize - 1;
            var heightAvailableToFitBody = Math.Max(0, boardHeight - y - startingSnakeSize);
            for (var snakeIndex = 0; snakeIndex < numberOfSnakes; snakeIndex++)
            {
                var startingPositions = new List<BoardPosition>();

                var x = widthPerSnake / 2 + snakeIndex * widthPerSnake;
                var randomYOffset = UnityEngine.Random.Range(0, heightAvailableToFitBody);

                for (var bodyPartIndex = 0; bodyPartIndex < startingSnakeSize; bodyPartIndex++)
                {
                    startingPositions.Add(new BoardPosition(x, y + randomYOffset + bodyPartIndex));
                }

                allStartingPositions.Add(startingPositions);
            }

            return allStartingPositions;
        }
    }
}