using System;
using UnityEngine;
using Unity.Cinemachine;

namespace Pathfinding.Data
{
    [Serializable]
    public class CameraData
    {
        [field: SerializeField] public Transform LevelPlane { get; private set; }
        [field: SerializeField] public float MinZoom { get; private set; }
        [field: SerializeField] public float MaxZoom { get; private set; }
        [field: SerializeField] public float ZoomSpeed { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public float RotateSpeed { get; private set; }
        [field: SerializeField] public Transform TrackingTargetTransform { get; private set; }
        [field: SerializeField] public CinemachineThirdPersonFollow CinemachineThirdPersonFollow { get; private set; }
        [field: SerializeField] public MeshRenderer TerrainMesh { get; private set; }
        [field: SerializeField] public float CenteringSmoothConstant { get; private set; }

        // Runtime camera state
        public float DefaultZoom { get; set; }

        // Cam movement boundaries
        public float MinPosX { get; set; }
        public float MaxPosX { get; set; }
        public float MinPosZ { get; set; }
        public float MaxPosZ { get; set; }
    }
}