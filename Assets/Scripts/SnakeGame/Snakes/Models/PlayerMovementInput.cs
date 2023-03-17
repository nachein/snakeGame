using System;
using SnakeGame.Snakes.Views;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SnakeGame.Snakes.Models
{
    public class PlayerMovementInput : MovementInput
    {
        private readonly InputAction _movementInputAction;
        private readonly PlayerInputView _playerInputView;

        public PlayerMovementInput(InputAction movementInputAction)
        {
            _movementInputAction = movementInputAction;
            _movementInputAction.started += OnInputMove;
        }

        public event Action<SnakeMovementDirection> OnDirectionChanged;

        public void Dispose()
        {
            _movementInputAction.started -= OnInputMove;
        }

        private void OnInputMove(InputAction.CallbackContext callbackContext)
        {
            var movementVector = callbackContext.ReadValue<Vector2>();
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