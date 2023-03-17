using System.Collections.Generic;
using SnakeGame.InputControls.Models;
using UnityEngine;

namespace SnakeGame.InputControls.Config
{
    [CreateAssetMenu(fileName = "DefaultInputControlMaps", menuName = "SnakeGame/DefaultInputControlMaps")]
    public class DefaultInputControlMaps : ScriptableObject
    {
        public List<InputControlMap> InputControlMaps;
    }
}