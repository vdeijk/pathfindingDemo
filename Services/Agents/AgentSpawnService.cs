using Pathfinding.Data;
using Pathfinding.Services;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[DefaultExecutionOrder(100)]
public class AgentSpawnService
{
    [Inject] private DiContainer _container;
    [Inject] private LevelGeneratorService _levelGeneratorService;
    [Inject] private LevelUtilityService _levelUtilityService;

    public AgentsData Data { get; private set; }

    // Initializes the service and spawns player and enemies
    public void Init(AgentsData data)
    {
        Data = data;
    }

    // Spawns all enemy agents at valid positions
    public void SpawnEnemies()
    {
        var validPositions = GetValidSpawnPositions();

        for (int i = 0; i < Data.NumberOfEnemies && validPositions.Count > 0; i++)
        {
            if (validPositions.Count > 0)
            {
                int idx = Random.Range(0, validPositions.Count);
                Vector2Int pos = validPositions[idx];
                Vector3 worldPos = _levelUtilityService.GetWorldPosition(pos);

                // Instantiate enemy prefab using Zenject for injection
                GameObject agent = _container.InstantiatePrefab(Data.EnemyPrefab, worldPos,
                    Quaternion.identity, Data.EnemyParent);
            }
        }
    }

    // Spawns the player agent at the entrance position
    public void SpawnPlayer()
    {
        Vector2Int pos = _levelGeneratorService.Data.Entrance;
        Vector3 worldPos = _levelUtilityService.GetWorldPosition(pos);

        // Instantiate player prefab using Zenject for injection
        GameObject agent = _container.InstantiatePrefab(Data.PlayerPrefab, worldPos,
            Quaternion.identity, Data.PlayerParent);
    }

    // Returns a list of valid grid positions for enemy spawning
    private List<Vector2Int> GetValidSpawnPositions()
    {
        var positions = new List<Vector2Int>();
        var data = _levelGeneratorService.Data;

        for (int x = 0; x < _levelGeneratorService.Data.Width; x++)
        {
            for (int z = 0; z < data.Height; z++)
            {
                Vector2Int pos = new Vector2Int(x, z);
                var square = data.Squares[x, z];
                bool isNormal = square.GridSquareType.Contains(GridSquareType.Normal);
                bool isEntranceOrExit = pos == data.Entrance || pos == data.Exit;
                if (isNormal && !isEntranceOrExit)
                {
                    positions.Add(pos);
                }
            }
        }

        return positions;
    }
}
