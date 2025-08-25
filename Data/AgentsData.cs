using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Data
{
    [Serializable]
    [DefaultExecutionOrder(100)]
    public class AgentsData
    {
        [field: SerializeField] public Transform EnemyPrefab;
        [field: SerializeField] public Transform PlayerPrefab;
        [field: SerializeField] public Transform PlayerParent;
        [field: SerializeField] public Transform EnemyParent;
        [field: SerializeField] public int NumberOfEnemies;

        public List<AgentData> Enemies { get; set; } = new List<AgentData>();
        public AgentData Player { get; set; }
    }
}