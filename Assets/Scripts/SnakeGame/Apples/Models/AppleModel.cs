using System;
using System.Threading.Tasks;
using SnakeGame.Board.Services;
using SnakeGame.Game.Models;

namespace SnakeGame.Apples.Models
{
    public class AppleModel
    {
        private readonly BoardService _boardService;
        private BoardPosition _currentApplePosition;

        public AppleModel(BoardService boardService)
        {
            _boardService = boardService;
        }

        public Action<BoardPosition> OnAppleSpawn = delegate {};
        public Action OnAppleEaten = delegate {};
        public BoardPosition CurrentPosition => _currentApplePosition;

        public async Task SpawnNewAppleAsync()
        {
            if (_currentApplePosition != null)
                OnAppleEaten();

            _currentApplePosition = null;
            var randomEmptyPosition = await _boardService.RandomEmptyPositionAsync();
            _currentApplePosition = randomEmptyPosition;
            OnAppleSpawn(_currentApplePosition);
        }
    }
}