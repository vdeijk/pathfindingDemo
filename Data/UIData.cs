using System;
using UnityEngine;

namespace Pathfinding.Data
{
    [Serializable]
    public class UIData
    {
        [field: SerializeField] public CanvasGroup LevelCompletedCV { get; private set; }
        [field: SerializeField] public CanvasGroup MainMenuCV { get; private set; }
    }
}
