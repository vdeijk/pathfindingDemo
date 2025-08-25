using Pathfinding.Data;
using UnityEngine;
using Zenject;

namespace Pathfinding.Services
{
    [DefaultExecutionOrder(100)]
    public class LevelUtilityService
    {
        [Inject] private LevelGeneratorService _levelGeneratorService;

        public GridData Data { get; private set; }

        public void Init(GridData data)
        {
            Data = data;
        }

        public bool IsWalkable(Vector2Int gridPosition)
        {
            bool isWidthOutsideGrid = gridPosition.x < 0 || gridPosition.x >= Data.Width;
            bool isHeightOutsideGrid = gridPosition.y < 0 || gridPosition.y >= Data.Height;
            if (isWidthOutsideGrid || isHeightOutsideGrid)
            {
                return false;
            }

            GridSquareData gridObject = _levelGeneratorService.Data.Squares[gridPosition.x, gridPosition.y];

            bool isInaccessible = gridObject.GridSquareType.Contains(GridSquareType.Inaccessible);
            bool isSteep = gridObject.GridSquareType.Contains(GridSquareType.Steep);
            if (isSteep || isInaccessible)
            {
                return false;
            }

            if (gridObject.Agents.Count > 0)
            {
                return false;
            }

            return true;
        }

        public Vector3 GetWorldPosition(Vector2Int gridPosition)
        {
            var center = Data.MeshRenderer.bounds.center;
            Vector3 size = new Vector3(Data.Width * Data.CellSize, 0, Data.Height * Data.CellSize);
            Vector3 gridOrigin = center - size * 0.5f;
            Vector3 offset = new Vector3((gridPosition.x + 0.5f) * Data.CellSize, 0, (gridPosition.y + 0.5f) * Data.CellSize);
            Vector3 position = gridOrigin + offset;
            float terrainY = GetTerrainY(position);
            position.y = terrainY;
            return position;
        }

        public Vector2Int GetGridPosition(Vector3 worldPosition)
        {
            var center = Data.MeshRenderer.bounds.center;
            Vector3 size = new Vector3(Data.Width * Data.CellSize, 0, Data.Height * Data.CellSize);
            Vector3 gridOrigin = center - size * 0.5f;

            float x = (worldPosition.x - gridOrigin.x) / Data.CellSize - 0.5f;
            float y = (worldPosition.z - gridOrigin.z) / Data.CellSize - 0.5f;

            return new Vector2Int(Mathf.RoundToInt(x), Mathf.RoundToInt(y));
        }

        public float GetTerrainY(Vector3 pos)
        {
            Vector3 startPos = new Vector3(pos.x, pos.y + 10000, pos.z);

            if (Physics.Raycast(startPos, Vector3.down, out RaycastHit hit, float.MaxValue, Data.TerrainLayer))
            {
                return hit.point.y;
            }

            return pos.y;
        }
    }
}