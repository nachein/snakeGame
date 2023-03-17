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
    }
}