using System;
using UnityEngine;

namespace Pathfinding.Data
{
    [Serializable]
    public class AIData
    {
        [field: SerializeField] public float MinWaitTime { get; private set; }
        [field: SerializeField] public float MaxWaitTime { get; private set; }
        [field: SerializeField] public bool IsEnemy { get; private set; } 

        // Current wait time for AI actions
        public float CurWaitTime { get; set; }
        // Patrol points for AI movement
        public Vector2Int PointA { get; set; }
        public Vector2Int PointB { get; set; }
        // Indicates if the agent is currently waiting
        public bool IsWaiting { get; set; } = true;
    }
}