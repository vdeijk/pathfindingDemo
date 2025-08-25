using Pathfinding.Data;
using System;
using UnityEngine;

namespace Pathfinding.Services
{
    [DefaultExecutionOrder(100)]
    public class AgentAnimationService
    {
        // Handles movement animation transitions
        public void Animate(AgentData data)
        {
            Animator unitAnimator = data.MovementData.BodyTransform.GetComponentInChildren<Animator>();
            foreach (AnimatorControllerParameter parameter in unitAnimator.parameters)
            {
                unitAnimator.SetBool(parameter.name, false);
            }
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

        public void AnimateDeath(AgentData data)
        {
            Animator unitAnimator = data.MovementData.BodyTransform.GetComponentInChildren<Animator>();
            unitAnimator.SetTrigger("Death");
        }
    }
}