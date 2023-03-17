using SnakeGame.Apples.Models;
using SnakeGame.Apples.Views;
using SnakeGame.Board.Services;
using SnakeGame.Game.Models;
using Object = UnityEngine.Object;

namespace SnakeGame.Apples.Presenters
{
    public class ApplePresenter
    {
        private readonly AppleView _appleViewPrefab;
        private readonly AppleModel _appleModel;
        private readonly BoardService _boardService;
        private AppleView _appleView;

        public ApplePresenter(AppleView appleViewPrefab, AppleModel appleModel)
        {
            _appleViewPrefab = appleViewPrefab;
            _appleModel = appleModel;
        }

        public void Initialize()
        {
            _appleView = Object.Instantiate(_appleViewPrefab);
            _appleView.IsVisible = false;

            _appleModel.OnAppleEaten += HideApple;
            _appleModel.OnAppleSpawn += SpawnApple;
        }

        private void HideApple()
        {
            _appleView.IsVisible = false;
        }

        private void SpawnApple(BoardPosition boardPosition)
        {
            _appleView.BoardPosition = boardPosition;
            _appleView.IsVisible = true;
        }
    }
}