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

        // List of all enemy agents in the scene
        public List<AgentData> Enemies { get; set; } = new List<AgentData>();
        // Reference to the player agent
        public AgentData Player { get; set; }
    }
}