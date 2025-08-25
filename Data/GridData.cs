using System;
using UnityEngine;
using System.Collections.Generic;

namespace Pathfinding.Data
{
    [Serializable]
    public class GridData
    {
        [field: SerializeField] public float CellSize { get; private set; } = 10;
        [field: SerializeField] public MeshRenderer MeshRenderer { get; private set; }
        [field: SerializeField] public Transform Parent { get; private set; }
        [field: SerializeField] public bool CreateLevelOnPlay { get; private set; }
        [field: SerializeField] public Transform BlockPrefab { get; private set; }
        [field: SerializeField] public LayerMask TerrainLayer { get; private set; }
        [field: SerializeField] public Transform EntranceTransform { get; private set; }
        [field: SerializeField] public Transform ExitTransform { get; private set; }
        [field: SerializeField] public LayerMask InaccessibleLayer { get; private set; }
        [field: SerializeField] public int ForestCost { get; private set; }

        // List of valid grid positions for agent movement
        public List<Vector2Int> ValidGridPositions { get; set; } = new List<Vector2Int>();
        // 2D array of grid square data
        public GridSquareData[,] Squares { get; set; }
        // Grid dimensions
        public int Width { get; set; } = 10;
        public int Height { get; set; } = 10;
        // Entrance and exit positions in the grid
        public Vector2Int Entrance { get; set; }
        public Vector2Int Exit { get; set; }
        // Data for procedural prop and vegetation spawning
        public GridSpawnData PropSpawnData { get; set; }
        public GridSpawnData VegetationSpawnData { get; set; }
    }
}