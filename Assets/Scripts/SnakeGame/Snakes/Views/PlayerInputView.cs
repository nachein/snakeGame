using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SnakeGame.Snakes.Views
{
    public class PlayerInputView : MonoBehaviour
    {
        public Action<Vector2> MoveInput = delegate {};
        
        public void OnMovementAction(InputValue value)
        {
            MoveInput.Invoke(value.Get<Vector2>());
        }
    }
}