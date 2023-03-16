using UnityEngine;

namespace SnakeGame.Board.Configs
{
    [CreateAssetMenu(fileName = "BoardConfig", menuName = "SnakeGame/BoardConfig")]
    public class BoardConfig : ScriptableObject
    {
        public int BoardWidth = 10;
        public int BoardHeight = 10;
    }
}