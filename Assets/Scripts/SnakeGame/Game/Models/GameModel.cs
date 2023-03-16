using System;
using System.Collections.Generic;
using System.Timers;

namespace SnakeGame.Game.Models
{
    public class GameModel
    {
        private readonly GameConfig _gameConfig;

        private List<Snake> _snakes = new List<Snake>();
        private int[,] _board;
        private Timer _updateTimer;

        public GameModel(GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
        }

        public Action OnUpdateSnakePositions = delegate { };
        public List<Snake> Snakes => _snakes;
        public int StartingSnakeSize => _gameConfig.StartingSnakeSize;

        public void Setup()
        {
            var boardWidth = _gameConfig.BoardWidth;
            var boardHeight = _gameConfig.BoardHeight;
            var numberOfSnakes = _gameConfig.NumberOfSnakes;
            var startingSnakeSize = _gameConfig.StartingSnakeSize;
            var updateIntervalInSeconds = _gameConfig.UpdateIntervalInSeconds;
            var startingMovementDirection = _gameConfig.StartingMovementDirection;

            _board = new int[boardWidth, boardHeight];
            _updateTimer = new Timer(updateIntervalInSeconds);
            _updateTimer.Elapsed += (sender, e) => GameUpdate();

            var allSnakeStartingPositions = CalculateStartingPositions(numberOfSnakes, startingSnakeSize, boardWidth, boardHeight);
            for (var snakeIndex = 0; snakeIndex < numberOfSnakes; snakeIndex++)
            {
                var startingPositions = allSnakeStartingPositions[snakeIndex];
                _snakes.Add(new Snake(startingPositions, startingMovementDirection));
                foreach (var startingPosition in startingPositions)
                {
                    _board[startingPosition.X, startingPosition.Y]++;
                }
            }
        }

        public void StartGame()
        {
            _updateTimer.Start();
        }

        private void GameUpdate()
        {
            MoveSnakes();
            // CheckForDeaths();
            OnUpdateSnakePositions();
        }

        private void MoveSnakes()
        {
            foreach (var snake in _snakes)
            {
                for (var bodyPartIndex = snake.BodyPartPositions.Count - 1; bodyPartIndex > 0; bodyPartIndex--)
                {
                    var currentBodyPartPosition = snake.BodyPartPositions[bodyPartIndex];
                    var nextBodyPartPosition = snake.BodyPartPositions[bodyPartIndex-1];

                    _board[currentBodyPartPosition.X, currentBodyPartPosition.Y]--;
                    _board[nextBodyPartPosition.X, nextBodyPartPosition.Y]++;

                    snake.BodyPartPositions[bodyPartIndex] = snake.BodyPartPositions[bodyPartIndex - 1];
                }

                var currentHeadPosition = snake.BodyPartPositions[0];
                var nextHeadPosition = currentHeadPosition + snake.MoveDirection;

                _board[currentHeadPosition.X, currentHeadPosition.Y]--;
                _board[nextHeadPosition.X, nextHeadPosition.Y]++;

                snake.BodyPartPositions[0] = nextHeadPosition;
            }
        }

        /// <summary>
        /// Snakes are positioned horizontally, leaving room on top and below, adding a random vertical offset
        /// </summary>
        private List<List<BoardPosition>> CalculateStartingPositions(int numberOfSnakes, int startingSnakeSize, int boardWidth, int boardHeight)
        {
            var allStartingPositions = new List<List<BoardPosition>>();

            var widthPerSnake = boardWidth / numberOfSnakes;
            var y = startingSnakeSize + 1;
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