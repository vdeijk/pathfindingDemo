using Pathfinding.Data;
using Pathfinding.Services;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Pathfinding.Controllers
{
    [DefaultExecutionOrder(100)]
    public class AIController : MonoBehaviour
    {
        [Inject] private AgentCategoryService _agentCategoryService;
        [Inject] private LevelUtilityService _levelUtilityService;
        [Inject] private AgentMoveService _agentMoveService;

        public List<AgentData> enemies => _agentCategoryService.Data.Enemies;

        private void OnEnable()
        {
            AgentMoveService.OnActionCompleted += AgentMoveService_OnActionCompleted;
        }

        private void OnDisable()
        {
            AgentMoveService.OnActionCompleted -= AgentMoveService_OnActionCompleted;
        }

        private void Update()
        {
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
        private void AgentMoveService_OnActionCompleted(object sender, ActionCompletedEventArgs e)
        {
            if (e.Agent.AIData == null) return;

            AgentData agent = e.Agent;
            Vector2Int currentPos = _levelUtilityService.GetGridPosition(agent.MovementData.BodyTransform.position);

            if (currentPos == agent.AIData.PointA)
            {
                agent.MovementData.TargetPos = agent.AIData.PointB;
            }
            else if (currentPos == agent.AIData.PointB)
            {
                agent.MovementData.TargetPos = agent.AIData.PointA;
            }

            e.Agent.AIData.IsWaiting = true;
        }
    }
}