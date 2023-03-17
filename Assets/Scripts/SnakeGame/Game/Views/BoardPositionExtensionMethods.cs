using SnakeGame.Game.Models;
using UnityEngine;

namespace SnakeGame.Game.Views
{
    public static class BoardPositionExtensionMethods
    {
        public static Vector3 ToVector3(this BoardPosition boardPosition)
        {
            return new Vector3(boardPosition.X, boardPosition.Y, 0);
        }

        public static bool IsOutOfBoundaries(this BoardPosition position, int boardWidth, int boardHeight)
        {
            return position.X < 0 || position.X >= boardWidth || position.Y < 0 || position.Y >= boardHeight;
        }
    }
}