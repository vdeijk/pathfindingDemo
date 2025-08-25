using Pathfinding.Data;
using UnityEngine;
using Zenject;

namespace Pathfinding.Services
{
    [DefaultExecutionOrder(100)]
    public class GridAgentService
    {
        [Inject] private LevelGeneratorService _levelGeneratorService;
        [Inject] private LevelUtilityService _levelUtilityService;

        public void AddAgent(AgentData data)
        {
            Vector2Int unitGridPosition = _levelUtilityService.GetGridPosition(data.MovementData.Rb.transform.position);
            GridSquareData curGridSquare = _levelGeneratorService.Data.Squares[unitGridPosition.x, unitGridPosition.y];

            if (curGridSquare.Agents.Contains(data)) return;
            curGridSquare.Agents.Add(data);
        }

        public void RemoveAgent(AgentData data, Vector2Int gridPos)
        {
            GridSquareData gridSquare = _levelGeneratorService.Data.Squares[gridPos.x, gridPos.y];

            if (!gridSquare.Agents.Contains(data)) return;
            gridSquare.Agents.Remove(data);
        }
    }
}