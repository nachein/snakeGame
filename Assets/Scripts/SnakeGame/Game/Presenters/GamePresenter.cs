using System.Collections.Generic;
using SnakeGame.Board.Services;
using SnakeGame.Game.Models;
using SnakeGame.Game.Views;
using SnakeGame.Snakes.Models;
using SnakeGame.Snakes.Views;
using UnityEngine.InputSystem;

namespace SnakeGame.Game.Presenters
{
    public class GamePresenter
    {
        private readonly GameModel _gameModel;
        private readonly GameView _gameView;
        private readonly SnakeViewFactory _snakeViewFactory;
        private readonly BoardService _boardService;
        private readonly InputActionAsset _inputActionAsset;
        private readonly Dictionary<Snake, SnakeView> _snakeViewMap;

        public GamePresenter(GameModel gameModel, GameView gameView, SnakeViewFactory snakeViewFactory,
            BoardService boardService, InputActionAsset inputActionAsset)
        {
            _gameModel = gameModel;
            _gameView = gameView;
            _snakeViewFactory = snakeViewFactory;
            _boardService = boardService;
            _inputActionAsset = inputActionAsset;

            _snakeViewMap = new Dictionary<Snake, SnakeView>();
        }

        public void Initialize(int numberOfPlayers)
        {
            var snakeSetups = new List<SnakeSetup>();
            for (var playerIndex = 0; playerIndex < numberOfPlayers; playerIndex++)
            {
                snakeSetups.Add(new SnakeSetup());
            }

            SetupSnakes(snakeSetups);
            UpdateAllSnakePositions();

            _gameModel.OnUpdateSnakePositions += UpdateAllSnakePositions;
            _gameModel.OnRemoveSnake += RemoveSnake;
            _gameView.OnViewDestroyed += OnViewDestroyed;
        }

        private void SetupSnakes(List<SnakeSetup> snakeSetups)
        {
            var numberOfSnakes = snakeSetups.Count;
            var allSnakeStartingPositions =
                _boardService.CalculateStartingPositions(numberOfSnakes, _gameModel.StartingSnakeSize);

            for (var snakeIndex = 0; snakeIndex < numberOfSnakes; snakeIndex++)
            {
                var snakeSetup = snakeSetups[snakeIndex];
                var snakeView = CreateSnakeView(snakeSetup);
                var movementInput = CreateMovementInput(snakeView, snakeSetup);
                var startingPositions = allSnakeStartingPositions[snakeIndex];
                var snake = new Snake(startingPositions, _gameModel.StartingMovementDirection, movementInput);

                _gameModel.AddSnake(snake);
                _snakeViewMap[snake] = snakeView;
            }
        }

        private SnakeView CreateSnakeView(SnakeSetup snakeSetup)
        {
            var snakeView = _snakeViewFactory.CreateSnake();
            for (var bodyPartIndex = 0; bodyPartIndex < _gameModel.StartingSnakeSize; bodyPartIndex++)
            {
                var bodyPart = _snakeViewFactory.CreateBodyPart();
                snakeView.AddBodyPart(bodyPart);
            }

            return snakeView;
        }

        private MovementInput CreateMovementInput(SnakeView snakeView, SnakeSetup snakeSetup)
        {
            var playerInput = snakeView.gameObject.AddComponent<PlayerInput>();
            playerInput.actions = _inputActionAsset;
            playerInput.SwitchCurrentActionMap(_inputActionAsset.actionMaps[0].name);

            var playerInputView = snakeView.gameObject.AddComponent<PlayerInputView>();

            return new PlayerMovementInput(playerInputView);
        }

        private void OnViewDestroyed()
        {
            _gameModel.OnUpdateSnakePositions -= UpdateAllSnakePositions;
            _gameModel.OnRemoveSnake -= RemoveSnake;
            _gameView.OnViewDestroyed -= OnViewDestroyed;

            _gameModel.Dispose();
        }

        private void UpdateAllSnakePositions()
        {
            foreach (var snake in _gameModel.Snakes)
            {
                UpdateSnakePosition(snake);
            }
        }

        private void UpdateSnakePosition(Snake snake)
        {
            var snakeView = _snakeViewMap[snake];
            for (var bodyPartIndex = 0; bodyPartIndex < snake.Size; bodyPartIndex++)
            {
                var bodyPartPosition = snake.BodyPartPositions[bodyPartIndex];
                snakeView.SetBodyPartPosition(bodyPartIndex, bodyPartPosition);
            }
        }

        private void RemoveSnake(Snake snakeToRemove)
        {
            _snakeViewMap[snakeToRemove].Kill();
            _snakeViewMap.Remove(snakeToRemove);
        }
    }
}