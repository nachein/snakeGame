using System;
using System.Collections.Generic;
using SnakeGame.Board.Configs;
using SnakeGame.Game.Models;

namespace SnakeGame.Board.Models
{
    public class BoardModel
    {
        private readonly BoardConfig _boardConfig;

        public BoardModel(BoardConfig boardConfig)
        {
            _boardConfig = boardConfig;
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
    }
}