using Pathfinding.Data;
using System;
using UnityEngine;
using Zenject;

namespace Pathfinding.Services
{
    public class ActionCompletedEventArgs : EventArgs
    {
        public AgentData Agent { get; }

        public ActionCompletedEventArgs(AgentData agent)
        {
            Agent = agent;
        }
    }

    [DefaultExecutionOrder(100)]
    public class AgentMoveService
    {
        [Inject] private AgentCategoryService _agentCategoryService;
        [Inject] private LevelAgentService _levelAgentService;
        [Inject] private LevelGeneratorService _levelGeneratorService;
        [Inject] private AgentPathService _agentPathService;
        [Inject] private LevelUtilityService _levelUtilityService;
        [Inject] private AgentAnimationService _agentAnimationService;
        [Inject] private AudioMonobService _audioController;
        [Inject] private LevelProgressionService _levelProgressionService;

        public static event EventHandler<ActionCompletedEventArgs> OnActionCompleted;

        // Updates agent movement and handles action completion
        public void UpdateAction(AgentData data)
        {
            if (data.MovementData.PathWaypoints != null)
            {
                switch (SetType(data.MovementData))
                {
                    case MovementType.Rotate:
                        UpdateRotation(data.MovementData);
                        break;

                    case MovementType.Move:
                        UpdateMovement(data);
                        break;

                    case MovementType.Next:
                        data.MovementData.CurWaypointIndex++;
                        if (data.MovementData.CurWaypointIndex >= data.MovementData.PathWaypoints.Count)
                        {
                            CompleteAction(data);
                        }
                        break;
                }
            }
        }

        // Starts a new movement action for the agent
        public void StartAction(AgentData data)
        {
            LevelData gridData = _levelGeneratorService.Data;

            bool isRangeValid = gridData.ValidGridPositions.Contains(data.MovementData.TargetPos);

            if (!isRangeValid) return;

            var path = _agentPathService.FindPath(data.MovementData);

            data.MovementData.PathWaypoints = path;
            data.MovementData.CurWaypointIndex = 0;

            data.AudioData.TargetVolume = data.AudioData.MaxVolume;
            _audioController.StartFade(data.AudioData);

            data.MovementData.CurAnimationState = AgentAnimationType.WalkFwd;
            _agentAnimationService.Animate(data);
            if (!data.AIData.IsEnemy) _audioController.PlayStartActionSound();
        }

        // Handles completion of movement/action and resets agent state
        private void CompleteAction(AgentData data)
        {
            Vector2Int curPos = _levelUtilityService.GetGridPosition(data.MovementData.Rb.transform.position);
            Vector3 pos = _levelUtilityService.GetWorldPosition(curPos);

            data.MovementData.Rb.position = pos;

            data.MovementData.PathWaypoints = null;

            data.AudioData.TargetVolume = 0;
            _audioController.StartFade(data.AudioData);

            data.MovementData.CurAnimationState = AgentAnimationType.Idle;
            _agentAnimationService.Animate(data);
            if (!data.AIData.IsEnemy) _audioController.PlayEndActionSound();

            _levelProgressionService.CheckCompletion(data.MovementData);

            OnActionCompleted?.Invoke(this, new ActionCompletedEventArgs(data));
        }

        // Determines the next movement type for the agent
        private MovementType SetType(MovementData data)
        {
            Vector3 targetWorldPos = _levelUtilityService.GetWorldPosition(data.CurTargetPos);

            if ((data.Rb.position - targetWorldPos).sqrMagnitude < 0.0001f)
            {
                data.Rb.position = targetWorldPos;

                return MovementType.Next;
            }

            Vector3 flatTarget = new Vector3(targetWorldPos.x, data.Rb.position.y, targetWorldPos.z);
            Vector3 worldDirection = (flatTarget - data.Rb.position).normalized;
            Quaternion targetRot = Quaternion.LookRotation(worldDirection);

            float angleDiff = Quaternion.Angle(data.Rb.transform.rotation, targetRot);
            Vector2Int curPos = _levelUtilityService.GetGridPosition(data.Rb.transform.position);
            if (angleDiff > 0.1f && data.CurTargetPos != curPos)
            {
                return MovementType.Rotate;
            }

            data.Rb.rotation = targetRot;

            return MovementType.Move;
        }

        // Moves the agent towards the next waypoint
        private void UpdateMovement(AgentData data)
        {
            Vector2Int nextGridPos = data.MovementData.CurTargetPos;
            var nextSquare = _levelGeneratorService.Data.Squares[nextGridPos.x, nextGridPos.y];

            if (nextSquare.Agents.Count > 0 && !nextSquare.Agents.Contains(data))
            {
                return;
            }

            Vector3 nextWorldPos = _levelUtilityService.GetWorldPosition(nextGridPos);
            Vector3 newPos = Vector3.MoveTowards(
                data.MovementData.Rb.position,
                nextWorldPos,
                data.MovementData.MoveSpeed * Time.fixedDeltaTime
            );

            Vector2Int curPos = _levelUtilityService.GetGridPosition(data.MovementData.Rb.transform.position);

            data.MovementData.Rb.MovePosition(newPos);
        }

        // Rotates the agent towards the target direction
        private void UpdateRotation(MovementData data)
        {
            Vector2Int curPos = _levelUtilityService.GetGridPosition(data.Rb.transform.position);
            Vector2Int direction = data.CurTargetPos - curPos;
            Vector3 worldDirection = new Vector3(direction.x, 0, direction.y).normalized;
            Quaternion targetRot = Quaternion.LookRotation(worldDirection);

            Quaternion newRotation = Quaternion.Slerp(
                data.Rb.rotation,
                targetRot,
                data.RotateSpeed * Time.fixedDeltaTime
            );

            data.Rb.MoveRotation(newRotation);
        }

        // Teleports the player agent to the entrance position
        public void TeleportPlayerToEntrance()
        {
            AgentData data = _agentCategoryService.Data.Player;
            Vector2Int entrance = _levelGeneratorService.Data.Entrance;

            Vector3 nextWorldPos = _levelUtilityService.GetWorldPosition(entrance);

            data.MovementData.Rb.MovePosition(nextWorldPos);

            _levelAgentService.AddAgent(data);
        }
    }
}
