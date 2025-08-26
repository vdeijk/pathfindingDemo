using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Data
{
    [Serializable]
    [DefaultExecutionOrder(100)]
    public class AgentsData
    {
        [field: SerializeField] public Transform EnemyPrefab { get; private set; }
        [field: SerializeField] public Transform PlayerPrefab { get; private set; }
        [field: SerializeField] public Transform PlayerParent { get; private set; }
        [field: SerializeField] public Transform EnemyParent { get; private set; }
        [field: SerializeField] public int NumberOfEnemies { get; private set; } = 5;

        // List of all enemy agents in the scene
        public List<AgentData> Enemies { get; set; } = new List<AgentData>();
        // Reference to the player agent
        public AgentData Player { get; set; }
    }
}