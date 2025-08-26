using Pathfinding.Data;
using Pathfinding.Services;
using UnityEngine;
using Zenject;

namespace Pathfinding.Controllers
{
    [DefaultExecutionOrder(-100)]
    public class GameController : MonoBehaviour
    {
        [Inject] private PlayerInputService _playerInputService;
        [Inject] private LevelProgressionService _levelProgressionService;

        [SerializeField] GameData _data;

        private void Awake()
        {
            // Initialize input service with game data
            _playerInputService.Init(_data);
        }

        private void OnEnable()
        {
            // Subscribe to agent action completion events
            AgentMoveService.OnActionCompleted += AgentMoveService_OnActionCompleted;
        }

        private void OnDisable()
        {
            AgentMoveService.OnActionCompleted -= AgentMoveService_OnActionCompleted;
        }   

        private void Update()
        {
            // Handle user input and game controls
            _playerInputService.ObtainUserInputs();
            _playerInputService.HandlePauseInput();

            if (_playerInputService.Data.AreControlsEnabled)
            {
                _playerInputService.HandleCameraInput();
                _playerInputService.HandleMouseInput();
                _playerInputService.HandleGridInput();
                _playerInputService.HandleCenteringInput();
            }
        }

        // Called when an agent completes its action; checks for level completion
        private void AgentMoveService_OnActionCompleted(object sender, ActionCompletedEventArgs e)
        {
            _levelProgressionService.CheckCompletion(e.Agent.MovementData);
        }

    }
}
