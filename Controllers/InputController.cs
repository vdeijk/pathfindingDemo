using Pathfinding.Data;
using Pathfinding.Services;
using UnityEngine;
using Zenject;

namespace Pathfinding.Controllers
{
    [DefaultExecutionOrder(-100)]
    public class InputController : MonoBehaviour
    {
        [Inject] private PlayerInputService _playerInputService;

        [SerializeField] GameData _data;

        private void Awake()
        {
            // Initialize input service with game data
            _playerInputService.Init(_data);
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
    }
}
