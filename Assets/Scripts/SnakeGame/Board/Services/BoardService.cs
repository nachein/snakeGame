using System.Collections.Generic;
using SnakeGame.Board.Models;
using SnakeGame.Game.Models;

namespace SnakeGame.Board.Services
{
    public class BoardService
    {
        private readonly BoardModel _boardModel;

        public BoardService(BoardModel boardModel)
        {
            _boardModel = boardModel;
        }

        public List<List<BoardPosition>> CalculateStartingPositions(int numberOfSnakes, int startingSnakeSize)
        {
            return _boardModel.CalculateStartingPositions(numberOfSnakes, startingSnakeSize);
        }
    }
}