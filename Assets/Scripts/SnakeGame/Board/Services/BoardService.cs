using System.Collections.Generic;
using System.Threading.Tasks;
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

        public void OccupySlot(BoardPosition boardPosition)
        {
            _boardModel.OccupySlot(boardPosition);
        }

        public void FreeSlot(BoardPosition boardPosition)
        {
            _boardModel.FreeSlot(boardPosition);
        }

        public bool IsSlotOccupied(BoardPosition boardPosition)
        {
            return _boardModel.IsSlotOccupied(boardPosition);
        }
        
        public bool IsOutOfBoundaries(BoardPosition boardPosition)
        {
            return _boardModel.IsOutOfBoundaries(boardPosition);
        }

        public async Task<BoardPosition> RandomEmptyPositionAsync()
        {
            return await _boardModel.RandomEmptyPositionAsync();
        }
    }
}