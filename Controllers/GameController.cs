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
            _inputService.Init(Data);
        }

        private void OnEnable()
        {
            AgentMoveService.OnActionCompleted += AgentMoveService_OnActionCompleted;
        }

        private void OnDisable()
        {
            AgentMoveService.OnActionCompleted -= AgentMoveService_OnActionCompleted;
        }   

        private void Update()
        {
            _inputService.ObtainUserInputs();
            _inputService.HandlePauseInput();

            if (_inputService.Data.AreControlsEnabled)
            {
                _inputService.HandleCameraInput();
                _inputService.HandleMouseInput();
                _inputService.HandleGridInput();
            }
        }

        private void AgentMoveService_OnActionCompleted(object sender, ActionCompletedEventArgs e)
        {
            _levelProgressionService.CheckCompletion(e.Agent.MovementData);
        }

    }
}
