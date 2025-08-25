using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Data
{
    [Serializable]
    public class MovementData
    {
        [field: SerializeField] public Transform BodyTransform { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public float RotateSpeed { get; private set; }
        [field: SerializeField] public Rigidbody Rb { get; private set; }

        // Target grid position for movement
        public Vector2Int TargetPos { get; set; }
        // Waypoints for pathfinding
        public List<Vector2Int> PathWaypoints { get; set; }
        // Current waypoint index in the path
        public int CurWaypointIndex { get; set; } = 0;
        // Current animation state for movement
        public AgentAnimationType CurAnimationState { get; set; } = AgentAnimationType.Idle;

        // Returns the current target position from the path waypoints
        public Vector2Int CurTargetPos => PathWaypoints[CurWaypointIndex];
    }
}

