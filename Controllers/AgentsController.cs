using Pathfinding.Data;
using Pathfinding.Services;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Pathfinding.Controllers
{
    [DefaultExecutionOrder(-100)]
    public class AgentsController : MonoBehaviour
    {
        [Inject] private AgentCategoryService _agentCategoryService;
        [Inject] private AgentSpawnService _agentSpawnService;
        [Inject] private LevelUtilityService _levelUtilityService;
        [Inject] private AgentMoveService _agentMoveService;

        [SerializeField] AgentsData _data;

        private List<AgentData> enemies => _agentCategoryService.Data.Enemies;

        private void OnEnable()
        {
            // Subscribe to agent action completion events
            AgentMoveService.OnActionCompleted += AgentMoveService_OnActionCompleted;
        }

        private void OnDisable()
        {
            AgentMoveService.OnActionCompleted -= AgentMoveService_OnActionCompleted;
        }

        private void Start()
        {
            // Initialize agent category and spawn services with Inspector-assigned data
            _agentCategoryService.Init(_data);
            _agentSpawnService.Init(_data);
        }

        private void Update()
        {
            // Handle enemy waiting logic and trigger new actions when wait is over
            foreach (AgentData enemy in enemies)
            {
                if (enemy.AIData.IsWaiting)
                {
                    enemy.AIData.CurWaitTime -= Time.deltaTime;
                    if (enemy.AIData.CurWaitTime <= 0f)
                    {
                        enemy.AIData.IsWaiting = false;
                        enemy.AIData.CurWaitTime = UnityEngine.Random.Range(enemy.AIData.MinWaitTime, enemy.AIData.MaxWaitTime);

                        _agentMoveService.StartAction(enemy);
                    }
                }
            }
        }

        // Handles patrol switching for enemies when their action completes
        private void AgentMoveService_OnActionCompleted(object sender, ActionCompletedEventArgs e)
        {
            if (e.Agent.AIData == null) return;

            AgentData agent = e.Agent;
            Vector2Int currentPos = _levelUtilityService.GetGridPosition(agent.MovementData.BodyTransform.position);

            agent.MovementData.TargetPos = (currentPos == agent.AIData.PointA)
                ? agent.AIData.PointB : agent.AIData.PointA;

            e.Agent.AIData.IsWaiting = true;
        }
    }
}