using SnakeGame.Apples.Models;
using SnakeGame.Apples.Presenters;
using SnakeGame.Game.Models;

namespace SnakeGame.Apples.Services
{
    public class AppleService
    {
        private readonly AppleModel _appleModel;

        public AppleService(AppleModel appleModel)
        {
            _appleModel = appleModel;
        }

        public void SpawnNewAppleAsync()
        {
            _appleModel.SpawnNewAppleAsync();
        }

        public BoardPosition ApplePosition => _appleModel.CurrentPosition;
    }
}