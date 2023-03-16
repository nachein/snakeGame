using SnakeGame.Board.Configs;
using SnakeGame.Board.Views;

namespace SnakeGame.Board.Presenters
{
    public class BoardPresenter
    {
        private readonly BoardView _boardView;
        private readonly BoardConfig _boardConfig;

        public BoardPresenter(BoardView boardView, BoardConfig boardConfig)
        {
            _boardView = boardView;
            _boardConfig = boardConfig;
        }

        public void Initialize()
        {
            _boardView.BoardWidth = _boardConfig.BoardWidth;
            _boardView.BoardHeight = _boardConfig.BoardHeight;

            var boardOffsetX = _boardConfig.BoardWidth / 2;
            var boardOffsetY = _boardConfig.BoardHeight / 2;
            _boardView.OffsetPosition(boardOffsetX, boardOffsetY);
        }
    }
}