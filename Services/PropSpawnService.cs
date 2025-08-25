using Pathfinding.Data;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using static UnityEditor.PlayerSettings;

namespace Pathfinding.Services
{
    [DefaultExecutionOrder(100)]
    public class PropSpawnService
    {
        [Inject] private LevelGeneratorService _levelGeneratorService;
        [Inject] private LevelUtilityService _levelUtilityService;
        [Inject] private DiContainer _container;

        private GridData Data => _levelGeneratorService.Data;

        public void SetNoiseMap(GridSpawnData gridSpawnData)
        {
            gridSpawnData.NoiseMap = new Texture2D(Data.Width, Data.Height, TextureFormat.RGB24, false);

            for (int x = 0; x < Data.Width; x++)
            {
                for (int y = 0; y < Data.Height; y++)
                {
                    float nx = (float)x / Data.Width * gridSpawnData.NoiseScale;
                    float ny = (float)y / Data.Height * gridSpawnData.NoiseScale;
                    float noise = Mathf.PerlinNoise(nx, ny);
                    gridSpawnData.NoiseMap.SetPixel(x, y, new Color(0, noise, 0));
                }
            }

            gridSpawnData.NoiseMap.Apply();
        }


        public void TrySpawnVegetation(GridSquareData squareData, GridSpawnData spawnData)
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

        public bool TrySpawnProps(GridSquareData squareData, GridSpawnData spawnData)
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

        private void Spawn(GridSquareData squareData, GridSpawnData spawnData)
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
