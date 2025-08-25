using Pathfinding.Data;
using Pathfinding.Services;
using UnityEngine;
using Zenject;

namespace Pathfinding.Controllers
{
    [DefaultExecutionOrder(-100)]
    public class GameController : MonoBehaviour
    {
        [Inject] private InputService _inputService;
        [Inject] private LevelProgressionService _levelProgressionService;

        [SerializeField] GameData Data;

        private void Awake()
        {
            // Initialize input service with game data
            _inputService.Init(Data);
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
            _inputService.ObtainUserInputs();
            _inputService.HandlePauseInput();

            if (_inputService.Data.AreControlsEnabled)
            {
                _inputService.HandleCameraInput();
                _inputService.HandleMouseInput();
                _inputService.HandleGridInput();
                _inputService.HandleCenteringInput();
            }
        }

        // Called when an agent completes its action; checks for level completion
        private void AgentMoveService_OnActionCompleted(object sender, ActionCompletedEventArgs e)
        {
            _levelProgressionService.CheckCompletion(e.Agent.MovementData);
        }

    }
}
