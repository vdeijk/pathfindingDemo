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
            Data = new AgentData();

            Data.MovementData = MovementData;
            Data.AudioData = AudioData;
            Data.AIData = AIData;
        }

        private void Start()
        {
            _agentAnimationService.Animate(Data);

            if (AIData.IsEnemy)
            {
                _agentCategoryService.AddEnemy(Data);
                _agentPathService.InitPatrol(Data);
            }
            else
            {
                _agentCategoryService.AddPlayer(Data);
                _overheadCameraService.InitCamPosition();
            }
        }

        private void Update()
        {
            _agentMoveService.UpdateAction(Data);
        }
    }
}