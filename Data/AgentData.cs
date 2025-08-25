using System;
using UnityEngine;

namespace Pathfinding.Data
{
    [Serializable]
    [DefaultExecutionOrder(100)]
    public class AgentData
    {
        public MovementData MovementData { get; set; }
        public AudioData AudioData { get; set; }
        public AIData AIData { get; set; }
    }
}