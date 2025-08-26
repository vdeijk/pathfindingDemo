using Pathfinding.Data;
using System;
using UnityEngine;

namespace Pathfinding.Services
{
    [DefaultExecutionOrder(100)]
    public class AgentAnimationService
    {
        // Handles movement animation transitions for an agent
        public void Animate(AgentData data)
        {
            Animator unitAnimator = data.MovementData.BodyTransform.GetComponentInChildren<Animator>();
            // Reset all animator parameters to false
            foreach (AnimatorControllerParameter parameter in unitAnimator.parameters)
            {
                unitAnimator.SetBool(parameter.name, false);
            }
            // Set the appropriate animation state
            switch (data.MovementData.CurAnimationState)
            {
                case AgentAnimationType.WalkFwd:
                    unitAnimator.SetBool("Walk", true);
                    break;
                default:
                    unitAnimator.SetBool("Idle", true);
                    break;
            }
        }
    }
}