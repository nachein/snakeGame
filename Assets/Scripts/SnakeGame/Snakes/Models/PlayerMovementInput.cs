using System;
using SnakeGame.Snakes.Views;
using UnityEngine;

namespace SnakeGame.Snakes.Models
{
    public class PlayerMovementInput : MovementInput
    {
        private readonly PlayerInputView _playerInputView;

        public PlayerMovementInput(PlayerInputView playerInputView)
        {
            _playerInputView = playerInputView;

            _playerInputView.MoveInput += OnInputMove;
        }

        public event Action<SnakeMovementDirection> OnDirectionChanged;

        public void Dispose()
        {
            _playerInputView.MoveInput -= OnInputMove;
        }

        private void OnInputMove(Vector2 movementVector)
        {
            if (movementVector == Vector2.right)
            {
                OnDirectionChanged(SnakeMovementDirection.RIGHT);
                return;
            }
                
            if (movementVector == Vector2.left)
            {
                OnDirectionChanged(SnakeMovementDirection.LEFT);
                return;
            }
                
            if (movementVector == Vector2.up)
            {
                OnDirectionChanged(SnakeMovementDirection.UP);
                return;
            }

            if (movementVector == Vector2.down)
            {
                OnDirectionChanged(SnakeMovementDirection.DOWN);
            }
        }
    }
}