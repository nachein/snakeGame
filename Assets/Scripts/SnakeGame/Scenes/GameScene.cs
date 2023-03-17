using System;
using SnakeGame.Board.Configs;
using SnakeGame.Board.Models;
using SnakeGame.Board.Presenters;
using SnakeGame.Board.Services;
using SnakeGame.Board.Views;
using SnakeGame.Camera.Presenters;
using SnakeGame.Camera.Views;
using SnakeGame.Game.Models;
using SnakeGame.Game.Presenters;
using SnakeGame.Game.Views;
using SnakeGame.InputControls.Config;
using SnakeGame.Snakes.Views;
using UnityEngine;

namespace SnakeGame.Scenes
{
    public class GameScene : MonoBehaviour
    {
        [SerializeField] private int _numberOfPlayers = 1;

        [Header("Configs")]
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private BoardConfig _boardConfig;
        [SerializeField] private DefaultInputControlMaps _defaultInputControlMaps;

        [Header("Prefabs")]
        [SerializeField] private SnakeView _snakeViewPrefab;
        [SerializeField] private SnakeBodyPartView _snakeBodyPartViewPrefab;

        [Header("Scene References")]
        [SerializeField] private CameraView _cameraView;
        [SerializeField] private BoardView _boardView;
        [SerializeField] private GameView _gameView;
        [SerializeField] private UnityGameUpdateProvider _gameUpdateProvider;

        private void Awake()
        {
            var cameraPresenter = new CameraPresenter(_cameraView, _boardConfig);
            cameraPresenter.Initialize();

            var boardModel = new BoardModel(_boardConfig);
            var boardService = new BoardService(boardModel);

            var boardPresenter = new BoardPresenter(_boardView, _boardConfig);
            boardPresenter.Initialize();

            var snakeViewFactory = new SnakeViewFactory(_snakeViewPrefab, _snakeBodyPartViewPrefab);

            _gameUpdateProvider.TickIntervalInSeconds = _gameConfig.UpdateIntervalInSeconds;
            var gameModel = new GameModel(_gameConfig, _boardConfig, _gameUpdateProvider);
            var gamePresenter = new GamePresenter(gameModel, _gameView, snakeViewFactory, boardService, _defaultInputControlMaps);
            gamePresenter.Initialize(Math.Max(1, _numberOfPlayers));

            gameModel.StartGame();
        }
    }
}
