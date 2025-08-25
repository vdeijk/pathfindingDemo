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

        public Vector2Int TargetPos { get; set; }
        public List<Vector2Int> PathWaypoints { get; set; }
        public int CurWaypointIndex { get; set; } = 0;
        public AgentAnimationType CurAnimationState { get; set; } = AgentAnimationType.Idle;

        public Vector2Int CurTargetPos => PathWaypoints[CurWaypointIndex];
    }
}

