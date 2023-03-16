using SnakeGame.Game.Models;
using SnakeGame.Game.Presenters;
using SnakeGame.Snakes.Views;
using UnityEngine;

namespace SnakeGame.Scenes
{
    public class GameScene : MonoBehaviour
    {
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private SnakeView _snakeViewPrefab;
        [SerializeField] private SnakeBodyPartView _snakeBodyPartViewPrefab;

        private void Awake()
        {
            var snakeViewFactory = new SnakeViewFactory(_snakeViewPrefab, _snakeBodyPartViewPrefab);
            var gameModel = new GameModel(_gameConfig);
            var gamePresenter = new GamePresenter(gameModel, snakeViewFactory);

            gamePresenter.Initialize();

            // gameModel.StartGame();
        }
    }
}
