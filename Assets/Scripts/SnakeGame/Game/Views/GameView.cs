using System;
using UnityEngine;

namespace SnakeGame.Game.Views
{
    public class GameView : MonoBehaviour
    {
        public Action OnViewDestroyed = delegate { };
        
        public void OnDestroy()
        {
            OnViewDestroyed.Invoke();
        }
    }
}