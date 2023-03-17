using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SnakeGame.Board.Configs;
using SnakeGame.Game.Models;

namespace SnakeGame.Board.Models
{
    public class BoardModel
    {
        private readonly BoardConfig _boardConfig;

        private int[,] _board;

        public BoardModel(BoardConfig boardConfig)
        {
            _boardConfig = boardConfig;
            _board = new int[_boardConfig.BoardWidth, _boardConfig.BoardHeight];
        }
        
        public List<List<BoardPosition>> CalculateStartingPositions(int numberOfSnakes, int startingSnakeSize)
        {
            var allStartingPositions = new List<List<BoardPosition>>();

            var widthPerSnake = _boardConfig.BoardWidth / numberOfSnakes;
            var y = startingSnakeSize - 1;
            var heightAvailableToFitBody = Math.Max(0, _boardConfig.BoardHeight - y - startingSnakeSize);
            for (var snakeIndex = 0; snakeIndex < numberOfSnakes; snakeIndex++)
            {
                var startingPositions = new List<BoardPosition>();

                var x = widthPerSnake / 2 + snakeIndex * widthPerSnake;
                var randomYOffset = UnityEngine.Random.Range(0, heightAvailableToFitBody);

                for (var bodyPartIndex = 0; bodyPartIndex < startingSnakeSize; bodyPartIndex++)
                {
                    startingPositions.Add(new BoardPosition(x, y + randomYOffset + bodyPartIndex));
                }

                allStartingPositions.Add(startingPositions);
            }

            return allStartingPositions;
        }

        public void FreeSlot(BoardPosition boardPosition)
        {
            _board[boardPosition.X, boardPosition.Y]--;
        }

        public void OccupySlot(BoardPosition boardPosition)
        {
            _board[boardPosition.X, boardPosition.Y]++;
        }

        public bool IsSlotOccupied(BoardPosition boardPosition)
        {
            return _board[boardPosition.X, boardPosition.Y] > 0;
        }

        public bool IsOutOfBoundaries(BoardPosition boardPosition)
        {
            return boardPosition.X < 0 || boardPosition.X >= _boardConfig.BoardWidth || boardPosition.Y < 0 ||
                   boardPosition.Y >= _boardConfig.BoardHeight;
        }

        public async Task<BoardPosition> RandomEmptyPositionAsync()
        {
            int randomX;
            int randomY;

            do
            {
                await Task.Yield();
                randomX = UnityEngine.Random.Range(0, _boardConfig.BoardWidth - 1);
                randomY = UnityEngine.Random.Range(0, _boardConfig.BoardHeight - 1);
            }
            while (IsSlotOccupied(new BoardPosition(randomX, randomY)));

            return new BoardPosition(randomX, randomY);
        }
    }
}