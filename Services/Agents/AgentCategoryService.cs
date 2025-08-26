using System;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding.Data;

namespace Pathfinding.Services
{
    [DefaultExecutionOrder(100)]
    public class AgentCategoryService
    {
        public AgentsData Data { get; private set; }

        public static event EventHandler OnAgentAdded;
        public static event EventHandler OnUnitRemoved;

        // Initializes the service with Inspector-assigned agent data
        public void Init(AgentsData data)
        {
            Data = data;
        }

        // Registers the player agent and triggers event
        public void AddPlayer(AgentData agent)
        {
            Data.Player = agent;
            OnAgentAdded?.Invoke(this, EventArgs.Empty);
        }

        // Registers an enemy agent and triggers event
        public void AddEnemy(AgentData agent)
        {
            Data.Enemies.Add(agent);
            OnAgentAdded?.Invoke(this, EventArgs.Empty);
        }
    }
}
