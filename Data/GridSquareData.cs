using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Data
{
    [Serializable]
    public class GridSquareData
    {
        // Agents currently occupying this grid square
        public List<AgentData> Agents { get; set; } = new List<AgentData>();
        // Position of this square in the grid
        public Vector2Int GridPosition { get; set; } = Vector2Int.zero;
        // Types assigned to this grid square (e.g. Forest, Entrance)
        public List<GridSquareType> GridSquareType { get; set; } = new List<GridSquareType>();
        // Reference to the associated GameObject in the scene
        public GameObject GO { get; set; }
    }
}