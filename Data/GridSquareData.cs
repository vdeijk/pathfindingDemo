using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Data
{
    [Serializable]
    public class GridSquareData
    {
        public List<AgentData> Agents { get; set; } = new List<AgentData>();
        public Vector2Int GridPosition { get; set; } = Vector2Int.zero;
        public List<GridSquareType> GridSquareType { get; set; } = new List<GridSquareType>();
        public GameObject GO { get; set; }
    }
}