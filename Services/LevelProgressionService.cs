using Pathfinding.Data;
using System;
using UnityEngine;
using Zenject;

namespace Pathfinding.Services
{
    [DefaultExecutionOrder(200)]
    public class LevelProgressionService
    {
        [Inject] private LevelGeneratorService _levelGeneratorService;
        [Inject] private LevelUtilityService _levelUtilityService;

        public static event EventHandler OnLevelCompleted;

        public void CheckCompletion(MovementData data)
        {
            Vector2Int curPos = _levelUtilityService.GetGridPosition(data.Rb.transform.position);

            if (curPos == _levelGeneratorService.Data.Exit)
            {
                Debug.Log("Level Completed!");
                OnLevelCompleted?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}