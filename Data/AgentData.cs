using System;
using UnityEngine;

namespace Pathfinding.Data
{
    [Serializable]
    [DefaultExecutionOrder(100)]
    public class AgentData
    {
        // Holds movement, audio, and AI data for an agent
        public MovementData MovementData { get; set; }
        public AudioData AudioData { get; set; }
        public AIData AIData { get; set; }
    }
}