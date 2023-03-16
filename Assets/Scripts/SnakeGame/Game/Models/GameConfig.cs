using UnityEngine;

namespace SnakeGame.Game.Models
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "SnakeGame/GameConfig")]
    public class GameConfig : ScriptableObject
    {
        public int BoardWidth;
        public int BoardHeight;
        public int NumberOfSnakes;
        public int StartingSnakeSize;
        public float UpdateIntervalInSeconds;
        public BoardPosition StartingMovementDirection;
    }
}