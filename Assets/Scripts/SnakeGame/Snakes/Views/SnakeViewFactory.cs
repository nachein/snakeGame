using SnakeGame.Game.Views;
using UnityEngine;

namespace SnakeGame.Snakes.Views
{
    public class SnakeViewFactory
    {
        private readonly SnakeView _snakePrefab;
        private readonly SnakeBodyPartView _snakeBodyPartPrefab;

        public SnakeViewFactory(SnakeView snakePrefab, SnakeBodyPartView snakeBodyPartPrefab)
        {
            _snakePrefab = snakePrefab;
            _snakeBodyPartPrefab = snakeBodyPartPrefab;
        }

        public SnakeView CreateSnake()
        {
            return Object.Instantiate(_snakePrefab);
        }

        public SnakeBodyPartView CreateBodyPart()
        {
            return Object.Instantiate(_snakeBodyPartPrefab);
        }
    }
}