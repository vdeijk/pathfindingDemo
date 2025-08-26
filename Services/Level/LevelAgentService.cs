using Pathfinding.Data;
using UnityEngine;
using Zenject;

namespace Pathfinding.Services
{
    [DefaultExecutionOrder(100)]
    public class LevelAgentService
    {
        [Inject] private LevelGeneratorService _levelGeneratorService;
        [Inject] private LevelUtilityService _levelUtilityService;

        // Adds an agent to the grid square at its current position
        public void AddAgent(AgentData data)
        {
            Vector2Int unitGridPosition = _levelUtilityService.GetGridPosition(data.MovementData.Rb.transform.position);
            GridSquareData curGridSquare = _levelGeneratorService.Data.Squares[unitGridPosition.x, unitGridPosition.y];

            if (curGridSquare.Agents.Contains(data)) return;
            curGridSquare.Agents.Add(data);
        }

        // Removes an agent from the specified grid square
        public void RemoveAgent(AgentData data, Vector2Int gridPos)
        {
            GridSquareData gridSquare = _levelGeneratorService.Data.Squares[gridPos.x, gridPos.y];

            if (!gridSquare.Agents.Contains(data)) return;
            gridSquare.Agents.Remove(data);
        }
    }
}