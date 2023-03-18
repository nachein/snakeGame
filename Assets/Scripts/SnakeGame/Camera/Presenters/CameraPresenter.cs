using SnakeGame.Board.Configs;
using SnakeGame.Camera.Views;
using UnityEngine.Device;

namespace SnakeGame.Camera.Presenters
{
    public class CameraPresenter
    {
        private readonly CameraView _cameraView;
        private readonly BoardConfig _boardConfig;

        public CameraPresenter(CameraView cameraView, BoardConfig boardConfig)
        {
            _cameraView = cameraView;
            _boardConfig = boardConfig;
        }

        public void Initialize()
        {
            var isScreenLandscape = Screen.width > Screen.height;

            if (_boardConfig.BoardHeight == _boardConfig.BoardWidth)
            {
                _cameraView.CameraOrthographicSize = isScreenLandscape
                    ? _boardConfig.BoardHeight * 0.5f
                    : _boardConfig.BoardWidth / _cameraView.CameraAspectRatio * 0.5f;
            }
            else if (_boardConfig.BoardHeight > _boardConfig.BoardWidth)
            {
                _cameraView.CameraOrthographicSize = _boardConfig.BoardHeight * 0.5f;
            }
            else
            {
                _cameraView.CameraOrthographicSize = _boardConfig.BoardWidth / _cameraView.CameraAspectRatio * 0.5f;
            }

            var cameraOffsetX = _boardConfig.BoardWidth / 2f - 0.5f;
            var cameraOffsetY = _boardConfig.BoardHeight / 2f - 0.5f;
            _cameraView.OffsetPosition(cameraOffsetX, cameraOffsetY);
        }
    }
}