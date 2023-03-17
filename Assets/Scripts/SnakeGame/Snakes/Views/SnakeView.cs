using System.Collections.Generic;
using SnakeGame.Game.Models;
using SnakeGame.Game.Views;
using UnityEngine;

namespace SnakeGame.Snakes.Views
{
    public class SnakeView : MonoBehaviour
    {
        [SerializeField] private Transform _snakePartContainer;

        private List<SnakeBodyPartView> _bodyParts = new List<SnakeBodyPartView>();

        public void AddBodyPart(SnakeBodyPartView bodyPartView)
        {
            _bodyParts.Add(bodyPartView);
            bodyPartView.transform.SetParent(_snakePartContainer);
        }

        public void SetBodyPartPosition(int bodyPartIndex, BoardPosition bodyPartPosition)
        {
            _bodyParts[bodyPartIndex].transform.position = bodyPartPosition.ToVector3();
        }

        public void Kill()
        {
            Destroy(gameObject);
        }
    }
}