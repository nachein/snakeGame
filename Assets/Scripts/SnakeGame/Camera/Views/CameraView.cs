using UnityEngine;

namespace SnakeGame.Camera.Views
{
    public class CameraView : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera _camera;

        public float CameraOrthographicSize
        {
            set => _camera.orthographicSize = value;
        }

        public float CameraAspectRatio => _camera.aspect;

        public void OffsetPosition(int x, int y)
        {
            var offset = new Vector3(x, y, 0);
            _camera.transform.position += offset;
        }
    }
}