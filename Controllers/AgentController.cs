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

        [SerializeField] MovementData _movementData;
        [SerializeField] AudioData _audioData;
        [SerializeField] AIData _aiData;

        private AgentData _data;

        private void Awake()
        {
            _data = new AgentData();
            _data.MovementData = _movementData;
            _data.AudioData = _audioData;
            _data.AIData = _aiData;
        }

        private void Start()
        {
            // Animate agent and register with category service
            _agentAnimationService.Animate(_data);

            if (_aiData.IsEnemy)
            {
                _agentCategoryService.AddEnemy(_data);
                _agentPathService.InitPatrol(_data); // Set up patrol for enemy
            }
            else
            {
                _agentCategoryService.AddPlayer(_data);
                _overheadCameraService.InitPosition(); // Set camera for player
            }
        }

        private void Update()
        {
            // Update agent movement/action each frame
            _agentMoveService.UpdateAction(_data);
        }
    }
}