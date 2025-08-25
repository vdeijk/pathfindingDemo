using Pathfinding.Data;
using Pathfinding.Services;
using UnityEngine;
using Zenject;

namespace Pathfinding.Controllers
{
    [DefaultExecutionOrder(-100)]
    public class AgentController : MonoBehaviour
    {
        [Inject] private AgentMoveService _agentMoveService;
        [Inject] private AgentAnimationService _agentAnimationService;
        [Inject] private AgentCategoryService _agentCategoryService;
        [Inject] private OverheadCameraService _overheadCameraService;
        [Inject] private AgentPathService _agentPathService;

        [SerializeField] MovementData MovementData;
        [SerializeField] AudioData AudioData;
        [SerializeField] AIData AIData;

        public AgentData Data;

        private void Awake()
        {
            // Compose AgentData from Inspector-assigned sub-data
            Data = new AgentData();
            Data.MovementData = MovementData;
            Data.AudioData = AudioData;
            Data.AIData = AIData;
        }

        private void Start()
        {
            // Animate agent and register with category service
            _agentAnimationService.Animate(Data);

            if (AIData.IsEnemy)
            {
                _agentCategoryService.AddEnemy(Data);
                _agentPathService.InitPatrol(Data); // Set up patrol for enemy
            }
            else
            {
                _agentCategoryService.AddPlayer(Data);
                _overheadCameraService.InitCamPosition(); // Set camera for player
            }
        }

        private void Update()
        {
            // Update agent movement/action each frame
            _agentMoveService.UpdateAction(Data);
        }
    }
}