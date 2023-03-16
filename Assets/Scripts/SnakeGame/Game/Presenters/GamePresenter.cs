using System.Collections.Generic;
using SnakeGame.Game.Models;
using SnakeGame.Game.Views;
using SnakeGame.Snakes.Views;

namespace SnakeGame.Game.Presenters
{
    public class GamePresenter
    {
        private readonly GameModel _gameModel;
        private readonly GameView _gameView;
        private readonly SnakeViewFactory _snakeViewFactory;
        private readonly Dictionary<Snake, SnakeView> _snakeViewMap;

        public GamePresenter(GameModel gameModel, GameView gameView, SnakeViewFactory snakeViewFactory)
        {
            _gameModel = gameModel;
            _gameView = gameView;
            _snakeViewFactory = snakeViewFactory;

            _snakeViewMap = new Dictionary<Snake, SnakeView>();
        }

        public void Initialize()
        {
            _gameModel.Setup();
            CreateSnakeViews();
            UpdateSnakePositions();
            _gameModel.OnUpdateSnakePositions += UpdateSnakePositions;
            _gameView.OnViewDestroyed += OnViewDestroyed;
        }

        private void OnViewDestroyed()
        {
            _gameModel.OnUpdateSnakePositions -= UpdateSnakePositions;
            _gameView.OnViewDestroyed -= OnViewDestroyed;

            _gameModel.Dispose();
        }

        private void CreateSnakeViews()
        {
            foreach (var snake in _gameModel.Snakes)
            {
                var snakeView = _snakeViewFactory.CreateSnake();
                for (var bodyPartIndex = 0; bodyPartIndex < _gameModel.StartingSnakeSize; bodyPartIndex++)
                {
                    var bodyPart = _snakeViewFactory.CreateBodyPart();
                    snakeView.AddBodyPart(bodyPart);
                }

                _snakeViewMap[snake] = snakeView;
            }
        }

        private void UpdateSnakePositions()
        {
            foreach (var snake in _gameModel.Snakes)
            {
                var snakeView = _snakeViewMap[snake];
                for (var bodyPartIndex = 0; bodyPartIndex < snake.Size; bodyPartIndex++)
                {
                    var bodyPartPosition = snake.BodyPartPositions[bodyPartIndex];
                    snakeView.SetBodyPartPosition(bodyPartIndex, bodyPartPosition);
                }
            }
        }
    }
}