using SnakeGame.Game.Models;
using SnakeGame.Game.Views;
using UnityEngine;

namespace SnakeGame.Apples.Views
{
    public class AppleView : MonoBehaviour
    {
        public bool IsVisible
        {
            set => gameObject.SetActive(value);
        }

        public BoardPosition BoardPosition
        {
            set => transform.position = value.ToVector3();
        }
    }
}