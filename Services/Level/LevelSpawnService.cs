using Pathfinding.Data;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Pathfinding.Services
{
    [DefaultExecutionOrder(100)]
    public class LevelSpawnService
    {
        [Inject] private LevelGeneratorService _levelGeneratorService;
        [Inject] private LevelUtilityService _levelUtilityService;
        [Inject] private DiContainer _container;

        // Shortcut to grid data
        private LevelData _data => _levelGeneratorService.Data;

        // Generates a noise map for procedural prop/vegetation spawning
        public void SetNoiseMap(LevelSpawnData gridSpawnData)
        {
            gridSpawnData.NoiseMap = new Texture2D(_data.Width, _data.Height, TextureFormat.RGB24, false);

            for (int x = 0; x < _data.Width; x++)
            {
                for (int y = 0; y < _data.Height; y++)
                {
                    float nx = (float)x / _data.Width * gridSpawnData.NoiseScale;
                    float ny = (float)y / _data.Height * gridSpawnData.NoiseScale;
                    float noise = Mathf.PerlinNoise(nx, ny);
                    gridSpawnData.NoiseMap.SetPixel(x, y, new Color(0, noise, 0));
                }
            }

            gridSpawnData.NoiseMap.Apply();
        }

        // Attempts to spawn vegetation based on noise and threshold
        public void TrySpawnVegetation(GridSquareData squareData, LevelSpawnData spawnData)
        {
            float noiseValue = spawnData.NoiseMap.GetPixel(squareData.GridPosition.x, squareData.GridPosition.y).g;
            float threshold = Random.Range(spawnData.ThresholdMin, spawnData.ThresholdMax);

            if (noiseValue > threshold)
            {
                squareData.GridSquareType.Add(GridSquareType.Forest);
                Spawn(squareData, spawnData);
            }
            else
            {
                squareData.GridSquareType.Add(GridSquareType.Normal);
                return;
            }
        }

        // Attempts to spawn props based on noise and threshold
        public bool TrySpawnProps(GridSquareData squareData, LevelSpawnData spawnData)
        {
            float noiseValue = spawnData.NoiseMap.GetPixel(squareData.GridPosition.x, squareData.GridPosition.y).g;
            float threshold = Random.Range(spawnData.ThresholdMin, spawnData.ThresholdMax);

            if (noiseValue > threshold)
            {
                Spawn(squareData, spawnData);
                return true;
            }

            return false;
        }

        // Spawns a random prefab at the grid square position with random scale
        private void Spawn(GridSquareData squareData, LevelSpawnData spawnData)
        {
            List<Vector3> spawnPositions = new List<Vector3>();
            Vector3 newPos = _levelUtilityService.GetWorldPosition(squareData.GridPosition);
            spawnPositions.Add(newPos);

            foreach (Vector3 pos in spawnPositions)
            {
                int randomPrefab = Random.Range(0, spawnData.Prefabs.Count);
                int randomRotation = Random.Range(0, 360);

                GameObject go = _container.InstantiatePrefab(spawnData.Prefabs[randomPrefab],
                    pos, Quaternion.identity, spawnData.Parent);

                float randomScale = Random.Range(spawnData.MinScale, spawnData.MaxScale);
                go.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
            }
        }
    }
}
