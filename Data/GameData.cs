using System;
using UnityEngine;

namespace Pathfinding.Data
{
    [Serializable]
    public class GameData
    {
        public bool AreControlsEnabled { get; set; } = true;
        public Vector3 MoveInputs { get; set; }
        public float ZoomInput { get; set; }
        public bool LeftMouseInput { get; set; }
        public bool RightMouseInput { get; set; }
        public bool PauseInput { get; set; }
        public float RotateInput { get; set; }
        public bool ShowGridInput { get; set; }
    }
}