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

        [SerializeField] InputData _data;

        private void Awake()
        {
            _playerInputService.Init(_data);
        }

        private void Update()
        {
            // Handle user input and game controls
            _playerInputService.ObtainUserInputs();

            if (_playerInputService.Data.AreControlsEnabled)
            {
                _playerInputService.HandleCameraInput();
                _playerInputService.HandleMouseInput();
                _playerInputService.HandleOtherInput();
            }
        }
    }
}
