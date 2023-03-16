using SnakeGame.Board.Configs;
using SnakeGame.Board.Presenters;
using SnakeGame.Board.Views;
using SnakeGame.Camera.Presenters;
using SnakeGame.Camera.Views;
using SnakeGame.Game.Models;
using SnakeGame.Game.Presenters;
using SnakeGame.Snakes.Views;
using UnityEngine;

namespace SnakeGame.Scenes
{
    public class GameScene : MonoBehaviour
    {
        [Header("Configs")]
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private BoardConfig _boardConfig;

        [Header("Prefabs")]
        [SerializeField] private SnakeView _snakeViewPrefab;
        [SerializeField] private SnakeBodyPartView _snakeBodyPartViewPrefab;

        [Header("Scene References")]
        [SerializeField] private CameraView _cameraView;
        [SerializeField] private BoardView _boardView;

        private void Awake()
        {
            var cameraPresenter = new CameraPresenter(_cameraView, _boardConfig);
            cameraPresenter.Initialize();

            var boardPresenter = new BoardPresenter(_boardView, _boardConfig);
            boardPresenter.Initialize();

            var snakeViewFactory = new SnakeViewFactory(_snakeViewPrefab, _snakeBodyPartViewPrefab);
            var gameModel = new GameModel(_gameConfig, _boardConfig);
            var gamePresenter = new GamePresenter(gameModel, snakeViewFactory);
            gamePresenter.Initialize();

            // gameModel.StartGame();
        }
    }
}
