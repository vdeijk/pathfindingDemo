using Pathfinding.Data;
using UnityEngine;
using Zenject;

namespace Pathfinding.Services
{
    [DefaultExecutionOrder(100)]
    public class LevelUtilityService
    {
        public LevelData Data { get; set; }

        public void Init(LevelData data)
        {
            Data = data;
        }

        // Converts grid position to world position
        public Vector3 GetWorldPosition(Vector2Int gridPosition)
        {
            // Calculate the world position based on grid position, cell size, and terrain height
            var center = Data.MeshRenderer.bounds.center;
            Vector3 size = new Vector3(Data.Width * Data.CellSize, 0, Data.Height * Data.CellSize);
            Vector3 gridOrigin = center - size * 0.5f;
            Vector3 offset = new Vector3((gridPosition.x + 0.5f) * Data.CellSize, 0, (gridPosition.y + 0.5f) * Data.CellSize);
            Vector3 position = gridOrigin + offset;
            float terrainY = GetTerrainY(position);
            position.y = terrainY;
            return position;
        }

        // Converts world position to grid position
        public Vector2Int GetGridPosition(Vector3 worldPosition)
        {
            // Calculate the grid position based on world position and cell size
            var center = Data.MeshRenderer.bounds.center;
            Vector3 size = new Vector3(Data.Width * Data.CellSize, 0, Data.Height * Data.CellSize);
            Vector3 gridOrigin = center - size * 0.5f;

            float x = (worldPosition.x - gridOrigin.x) / Data.CellSize - 0.5f;
            float y = (worldPosition.z - gridOrigin.z) / Data.CellSize - 0.5f;

            return new Vector2Int(Mathf.RoundToInt(x), Mathf.RoundToInt(y));
        }

        // Gets the terrain height at a given position
        public float GetTerrainY(Vector3 pos)
        {
            // Perform a raycast to determine the terrain height at the specified position
            Vector3 startPos = new Vector3(pos.x, pos.y + 10000, pos.z);

            if (Physics.Raycast(startPos, Vector3.down, out RaycastHit hit, float.MaxValue, Data.TerrainLayer))
            {
                return hit.point.y;
            }

            return pos.y;
        }
    }
}