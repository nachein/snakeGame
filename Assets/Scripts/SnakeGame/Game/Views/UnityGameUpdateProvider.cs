using System;
using SnakeGame.Game.Models;
using UnityEngine;

namespace SnakeGame.Game.Views
{
    public class UnityGameUpdateProvider : MonoBehaviour, GameUpdateProvider
    {
        private float _timeSinceLastMovementUpdate;
        private bool _shouldProcessMovement;
        private float _tickIntervalInSeconds = 0.5f;

        public event Action OnTick = delegate {};

        public void Run()
        {
            _shouldProcessMovement = true;
        }

        public void Stop()
        {
            _shouldProcessMovement = false;
            _timeSinceLastMovementUpdate = 0;
        }

        public bool ShouldUpdateMovement
        {
            set => _shouldProcessMovement = value;
        }

        public float TickIntervalInSeconds
        {
            set => _tickIntervalInSeconds = value;
        }

        private void Update()
        {
            if (!_shouldProcessMovement)
                return;

            _timeSinceLastMovementUpdate += Time.deltaTime;
            if (_timeSinceLastMovementUpdate >= _tickIntervalInSeconds)
            {
                OnTick.Invoke();
                _timeSinceLastMovementUpdate = 0;
            }
        }
    }
}