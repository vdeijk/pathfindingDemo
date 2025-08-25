using System;
using UnityEngine;
using System.Collections.Generic;

namespace Pathfinding.Data
{
    [Serializable]
    public class GridSpawnData
    {
        [field: SerializeField] public Transform Parent { get; private set; }
        [field: SerializeField] public float NoiseScale { get; private set; }
        [field: SerializeField] public List<Transform> Prefabs { get; private set; }
        [field: SerializeField] public float MinScale { get; private set; }
        [field: SerializeField] public float MaxScale { get; private set; }
        [field: SerializeField] public float ThresholdMin { get; private set; }
        [field: SerializeField] public float ThresholdMax { get; private set; }

        public Texture2D NoiseMap;
    }
}