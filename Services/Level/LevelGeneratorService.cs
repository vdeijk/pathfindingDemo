using Pathfinding.Data;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Pathfinding.Services
{
    [DefaultExecutionOrder(100)]
    public class LevelGeneratorService
    {
        [Inject] private LevelUtilityService _levelUtilityService;
        [Inject] private LevelSpawnService _propSpawnService;
        [Inject] private DiContainer _container;

        private RaycastHit[] _raycastHits = new RaycastHit[5];
        private LevelSpawnData _vegetationSpawnData;
        private LevelSpawnData _propSpawnData;

        public LevelData Data { get; private set; }

        // Initializes the service with Inspector-assigned grid data
        public void Init(LevelData gridData, LevelSpawnData vegetationSpawnData, LevelSpawnData propSpawnData)
        {
            Data = gridData;
            _vegetationSpawnData = vegetationSpawnData;
            _propSpawnData = propSpawnData;
        }

        // Toggles grid visibility in the scene
        public void ToggleGrid(bool input)
        {
            if (!input) return;

            bool isActive = Data.Parent.gameObject.activeSelf;
            Data.Parent.gameObject.SetActive(!isActive);
        }

        // Sets entrance and exit positions based on transforms
        public void SetEntrancAndExit()
        {
            Data.Entrance = _levelUtilityService.GetGridPosition(Data.EntranceTransform.position);
            Data.Exit = _levelUtilityService.GetGridPosition(Data.ExitTransform.position);
        }

        // Creates the grid and initializes grid square data
        public void CreateGrid()
        {
            Data.Width = Mathf.RoundToInt(Data.MeshRenderer.bounds.size.x) / Mathf.RoundToInt(Data.CellSize);
            Data.Height = Mathf.RoundToInt(Data.MeshRenderer.bounds.size.z) / Mathf.RoundToInt(Data.CellSize);
            GridSquareData[,] gridSquares = new GridSquareData[Data.Width, Data.Height];

            for (int x = 0; x < Data.Width; x++)
            {
                for (int z = 0; z < Data.Height; z++)
                {
                    gridSquares[x, z] = new GridSquareData();
                    gridSquares[x, z].GridPosition = new Vector2Int(x, z);
                }
            }

            Data.Squares = gridSquares;
        }

        // Places grid squares and sets their types and colors
        public void PlaceSquares()
        {
            for (int x = 0; x < Data.Width; x++)
            {
                for (int z = 0; z < Data.Height; z++)
                {
                    Vector2Int gridPos = new Vector2Int(x, z);
                    var data = Data.Squares[x, z];

                    data.GO = InstantiateSquare(gridPos, Data.BlockPrefab);

                    SetSquareType(data);
                    SetSquareColor(data);

                    bool isSteep = data.GridSquareType.Contains(GridSquareType.Steep);
                    bool isInaccessible = data.GridSquareType.Contains(GridSquareType.Inaccessible);
                    if (!isSteep && !isInaccessible)
                    {
                        Data.ValidGridPositions.Add(gridPos);
                    }
                }
            }
        }

        // Determines the type of a grid square based on raycast and spawns props/vegetation
        private void SetSquareType(GridSquareData data)
        {
            if (data.GridPosition == Data.Entrance || data.GridPosition == Data.Exit)
            {
                data.GridSquareType.Add(GridSquareType.Normal);
                return;
            }

            Vector3 pos = _levelUtilityService.GetWorldPosition(data.GridPosition) + Vector3.up * 1000f;
            int hits = Physics.RaycastNonAlloc(pos, Vector3.down, _raycastHits, float.MaxValue);

            if (hits > 0)
            {
                for (int i = 0; i < hits; i++)
                {
                    float slope = Vector3.Angle(_raycastHits[i].normal, Vector3.up);
                    float height = _raycastHits[i].point.y;
                    LayerMask layer = _raycastHits[i].collider.gameObject.layer;

                    if ((Data.InaccessibleLayer.value & (1 << layer)) != 0)
                    {
                        data.GridSquareType.Add(GridSquareType.Inaccessible);
                    }
                    else if (slope > 30)
                    {
                        data.GridSquareType.Add(GridSquareType.Steep);
                    }
                    else if (height > 30)
                    {
                        data.GridSquareType.Add(GridSquareType.High);
                    }
                    else if ((Data.TerrainLayer.value & (1 << layer)) != 0)
                    {
                        // Try to spawn props and vegetation, mark as inaccessible/forest if successful
                        if (_propSpawnService.TrySpawnProps(data, _propSpawnData))
                        {
                            data.GridSquareType.Add(GridSquareType.Inaccessible);
                            continue;
                        }
                        if (_propSpawnService.TrySpawnProps(data, _vegetationSpawnData))
                        {
                            data.GridSquareType.Add(GridSquareType.Forest);
                            continue;
                        }
                        else
                        {
                            data.GridSquareType.Add(GridSquareType.Normal);
                        }
                    }
                }
            }
        }

        private void SetSquareColor(GridSquareData data)
        {
            var renderer = data.GO.GetComponent<MeshRenderer>();

            // Set color based on grid square type
            if (data.GridSquareType.Contains(GridSquareType.High))
            {
                renderer.material.color = SetSquareColorAlpha(ColorData.High);
            }
            else if (data.GridSquareType.Contains(GridSquareType.Steep))
            {
                renderer.material.color = SetSquareColorAlpha(ColorData.Steep);
            }
            else if (data.GridSquareType.Contains(GridSquareType.Inaccessible))
            {
                renderer.material.color = SetSquareColorAlpha(ColorData.Inaccessible);
            }
            else if (data.GridSquareType.Contains(GridSquareType.Forest))
            {
                renderer.material.color = SetSquareColorAlpha(ColorData.Forest);
            }
            else
            {
                renderer.material.color = SetSquareColorAlpha(ColorData.Normal);
            }
        }

        // Sets alpha for grid square color
        private Color32 SetSquareColorAlpha(Color32 color)
        {
            Color32 newColor = color;
            color.a = 128;

            return color;
        }

        // Instantiates a grid square prefab at the specified position
        private GameObject InstantiateSquare(Vector2Int gridPos, Transform prefab)
        {
            Vector3 pos = _levelUtilityService.GetWorldPosition(new Vector2Int(gridPos.x, gridPos.y));
            Vector3 finalPos = new Vector3(pos.x, pos.y, pos.z);
            GridSquareData data = Data.Squares[gridPos.x, gridPos.y];

            return _container.InstantiatePrefab(prefab,
                finalPos, Quaternion.identity, Data.Parent);
        }
    }
}
