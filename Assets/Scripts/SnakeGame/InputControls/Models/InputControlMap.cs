using System;

namespace SnakeGame.InputControls.Models
{
    [Serializable]
    public class InputControlMap
    {
        public InputDevice Device;
        public string UpKeyName;
        public string DownKeyName;
        public string LeftKeyName;
        public string RightKeyName;
    }
}