using UnityEngine;

namespace SnakeGame.Board.Views
{
    public class BoardView : MonoBehaviour
    {
        [SerializeField] private Transform _boardTransform;

        public int BoardWidth
        {
            set
            {
                var currentScale = _boardTransform.localScale;
                _boardTransform.localScale = new Vector3(value, currentScale.y, currentScale.z);
            }
        }

        public int BoardHeight
        {
            set
            {
                var currentScale = _boardTransform.localScale;
                _boardTransform.localScale = new Vector3(currentScale.x, value, currentScale.z);
            }
        }

        public void OffsetPosition(int x, int y)
        {
            var offset = new Vector3(x, y, 0);
            _boardTransform.position += offset;
        }
    }
}