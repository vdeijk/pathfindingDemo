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

        public float CurWaitTime { get; set; }
        public Vector2Int PointA { get; set; }
        public Vector2Int PointB { get; set; }
        public bool IsWaiting { get; set; } = true;
    }
}